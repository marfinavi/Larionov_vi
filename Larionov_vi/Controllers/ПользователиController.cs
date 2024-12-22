using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Larionov_vi.Models;

namespace Larionov_vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ПользователиController : ControllerBase
    {
        private readonly VicourselaContext _context;

        public ПользователиController(VicourselaContext context)
        {
            _context = context;
        }

        // GET: api/Пользователи
        [HttpGet]
        public IActionResult GetAll()
        {
            var пользователи = _context.Пользователиs
                .Select(u => new
                {
                    u.КодПользователя,
                    u.Имя,
                    u.Фамилия,
                    u.ЭлектроннаяПочта,
                    u.КодРоли
                })
                .ToList();

            return Ok(пользователи);
        }

        // GET: api/Пользователи/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var пользователь = _context.Пользователиs
                .Where(u => u.КодПользователя == id)
                .Select(u => new
                {
                    u.КодПользователя,
                    u.Имя,
                    u.Фамилия,
                    u.ЭлектроннаяПочта,
                    u.КодРоли
                })
                .FirstOrDefault();

            if (пользователь == null)
            {
                return NotFound(new { message = "Пользователь не найден" });
            }

            return Ok(пользователь);
        }

        // POST: api/Пользователи
        [HttpPost]
        public IActionResult Create([FromBody] Пользователи пользователь)
        {
            if (пользователь == null || string.IsNullOrWhiteSpace(пользователь.Имя) || string.IsNullOrWhiteSpace(пользователь.Фамилия) ||
                string.IsNullOrWhiteSpace(пользователь.ЭлектроннаяПочта) || string.IsNullOrWhiteSpace(пользователь.ХэшПароля) ||
                пользователь.КодРоли <= 0)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            if (!_context.Ролиs.Any(r => r.КодРоли == пользователь.КодРоли))
            {
                return BadRequest(new { message = "Роль с таким КодРоли не существует" });
            }

            if (_context.Пользователиs.Any(u => u.ЭлектроннаяПочта == пользователь.ЭлектроннаяПочта))
            {
                return Conflict(new { message = "Пользователь с такой электронной почтой уже существует" });
            }

            _context.Пользователиs.Add(пользователь);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = пользователь.КодПользователя }, пользователь);
        }

        // PUT: api/Пользователи/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Пользователи обновленныйПользователь)
        {
            if (обновленныйПользователь == null || обновленныйПользователь.КодПользователя != id)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            var существующийПользователь = _context.Пользователиs.FirstOrDefault(u => u.КодПользователя == id);
            if (существующийПользователь == null)
            {
                return NotFound(new { message = "Пользователь не найден" });
            }

            существующийПользователь.Имя = обновленныйПользователь.Имя;
            существующийПользователь.Фамилия = обновленныйПользователь.Фамилия;
            существующийПользователь.ЭлектроннаяПочта = обновленныйПользователь.ЭлектроннаяПочта;
            существующийПользователь.ХэшПароля = обновленныйПользователь.ХэшПароля;
            существующийПользователь.КодРоли = обновленныйПользователь.КодРоли;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Пользователи/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var пользователь = _context.Пользователиs.FirstOrDefault(u => u.КодПользователя == id);
            if (пользователь == null)
            {
                return NotFound(new { message = "Пользователь не найден" });
            }

            // Проверяем зависимые записи
            if (_context.Студентыs.Any(s => s.КодПользователя == id) || _context.УчастникиСобытийs.Any(us => us.КодПользователя == id))
            {
                return Conflict(new { message = "Невозможно удалить пользователя, так как с ним связаны другие записи" });
            }

            _context.Пользователиs.Remove(пользователь);
            _context.SaveChanges();
            return NoContent();
        }
    }
}