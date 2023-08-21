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
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCar()
        {
            return await _context.Cars
                .Select(x => TodoCarDTO(x))
                .ToListAsync();
        }

        private static CarDTO TodoCarDTO(Cars cars) => new CarDTO
        {
            Id = cars.Id,
            Name = cars.Name,
            Brand = cars.Brand,
            YearManufacture = cars.YearManufacture,
            Color = cars.Color
        };
    }
}
