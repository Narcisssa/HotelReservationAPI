// Define el espacio de nombres HotelReservationAPI.Models.
namespace HotelReservationAPI.Models
{
    // Define la clase Room.
    public class Room
    {
        // Propiedad Id para identificar de manera única cada habitación.
        public int Id { get; set; }

        // Propiedad Number que indica el número de la habitación.
        // Marcada como required para asegurar que siempre tenga un valor.
        public required string Number { get; set; }

        // Propiedad Type que indica el tipo de habitación (Sencilla, Doble, Suite).Se marca como required para asegurar que siempre tenga un valor.
        public required string Type { get; set; } // Sencilla, Doble, Suite

        // Propiedad Price que indica el precio de la habitación.
        public decimal Price { get; set; }
    }
}
