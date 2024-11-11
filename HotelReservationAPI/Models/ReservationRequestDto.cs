// Define el espacio de nombres HotelReservationAPI.Models.
namespace HotelReservationAPI.Models
{
    // Define la clase ReservationRequestDto.
    public class ReservationRequestDto
    {
        // Propiedad RoomId que se refiere a la identificación de la habitación solicitada.
        public int RoomId { get; set; }

        // Propiedad StartDate que indica la fecha de inicio de la reserva solicitada.
        public DateTime StartDate { get; set; }

        // Propiedad EndDate que indica la fecha de finalización de la reserva solicitada.
        public DateTime EndDate { get; set; }

        // Se podría agregar esto como una validación personalizada para verificar la fecha de inicio.
        public bool IsStartDateValid()
        {
            // Verifica si la fecha de inicio es en el futuro (igual o posterior a la fecha actual).
            return StartDate >= DateTime.Now.Date;  
        }

        // Agrega una validación personalizada para la duración de la reserva.
        public bool IsValidReservationLength()
        {
            // Asegura que la reserva no sea superior a 30 días.
            return (EndDate - StartDate).Days <= 30;  
        }
    }
}
