using HotelReservationAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelReservationAPI.Controllers
{
    // controlador para gestionar cuentas 
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        // inyección de dependencias de UserManager e IConfiguration
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        // inicializar UserManager e IConfiguration
        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        // registrar un nuevo usuario
        // POST: api/account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // nombre de usuario ya existente 
            if (await _userManager.FindByNameAsync(model.Username) != null)
                return BadRequest("El nombre de usuario ya está en uso.");

          
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            // crea un nuevo usuario en la BD 
            var result = await _userManager.CreateAsync(user, model.Password);

            // devolver los errores en caso de fallo
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // asigna el rol de usuario al nuevo usuario
            await _userManager.AddToRoleAsync(user, model.Role);

            return Ok("Usuario registrado exitosamente");
        }

        // método para iniciar sesión
        // POST: api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // buscar el usuario por  nombre 
            var user = await _userManager.FindByNameAsync(model.Username);
           
            // usuario o contraseña son incorrectos
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized("Nombre de usuario o contraseña incorrecta");

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Customer") 
            };

            // agrega cada rol de usuario a los claims 
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            // generar JWT con claims, clave de firma y configuracion
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            // devuelve JWT y la fecha de expiración
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
    }
}
