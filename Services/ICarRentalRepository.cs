using CarRentalLibrary.Models;

namespace abraham_luzon_group6_assignment.Services
{
    public interface ICarRentalRepository
    {
        IEnumerable<Car> GetCarByCarRentalCompany(string carrentalid);
        IEnumerable<Car> GetCars();
        IEnumerable<Booking> GetBookings();
        Task<bool> CarRentalCompanyExistAsync(string carrentalid);
        Task AddCarRentalCompany(Carrental carrental);
        Task<Car> GetCarById(string id);
        void DeleteCarRental (Carrental carrental);

        //Ingredient Related queries
        IEnumerable<Carrental> GetCarRentalCompanies();
        Task<Carrental> GetCarRentalById(string id);

        Task<Booking> GetBookingById(int id);
        Task<bool> CarExistsAsync(string carrid);
        Task<bool> BookingExistsAsync(int bookingid);
        Task AddCar(Car newCar);

        Task AddBooking(Booking newBooking);
        void DeleteCar(Car car);
        void DeleteBooking(Booking booking);

        Task<bool> SaveAsync();
    }
}
