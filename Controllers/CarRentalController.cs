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
    public class CarRentalController : ControllerBase
    {
        private ICarRentalRepository carRentalRepository;
        private readonly IMapper mapper;

        //Constructor
        public CarRentalController(ICarRentalRepository carRentalRepository, IMapper mapper)
        {
            this.carRentalRepository = carRentalRepository;
            this.mapper = mapper;
        }

        // GET: api/<CarRentalController>
        //Get all CarRentalCompany
        [HttpGet]
        [Route("/api/carrentals/")]
        public IActionResult GetAllCarRentals()
        {
            var carrentalcompanies = carRentalRepository.GetCarRentalCompanies();
            var results = mapper.Map<IEnumerable<CarRentalDto>>(carrentalcompanies);
            return Ok(results);
        }



        // POST api/<CarRentalController>  
        //Add car rental
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CarRentalDto carRentalDto)
        {
            if (carRentalDto == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid) { 
            return BadRequest(); }

            var company = mapper.Map<Carrental>(carRentalDto);
            await carRentalRepository.AddCarRentalCompany(company);
            var viewCarRental = mapper.Map<CarRentalDto>(company);
            return Created("Car Rental Company Added!", viewCarRental);
        }
        [HttpGet("{carrentalid}")]
        public IActionResult GetCarRentalById(string carrentalid)
        {
            var carrental = carRentalRepository.GetCarRentalById(carrentalid);
            var results = mapper.Map<CarRentalDto>(carrental.Result);
            return Ok(results);
        }

        // PUT api/<CarRentalController>  
        //update car rental company
        [HttpPut("{carrentalid}")]
        public async Task<IActionResult> PUT(string carrentalid, [FromBody] CarRentalDto carRentalDto)
        {
            if (carRentalDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await carRentalRepository.CarRentalCompanyExistAsync(carrentalid))
            {
                return NotFound();
            }
            var oldcarrental = carRentalRepository.GetCarRentalById(carrentalid);
            if (oldcarrental.Result == null)
            {
                return NotFound();
            }
            mapper.Map(carRentalDto, oldcarrental.Result);
            if (!await carRentalRepository.SaveAsync())
            {
                return StatusCode(500, "A problem happened while handling your request");

            }
            return NoContent();
        }

        // DELETE api/<>  
        //delete 
        [HttpDelete("{carrentalid}")]
        public async Task<ActionResult> DeleteCarRentalCompany(string carrentalid)
        {
            if (!await carRentalRepository.CarRentalCompanyExistAsync(carrentalid))
            {
                return NotFound();
            }
            Carrental carrentalToDelete = carRentalRepository.GetCarRentalById(carrentalid).Result;
            if (carrentalToDelete == null)
            {
                return NotFound();
            }

            carRentalRepository.DeleteCarRental(carrentalToDelete);
            if (!await carRentalRepository.SaveAsync())
            {
                return StatusCode(500, "Deletion Failed!");

            }
            return NoContent();

        }

        // PATCH api/<>  
        //Paritally update 
        [HttpPatch("{carrentalid}")]
        public async Task<ActionResult> ParitiallyUpdateCarRentalCompany(string carrentalid, JsonPatchDocument<CarRentalDto> patchDocument)
        {
            if (!await carRentalRepository.CarRentalCompanyExistAsync(carrentalid))
            {
                return NotFound();
            }

            Carrental carrental = await carRentalRepository.GetCarRentalById(carrentalid);
            if (carrental == null)
            {
                return NotFound();
            }

            var carRentalDto = mapper.Map<CarRentalDto>(carrental);
            patchDocument.ApplyTo(carRentalDto, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(carRentalDto))
            {
                return BadRequest(ModelState);
            }

            // Apply changes back to the entity before saving
            mapper.Map(carRentalDto, carrental);

            await carRentalRepository.SaveAsync();
            return NoContent();
        }
    }
}
