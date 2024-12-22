using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Larionov_vi.Models;

namespace Larionov_vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ФакультетыController : ControllerBase
    {
        private readonly VicourselaContext _context;

        public ФакультетыController(VicourselaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var факультеты = _context.Факультетыs.ToList();
            return Ok(факультеты);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var факультет = _context.Факультетыs.FirstOrDefault(f => f.КодФакультета == id);
            if (факультет == null)
            {
                return NotFound(new { message = "Факультет не найден" });
            }

            return Ok(факультет);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Факультеты факультет)
        {
            if (факультет == null)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            _context.Факультетыs.Add(факультет);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = факультет.КодФакультета }, факультет);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Факультеты обновленныйФакультет)
        {
            if (обновленныйФакультет == null || обновленныйФакультет.КодФакультета != id)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            var существующийФакультет = _context.Факультетыs.FirstOrDefault(f => f.КодФакультета == id);
            if (существующийФакультет == null)
            {
                return NotFound(new { message = "Факультет не найден" });
            }

            существующийФакультет.НазваниеФакультета = обновленныйФакультет.НазваниеФакультета;
            существующийФакультет.Декан = обновленныйФакультет.Декан;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var факультет = _context.Факультетыs.FirstOrDefault(f => f.КодФакультета == id);
            if (факультет == null)
            {
                return NotFound(new { message = "Факультет не найден" });
            }

            _context.Факультетыs.Remove(факультет);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
