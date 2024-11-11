namespace HotelReservationAPI.Models
{
    public class ReservationRequestDto
    {
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // que la fecha sea no sea una pasada
         public bool IsStartDateValid()
        {
            return StartDate >= DateTime.Now.Date; 
        }

        // no más de 30 días por reserva 
        public bool IsValidReservationLength()
        {
            return (EndDate - StartDate).Days <= 30;  
        }
    }
}
