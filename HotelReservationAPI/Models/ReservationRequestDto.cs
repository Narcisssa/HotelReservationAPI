namespace HotelReservationAPI.Models
{
    public class ReservationRequestDto
    {
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // You could add this as a custom validation to check the start date
        public bool IsStartDateValid()
        {
            return StartDate >= DateTime.Now.Date;  // Checks if the start date is in the future
        }

        // Add custom validation for the reservation length
        public bool IsValidReservationLength()
        {
            return (EndDate - StartDate).Days <= 30;  // Ensures reservation is no longer than 30 days
        }
    }
}
