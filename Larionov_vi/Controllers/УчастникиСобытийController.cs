using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Larionov_vi.Models;

namespace Larionov_vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class УчастникиСобытийController : ControllerBase
    {
        private readonly VicourselaContext _context;

        public УчастникиСобытийController(VicourselaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var участники = _context.УчастникиСобытийs.ToList();
            return Ok(участники);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var участник = _context.УчастникиСобытийs.FirstOrDefault(u => u.КодУчастника == id);
            if (участник == null)
            {
                return NotFound(new { message = "Участник не найден" });
            }

            return Ok(участник);
        }

        [HttpPost]
        public IActionResult Create([FromBody] УчастникиСобытий участник)
        {
            if (участник == null)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            if (!_context.Пользователиs.Any(u => u.КодПользователя == участник.КодПользователя))
            {
                return BadRequest(new { message = "Пользователь с указанным КодПользователя не существует" });
            }

            if (!_context.Событияs.Any(s => s.КодСобытия == участник.КодСобытия))
            {
                return BadRequest(new { message = "Событие с указанным КодСобытия не существует" });
            }

            _context.УчастникиСобытийs.Add(участник);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = участник.КодУчастника }, участник);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] УчастникиСобытий обновленныйУчастник)
        {
            if (обновленныйУчастник == null || обновленныйУчастник.КодУчастника != id)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            var существующийУчастник = _context.УчастникиСобытийs.FirstOrDefault(u => u.КодУчастника == id);
            if (существующийУчастник == null)
            {
                return NotFound(new { message = "Участник не найден" });
            }

            существующийУчастник.КодПользователя = обновленныйУчастник.КодПользователя;
            существующийУчастник.КодСобытия = обновленныйУчастник.КодСобытия;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var участник = _context.УчастникиСобытийs.FirstOrDefault(u => u.КодУчастника == id);
            if (участник == null)
            {
                return NotFound(new { message = "Участник не найден" });
            }

            _context.УчастникиСобытийs.Remove(участник);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
