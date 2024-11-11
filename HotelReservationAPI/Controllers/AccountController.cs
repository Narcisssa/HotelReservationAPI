// Importa el espacio de nombres que contienen las definiciones de modelos y dependencias necesarias.
using HotelReservationAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// Define el espacio de nombres para los controladores de la API del sistema de reservas de hotel
namespace HotelReservationAPI.Controllers
{
    // Marca la clase como un controlador de API y define la ruta base para las solicitudes HTTP
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        // Variables para gestionar usuarios y configuraciones
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        // Constructor del controlador, inicializa UserManager y IConfiguration
        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        // Método para registrar un nuevo usuario
        // POST: api/account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // Verifica si el nombre de usuario ya está en uso
            if (await _userManager.FindByNameAsync(model.Username) != null)
                return BadRequest("El nombre de usuario ya está en uso.");

            // Crea un nuevo usuario con los datos del modelo
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            // Intenta crear el usuario con la contraseña proporcionada
            var result = await _userManager.CreateAsync(user, model.Password);

            // Verifica si la creación del usuario fue exitosa
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Asigna el rol de usuario al nuevo usuario
            await _userManager.AddToRoleAsync(user, model.Role);

            // Devuelve un mensaje de éxito
            return Ok("Usuario registrado exitosamente");
        }

        // Método para iniciar sesión
        // POST: api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Busca al usuario por su nombre de usuario
            var user = await _userManager.FindByNameAsync(model.Username);
            // Verifica si el usuario existe y si la contraseña es correcta
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized("Nombre de usuario o contraseña incorrecta");

            // Obtiene los roles del usuario
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id), // user.Id es el userId único
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Customer") // O el rol apropiado
            };

            // Añade todos los roles del usuario a los claims de autenticación
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            // Genera una clave de firma para el JWT
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            // Devuelve el token y su fecha de expiración
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
    }
}
