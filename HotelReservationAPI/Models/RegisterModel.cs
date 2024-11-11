// Importa el espacio de nombres System.ComponentModel.DataAnnotations,que contiene atributos y clases para validar datos en las aps .NET.
using System.ComponentModel.DataAnnotations;

namespace HotelReservationAPI.Models
{
    // Define la clase RegisterModel.
    public class RegisterModel
    {
        // Atributo Required indica que la propiedad Username es obligatoria.
        [Required]
        public required string Username { get; set; }

        // Atributo Required pide que el Email sea obligatorio.
        // Atributo EmailAddress valida que la propiedad Email tiene un formato de correo electrónico.
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        // Atributo Required indica que la propiedad Password es obligatoria.
        [Required]
        public required string Password { get; set; }

        // Atributo Required indica que la propiedad Role es obligatoria.
        // La propiedad Role puede tomar valores como "Customer" o "Admin".
        [Required]
        public required string Role { get; set; } // "Customer" o "Admin"
    }
}
