// Importa el espacio de nombres que contienen las funcionalidades de Entity Framework Core.
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// Importa el espacio de los  nombres que contiene los modelos utilizados en la API
using HotelReservationAPI.Models;

namespace HotelReservationAPI.Context
{
    // Define la clase HotelDbContext que hereda de IdentityDbContext y utiliza ApplicationUser.
    public class HotelDbContext : IdentityDbContext<ApplicationUser>
    {
        // Constructor que recibe opciones de configuración y las pasa a la clase base.
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        // Define un DbSet para las habitaciones.
        public DbSet<Room> Rooms { get; set; }

        // Define un DbSet para las reservas.
        public DbSet<Reservation> Reservations { get; set; }

        // Método para configurar el modelo utilizando ModelBuilder.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configura el tipo de columna para la propiedad Price de la entidad Room.
            modelBuilder.Entity<Room>().Property(r => r.Price).HasColumnType("decimal(18,2)");
        }
    }
}
