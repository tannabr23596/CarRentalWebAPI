namespace abraham_luzon_group6_assignment.DTOs
{
        public class CarDto
        {
            public string? Carid { get; set; }
            public string? Carmodel { get; set; }
            public decimal? Insuranceamount { get; set; }
            public string? Fueltype { get; set; }
            public string? Mileage { get; set; }
            public int? Noofdoors { get; set; }
            public decimal? Rentalpriceperday { get; set; }
            public string? Luggagespace { get; set; }
            public bool? Freecancelation { get; set; }
            public string? Geartype { get; set; }
            public string? Caravailability { get; set; }

            public string? Carrentalid { get; set; } // Foreign key representation

            //public virtual CarRentalDto CarRental { get; set; } = null!;

        }
    
}
