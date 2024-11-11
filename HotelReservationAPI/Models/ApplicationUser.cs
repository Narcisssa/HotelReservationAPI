// Importa el espacio de nombres Microsoft.AspNetCore.Identity, que contiene clases y métodos para la gestión de identidades en ASP.NET Core.
using Microsoft.AspNetCore.Identity;

namespace HotelReservationAPI.Models
{
    // Define la clase  ApplicationUser que hereda de IdentityUser.
    // IdentityUser => clase de Identity que contiene propiedades para la identidad de un usuario (UserName, Email, Password, etc)
    public class ApplicationUser : IdentityUser
    {
    }
}
