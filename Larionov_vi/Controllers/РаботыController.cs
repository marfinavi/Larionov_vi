using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Larionov_vi.Models;


namespace Larionov_vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class РаботыController : ControllerBase
    {
        private readonly VicourselaContext _context;

        public РаботыController(VicourselaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var работы = _context.Работыs.ToList();
            return Ok(работы);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var работа = _context.Работыs.FirstOrDefault(r => r.КодРаботы == id);
            if (работа == null)
            {
                return NotFound(new { message = "Работа не найдена" });
            }

            return Ok(работа);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Работы работа)
        {
            if (работа == null)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            if (!_context.Заданияs.Any(z => z.КодЗадания == работа.КодЗадания))
            {
                return BadRequest(new { message = "Задание с указанным КодЗадания не существует" });
            }

            if (!_context.Студентыs.Any(s => s.КодСтудента == работа.КодСтудента))
            {
                return BadRequest(new { message = "Студент с указанным КодСтудента не существует" });
            }

            _context.Работыs.Add(работа);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = работа.КодРаботы }, работа);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Работы обновленнаяРабота)
        {
            if (обновленнаяРабота == null || обновленнаяРабота.КодРаботы != id)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            var существующаяРабота = _context.Работыs.FirstOrDefault(r => r.КодРаботы == id);
            if (существующаяРабота == null)
            {
                return NotFound(new { message = "Работа не найдена" });
            }

            существующаяРабота.КодЗадания = обновленнаяРабота.КодЗадания;
            существующаяРабота.КодСтудента = обновленнаяРабота.КодСтудента;
            существующаяРабота.ДатаСдачи = обновленнаяРабота.ДатаСдачи;
            существующаяРабота.Оценка = обновленнаяРабота.Оценка;
            существующаяРабота.Комментарии = обновленнаяРабота.Комментарии;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var работа = _context.Работыs.FirstOrDefault(r => r.КодРаботы == id);
            if (работа == null)
            {
                return NotFound(new { message = "Работа не найдена" });
            }

            _context.Работыs.Remove(работа);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
