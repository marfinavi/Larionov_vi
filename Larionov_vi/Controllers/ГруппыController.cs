using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Larionov_vi.Models;

namespace Larionov_vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ГруппыController : ControllerBase
    {
        private readonly VicourselaContext _context;

        public ГруппыController(VicourselaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var группы = _context.Группыs.ToList();
            return Ok(группы);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var группа = _context.Группыs.FirstOrDefault(g => g.КодГруппы == id);
            if (группа == null)
            {
                return NotFound(new { message = "Группа не найдена" });
            }
            
            return Ok(группа);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Группы группа)
        {
            if (группа == null || string.IsNullOrWhiteSpace(группа.НазваниеГруппы) || string.IsNullOrWhiteSpace(группа.Специализация))
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            if (_context.Группыs.Any(g => g.НазваниеГруппы == группа.НазваниеГруппы))
            {
                return Conflict(new { message = "Группа с таким названием уже существует" });
            }

            _context.Группыs.Add(группа);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = группа.КодГруппы }, группа);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Группы обновленнаяГруппа)
        {
            if (обновленнаяГруппа == null || обновленнаяГруппа.КодГруппы != id)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            var существующаяГруппа = _context.Группыs.FirstOrDefault(g => g.КодГруппы == id);
            if (существующаяГруппа == null)
            {
                return NotFound(new { message = "Группа не найдена" });
            }

            существующаяГруппа.НазваниеГруппы = обновленнаяГруппа.НазваниеГруппы;
            существующаяГруппа.Специализация = обновленнаяГруппа.Специализация;
            существующаяГруппа.ГодКурса = обновленнаяГруппа.ГодКурса;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var группа = _context.Группыs.FirstOrDefault(g => g.КодГруппы == id);
            if (группа == null)
            {
                return NotFound(new { message = "Группа не найдена" });
            }

            if (_context.Студентыs.Any(s => s.КодГруппы == id))
            {
                return Conflict(new { message = "Невозможно удалить группу, так как с ней связаны студенты" });
            }

            _context.Группыs.Remove(группа);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
