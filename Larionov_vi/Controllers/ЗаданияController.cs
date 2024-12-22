using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Larionov_vi.Models;

namespace Larionov_vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ЗаданияController : ControllerBase
    {
        private readonly VicourselaContext _context;

        public ЗаданияController(VicourselaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var задания = _context.Заданияs.ToList();
            return Ok(задания);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var задание = _context.Заданияs.FirstOrDefault(z => z.КодЗадания == id);
            if (задание == null)
            {
                return NotFound(new { message = "Задание не найдено" });
            }

            return Ok(задание);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Задания задание)
        {
            if (задание == null || string.IsNullOrWhiteSpace(задание.Название) || задание.КодКурса <= 0)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            if (!_context.Курсыs.Any(k => k.КодКурса == задание.КодКурса))
            {
                return BadRequest(new { message = "Курс с указанным КодКурса не существует" });
            }

            _context.Заданияs.Add(задание);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = задание.КодЗадания }, задание);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Задания обновленноеЗадание)
        {
            if (обновленноеЗадание == null || обновленноеЗадание.КодЗадания != id)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            var существующееЗадание = _context.Заданияs.FirstOrDefault(z => z.КодЗадания == id);
            if (существующееЗадание == null)
            {
                return NotFound(new { message = "Задание не найдено" });
            }

            существующееЗадание.Название = обновленноеЗадание.Название;
            существующееЗадание.Описание = обновленноеЗадание.Описание;
            существующееЗадание.ДатаСдачи = обновленноеЗадание.ДатаСдачи;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var задание = _context.Заданияs.FirstOrDefault(z => z.КодЗадания == id);
            if (задание == null)
            {
                return NotFound(new { message = "Задание не найдено" });
            }

            if (_context.Работыs.Any(r => r.КодЗадания == id))
            {
                return Conflict(new { message = "Невозможно удалить задание, так как с ним связаны работы студентов" });
            }

            _context.Заданияs.Remove(задание);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
