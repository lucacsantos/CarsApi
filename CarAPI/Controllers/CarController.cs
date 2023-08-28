using CarAPI.Models;
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
        [HttpGet("id")]
        public async Task<ActionResult<CarDTO>> GetCarsId(long id)
        {
            var cars = await _context.Cars.FindAsync(id);
            
            if (cars == null)
            {
                return NotFound();
            }
            return TodoCarDTO(cars);
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
