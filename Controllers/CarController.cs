using abraham_luzon_group6_assignment.DTOs;
using abraham_luzon_group6_assignment.Services;
using AutoMapper;
using CarRentalLibrary.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace abraham_luzon_group6_assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private ICarRentalRepository carRentalRepository;
        private readonly IMapper mapper;

        //Constructor
        public CarController(ICarRentalRepository carRentalRepository, IMapper mapper)
        {
            this.carRentalRepository = carRentalRepository;
            this.mapper = mapper;
        }

        // GET: api/<CarRentalController>
        //Get all CarRentalCompany
        [HttpGet]
        [Route("/api/cars/")]
        public IActionResult GetAllCars()
        {
            var cars = carRentalRepository.GetCars();
            var results = mapper.Map<IEnumerable<CarDto>>(cars);
            return Ok(results);
        }



        // POST api/<CarRentalController>  
        //Add car rental
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CarDto carDto)
        {
            if (carDto == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var car = mapper.Map<Car>(carDto);
            await carRentalRepository.AddCar(car);
            var viewCar = mapper.Map<CarDto>(car);
            return Created("Car  Added!", viewCar);
        }

        // PUT api/<CarRentalController>  
        //update car rental company
        [HttpPut("{carid}")]
        public async Task<IActionResult> PUT(string carid, [FromBody] CarDto carDto)
        {
            if (carDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await carRentalRepository.CarExistsAsync(carid))
            {
                return NotFound();
            }
            var oldcar = carRentalRepository.GetCarById(carid);
            if (oldcar.Result == null)
            {
                return NotFound();
            }
            mapper.Map(carDto, oldcar.Result);
            if (!await carRentalRepository.SaveAsync())
            {
                return StatusCode(500, "A problem happened while handling your request");

            }
            return NoContent();
        }

        // DELETE api/<IngredientController>  
        //delete ingredient
        [HttpDelete("{carid}")]
        public async Task<ActionResult> DeleteCar(string carid)
        {
            if (!await carRentalRepository.CarExistsAsync(carid))
            {
                return NotFound();
            }
            Car carToDelete = carRentalRepository.GetCarById(carid).Result;
            if (carToDelete == null)
            {
                return NotFound();
            }

            carRentalRepository.DeleteCar(carToDelete);
            if (!await carRentalRepository.SaveAsync())
            {
                return StatusCode(500, "Deletion Failed!");

            }
            return NoContent();

        }

        // PATCH api/<IngredientController>  
        //Paritally update Ingredient
        [HttpPatch("{carid}")]
        public async Task<ActionResult> ParitiallyUpdateCar(string carid, JsonPatchDocument<CarDto> patchDocument)
        {
            if (!await carRentalRepository.CarExistsAsync(carid))
            {
                return NotFound();
            }

            Car car = await carRentalRepository.GetCarById(carid);
            if (car == null)
            {
                return NotFound();
            }

            var carDto = mapper.Map<CarDto>(car);
            patchDocument.ApplyTo(carDto, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(carDto))
            {
                return BadRequest(ModelState);
            }

            // Apply changes back to the entity before saving
            mapper.Map(carDto, car);

            await carRentalRepository.SaveAsync();
            return NoContent();
        }
    }
}
