using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Larionov_vi.Models;

namespace Larionov_vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ЗачисленияController : ControllerBase
    {
        private readonly VicourselaContext _context;

        public ЗачисленияController(VicourselaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var зачисления = _context.Зачисленияs.ToList();
            return Ok(зачисления);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var зачисление = _context.Зачисленияs.FirstOrDefault(z => z.КодЗачисления == id);
            if (зачисление == null)
            {
                return NotFound(new { message = "Зачисление не найдено" });
            }

            return Ok(зачисление);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Зачисления зачисление)
        {
            if (зачисление == null || зачисление.КодСтудента <= 0 || зачисление.КодКурса <= 0)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            if (!_context.Студентыs.Any(s => s.КодСтудента == зачисление.КодСтудента))
            {
                return BadRequest(new { message = "Студент с указанным КодСтудента не существует" });
            }

            if (!_context.Курсыs.Any(k => k.КодКурса == зачисление.КодКурса))
            {
                return BadRequest(new { message = "Курс с указанным КодКурса не существует" });
            }

            _context.Зачисленияs.Add(зачисление);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = зачисление.КодЗачисления }, зачисление);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Зачисления обновленноеЗачисление)
        {
            if (обновленноеЗачисление == null || обновленноеЗачисление.КодЗачисления != id)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            var существующееЗачисление = _context.Зачисленияs.FirstOrDefault(z => z.КодЗачисления == id);
            if (существующееЗачисление == null)
            {
                return NotFound(new { message = "Зачисление не найдено" });
            }

            существующееЗачисление.КодСтудента = обновленноеЗачисление.КодСтудента;
            существующееЗачисление.КодКурса = обновленноеЗачисление.КодКурса;
            существующееЗачисление.ДатаЗачисления = обновленноеЗачисление.ДатаЗачисления;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var зачисление = _context.Зачисленияs.FirstOrDefault(z => z.КодЗачисления == id);
            if (зачисление == null)
            {
                return NotFound(new { message = "Зачисление не найдено" });
            }

            _context.Зачисленияs.Remove(зачисление);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
