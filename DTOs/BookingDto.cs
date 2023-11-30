namespace abraham_luzon_group6_assignment.DTOs
{
    public class BookingDto
    {
        public int Bookingid { get; set; }
        public string? Carid { get; set; }
        public string? Carrentalcompanyid { get; set; }
        public string? Customername { get; set; }
        public int? Numberofpeople { get; set; }
        public int? Luggagespace { get; set; }
        public string? InsuranceNeeded { get; set; }
        public DateTime? Bookingdate { get; set; }
    }
}