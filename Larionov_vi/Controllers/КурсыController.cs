using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Larionov_vi.Models;


namespace Larionov_vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class КурсыController : ControllerBase
    {
        private readonly VicourselaContext _context;

        public КурсыController(VicourselaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var курсы = _context.Курсыs.ToList();
            return Ok(курсы);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var курс = _context.Курсыs.FirstOrDefault(k => k.КодКурса == id);
            if (курс == null)
            {
                return NotFound(new { message = "Курс не найден" });
            }

            return Ok(курс);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Курсы курс)
        {
            if (курс == null || курс.КодПреподавателя <= 0)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            if (!_context.Пользователиs.Any(p => p.КодПользователя == курс.КодПреподавателя))
            {
                return BadRequest(new { message = "Преподаватель с указанным КодПреподавателя не существует" });
            }

            _context.Курсыs.Add(курс);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = курс.КодКурса }, курс);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Курсы обновленныйКурс)
        {
            if (обновленныйКурс == null || обновленныйКурс.КодКурса != id)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            var существующийКурс = _context.Курсыs.FirstOrDefault(k => k.КодКурса == id);
            if (существующийКурс == null)
            {
                return NotFound(new { message = "Курс не найден" });
            }

            существующийКурс.НазваниеКурса = обновленныйКурс.НазваниеКурса;
            существующийКурс.Описание = обновленныйКурс.Описание;
            существующийКурс.Кредиты = обновленныйКурс.Кредиты;
            существующийКурс.КодПреподавателя = обновленныйКурс.КодПреподавателя;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var курс = _context.Курсыs.FirstOrDefault(k => k.КодКурса == id);
            if (курс == null)
            {
                return NotFound(new { message = "Курс не найден" });
            }

            if (_context.Зачисленияs.Any(z => z.КодКурса == id) || _context.Занятияs.Any(z => z.КодКурса == id))
            {
                return Conflict(new { message = "Невозможно удалить курс, так как с ним связаны зачисления или занятия" });
            }

            _context.Курсыs.Remove(курс);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
