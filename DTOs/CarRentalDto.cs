using CarRentalLibrary.Models;
namespace abraham_luzon_group6_assignment.DTOs
{
    public class CarRentalDto
    {
       
            public string? Carrentalid { get; set; }
            public string? Carrentalcompanyname { get; set; }
            public string? Location { get; set; }
            public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
        
    }
}
