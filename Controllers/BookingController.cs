using AutoMapper;
using CarRentalLibrary.Models;
using abraham_luzon_group6_assignment.DTOs;
using abraham_luzon_group6_assignment.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace abraham_luzon_group6_assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private ICarRentalRepository carRentalRepository;
        private readonly IMapper mapper;

        //Constructor
        public BookingController(ICarRentalRepository carRentalRepository, IMapper mapper)
        {
            this.carRentalRepository = carRentalRepository;
            this.mapper = mapper;
        }

        // GET: api/<CarRentalController>
        //Get all CarRentalCompany
        [HttpGet]
        [Route("/api/bookings/")]
        public IActionResult GetAllBookings()
        {
            var bookings = carRentalRepository.GetBookings();
            var results = mapper.Map<IEnumerable<BookingDto>>(bookings);
            return Ok(results);
        }



        // POST api/<CarRentalController>  
        //Add car rental
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] BookingDto bookingDto)
        {
            if (bookingDto == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var booking = mapper.Map<Booking>(bookingDto);
            await carRentalRepository.AddBooking(booking);
            var viewBooking = mapper.Map<BookingDto>(booking);
            return Created("Booking Added!", viewBooking);
        }
        [HttpGet("{bookingid}")]
        public IActionResult GetBookingById(int bookingid)
        {
            var booking = carRentalRepository.GetBookingById(bookingid);
            var results = mapper.Map<BookingDto>(booking.Result);
            return Ok(results);
        }
        // PUT api/<CarRentalController>  
        //update car rental company
        [HttpPut("{bookingid}")]
        public async Task<IActionResult> PUT(int bookingid, [FromBody] BookingDto bookingDto)
        {
            if (bookingDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await carRentalRepository.BookingExistsAsync(bookingid))
            {
                return NotFound();
            }
            var oldBooking = carRentalRepository.GetBookingById(bookingid);
            if (oldBooking.Result == null)
            {
                return NotFound();
            }
            mapper.Map(bookingDto, oldBooking.Result);
            if (!await carRentalRepository.SaveAsync())
            {
                return StatusCode(500, "A problem happened while handling your request");

            }
            return NoContent();
        }

        // DELETE api/<>  
        //delete 
        [HttpDelete("{bookingid}")]
        public async Task<ActionResult> DeleteBooking(int bookingid)
        {
            if (!await carRentalRepository.BookingExistsAsync(bookingid))
            {
                return NotFound();
            }
            Booking bookingToDelete = carRentalRepository.GetBookingById(bookingid).Result;
            if (bookingToDelete == null)
            {
                return NotFound();
            }

            carRentalRepository.DeleteBooking(bookingToDelete);
            if (!await carRentalRepository.SaveAsync())
            {
                return StatusCode(500, "Deletion Failed!");

            }
            return NoContent();

        }

        // PATCH api/<>  
        //Paritally update 
        [HttpPatch("{bookingid}")]
        public async Task<ActionResult> ParitiallyUpdateBooking(int bookingid, JsonPatchDocument<BookingDto> patchDocument)
        {
            if (!await carRentalRepository.BookingExistsAsync(bookingid))
            {
                return NotFound();
            }

            Booking booking = await carRentalRepository.GetBookingById(bookingid);
            if (booking == null)
            {
                return NotFound();
            }

            var bookingDto = mapper.Map<BookingDto>(booking);
            patchDocument.ApplyTo(bookingDto, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(bookingDto))
            {
                return BadRequest(ModelState);
            }

            // Apply changes back to the entity before saving
            mapper.Map(bookingDto, booking);

            await carRentalRepository.SaveAsync();
            return NoContent();
        }
    }
}
