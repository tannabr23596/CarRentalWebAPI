using CarRentalLibrary.Models;
using abraham_luzon_group6_assignment.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace abraham_luzon_group6_assignment.Services
{
    public class CarRentalRepository : ICarRentalRepository
    {
        private CarRentalDBContext dBContext;

        public CarRentalRepository(CarRentalDBContext context)
        {
            this.dBContext = context;
        }
        public async Task<bool> CarRentalCompanyExistAsync(string carrentalid)
        {
            return await dBContext.Carrentals.AnyAsync<Carrental>(i => i.Carrentalid.Equals(carrentalid));
        }

        public async Task<bool> BookingExistsAsync(int bookingid)
        {
            return await dBContext.Bookings.AnyAsync<Booking>(i => i.Bookingid == bookingid);
        }


        public async Task<bool> CarExistsAsync(string carid)
        {
            return await dBContext.Cars.AnyAsync<Car>(i => i.Carid.Equals(carid));
        }
        public IEnumerable<Car> GetCarByCarRentalCompany(string carrentalid)
        {
            IEnumerable<Car> allCars = dBContext.Cars.ToList(); // Retrieve all cars from the database
            IEnumerable<Car> carsForRentalCompany = allCars.Where(car => car.Carrentalid.Equals(carrentalid));
            return carsForRentalCompany;
        }
        public async Task AddCarRentalCompany(Carrental carrental)
        {
            dBContext.Carrentals.Add(carrental);
            await dBContext.SaveChangesAsync();
        }
        public async Task<Car> GetCarById(string id)
        {
            IEnumerable<Car> allCars = dBContext.Cars.ToList();
            IEnumerable<Car> carsForRentalCompany = allCars.Where(car => car.Carid.Equals(id));
            return carsForRentalCompany.FirstOrDefault();
        }

        public void DeleteCar(Car car)
        {
            dBContext.Cars.Remove(car);
        }

        public IEnumerable<Car> GetCars()
        {
            IEnumerable<Car> allCars = dBContext.Cars.ToList();
            return allCars;
        }

        public IEnumerable<Booking> GetBookings()
        {
            IEnumerable<Booking> allBookings = dBContext.Bookings.ToList();
            return allBookings;
        }


        public IEnumerable<Carrental> GetCarRentalCompanies()
        {
            IEnumerable<Carrental> carrentals = dBContext.Carrentals;
            return carrentals;
        }

        public async Task<Carrental> GetCarRentalById(string id)
        {
            IEnumerable<Carrental> carrentals = dBContext.Carrentals.Where(c => c.Carrentalid.Equals(id));
            return carrentals.FirstOrDefault();
        }
        public async Task<Booking> GetBookingById(int id)
        {
            IEnumerable<Booking> booking = dBContext.Bookings.Where(c => c.Bookingid == id);
            return booking.FirstOrDefault();
        }
        public async Task AddBooking(Booking booking)
        {
            if (!await BookingExistsAsync(booking.Bookingid))
            {
                dBContext.Bookings.Add(booking);
                await dBContext.SaveChangesAsync();
            }
        }
        public async Task AddCarRental(Carrental carrental)
        {
            if (!await CarRentalCompanyExistAsync(carrental.Carrentalcompanyname))
            {
                dBContext.Carrentals.Add(carrental);
                await dBContext.SaveChangesAsync();
            }
        }
        public async Task AddCar(Car car)
        {
            if (!await CarExistsAsync(car.Carrentalid))
            {
                dBContext.Cars.Add(car);
                await dBContext.SaveChangesAsync();
            }
        }
        public void DeleteCarRental(Carrental carrental)
        {

            dBContext.Carrentals.Remove(carrental);

        }
        public void DeleteBooking(Booking booking)
        {

            dBContext.Bookings.Remove(booking);

        }
        public async Task<bool> SaveAsync()
        {
            return (await dBContext.SaveChangesAsync()) > 0;
        }

    }
}
