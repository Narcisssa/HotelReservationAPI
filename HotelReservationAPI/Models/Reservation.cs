// Define el espacio de nombres HotelReservationAPI.Models
namespace HotelReservationAPI.Models
{
    // Define la clase Reservation
    public class Reservation
    {
        // Atributo Id para identificar de manera única cada reserva
        public int Id { get; set; }

        // Atributo RoomId que se refiere a la habitación reservada
        public int RoomId { get; set; }

        // Atributo Room que representa la relación con la clase Room
        public Room? Room { get; set; }

        // Atributo UserId que se refiere al usuario que hizo la reserva 
        public string? UserId { get; set; }

        // Atributo User que representa la relación con la clase ApplicationUser 
        public ApplicationUser? User { get; set; }

        // Atributo StartDate que indica la fecha de inicio de la reserva
        public DateTime StartDate { get; set; }

        // Atributo EndDate que indica la fecha de finalización de la reserva
        public DateTime EndDate { get; set; }
    }
}
