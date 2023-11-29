using CarRentalLibrary.Models;

namespace abraham_luzon_group6_assignment.Services
{
    public interface ICarRentalRepository
    {
        IEnumerable<Car> GetCarByCarRentalCompany(string carrentalid);
        IEnumerable<Car> GetCars();
        Task<bool> CarRentalCompanyExistAsync(string carrentalid);
        Task AddCarRentalCompany(Carrental carrental);
        Task<Car> GetCarById(string id);
        void DeleteCarRental (Carrental carrental);

        //Ingredient Related queries
        IEnumerable<Carrental> GetCarRentalCompanies();
        Task<Carrental> GetCarRentalById(string id);
        Task<bool> CarExistsAsync(string carrid);
        Task AddCar(Car newCar);
        void DeleteCar(Car car);


        Task<bool> SaveAsync();
    }
}
