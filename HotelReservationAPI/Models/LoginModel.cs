// Importa el espacio de nombres System.ComponentModel.DataAnnotations,(atributos y clases) para validar los datos en las aplicaciones .NET.
using System.ComponentModel.DataAnnotations;

namespace HotelReservationAPI.Models
{
    // Define la clase LoginModel.
    public class LoginModel
    {
        // Atributo Required indica que la propiedad Username es obligatoria.
        [Required]
        public required string Username { get; set; }

        // Atributo Required indica que la propiedad Password es obligatoria.
        [Required]
        public required string Password { get; set; }
    }
}
