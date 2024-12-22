using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Larionov_vi.Models;

namespace Larionov_vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class СтудентыController : ControllerBase
    {
        private readonly VicourselaContext _context;

        public СтудентыController(VicourselaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var студенты = _context.Студентыs.ToList();
            return Ok(студенты);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var студент = _context.Студентыs.FirstOrDefault(s => s.КодСтудента == id);
            if (студент == null)
            {
                return NotFound(new { message = "Студент не найден" });
            }
            return Ok(студент);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Студенты студент)
        {
            if (студент == null || студент.КодПользователя <= 0 || студент.ГодПоступления <= 0 || студент.КодГруппы <= 0)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            // Проверка существования пользователя и группы
            if (!_context.Пользователиs.Any(p => p.КодПользователя == студент.КодПользователя))
            {
                return BadRequest(new { message = "Пользователь с таким КодПользователя не существует" });
            }

            if (!_context.Группыs.Any(g => g.КодГруппы == студент.КодГруппы))
            {
                return BadRequest(new { message = "Группа с таким КодГруппы не существует" });
            }

            _context.Студентыs.Add(студент);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = студент.КодСтудента }, студент);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Студенты обновленныйСтудент)
        {
            if (обновленныйСтудент == null || обновленныйСтудент.КодСтудента != id)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            var существующийСтудент = _context.Студентыs.FirstOrDefault(s => s.КодСтудента == id);
            if (существующийСтудент == null)
            {
                return NotFound(new { message = "Студент не найден" });
            }

            // Проверка существования пользователя и группы
            if (!_context.Пользователиs.Any(p => p.КодПользователя == обновленныйСтудент.КодПользователя))
            {
                return BadRequest(new { message = "Пользователь с таким КодПользователя не существует" });
            }

            if (!_context.Группыs.Any(g => g.КодГруппы == обновленныйСтудент.КодГруппы))
            {
                return BadRequest(new { message = "Группа с таким КодГруппы не существует" });
            }

            существующийСтудент.КодПользователя = обновленныйСтудент.КодПользователя;
            существующийСтудент.ГодПоступления = обновленныйСтудент.ГодПоступления;
            существующийСтудент.КодГруппы = обновленныйСтудент.КодГруппы;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var студент = _context.Студентыs.FirstOrDefault(s => s.КодСтудента == id);
            if (студент == null)
            {
                return NotFound(new { message = "Студент не найден" });
            }

            _context.Студентыs.Remove(студент);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
