using CarAPI.Models;
using CarAPI.Models.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarContext _context;

        public CarController(CarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCars()
        {
            return await _context.Cars
                .Select(x => TodoCarDTO(x))
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CarDTO>> PostCars(CarDTO carsDTO)
        {
            CarValidators validador = new CarValidators();

            validador.ValidateAndThrow(carsDTO);

            var cars = new Cars
            {
                Name = carsDTO.Name,
                Brand = carsDTO.Brand,
                Color = carsDTO.Color,
                YearManufacture = carsDTO.YearManufacture
            };

            _context.Cars.Add(cars);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCars), new { id = cars.Id }, TodoCarDTO(cars));

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDTO>> GetCarsId(long id)
        {
            var cars = await _context.Cars.FindAsync(id);

            if (cars == null)
            {
                return NotFound();
            }
            return TodoCarDTO(cars);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCars(long id, CarDTO carDTO)
        {
            if (id != carDTO.Id)
            {
                return BadRequest();
            }
            var cars = await _context.Cars.FindAsync(id);
            if (cars == null)
            {
                return NotFound();
            }
            cars.Name = carDTO.Name;
            cars.Brand = carDTO.Brand;
            cars.YearManufacture = carDTO.YearManufacture;
            cars.Color = carDTO.Color;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CarExistis(id))
            {

                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(long id)
        {
            var cars = await _context.Cars.FindAsync(id);
            if (cars == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(cars);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExistis(long id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
        private static CarDTO TodoCarDTO(Cars cars)
            => new CarDTO
            {

                Id = cars.Id,
                Name = cars.Name,
                Brand = cars.Brand,
                YearManufacture = cars.YearManufacture,
                Color = cars.Color
            };
    }
}
