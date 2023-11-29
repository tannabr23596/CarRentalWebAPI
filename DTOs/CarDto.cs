namespace abraham_luzon_group6_assignment.DTOs
{
    public class CarDto
    {
        public class CarDTO
        {
            public string CarId { get; set; }
            public string? CarModel { get; set; }
            public decimal? InsuranceAmount { get; set; }
            public string? FuelType { get; set; }
            public string? Mileage { get; set; }
            public int? NumberOfDoors { get; set; }
            public decimal? RentalPricePerDay { get; set; }
            public string? LuggageSpace { get; set; }
            public string? FreeCancellation { get; set; }
            public string? GearType { get; set; }
            public string? CarRentalId { get; set; } // Foreign key representation

            public virtual CarRentalDto CarRental { get; set; } = null!;

        }
    }
}
