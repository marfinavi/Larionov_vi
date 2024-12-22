using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Larionov_vi.Models;


namespace Larionov_vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ЗанятияController : ControllerBase
    {
        private readonly VicourselaContext _context;

        public ЗанятияController(VicourselaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var занятия = _context.Занятияs.ToList();
            return Ok(занятия);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var занятие = _context.Занятияs.FirstOrDefault(z => z.КодЗанятия == id);
            if (занятие == null)
            {
                return NotFound(new { message = "Занятие не найдено" });
            }

            return Ok(занятие);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Занятия занятие)
        {
            if (занятие == null || занятие.КодКурса <= 0)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            if (!_context.Курсыs.Any(k => k.КодКурса == занятие.КодКурса))
            {
                return BadRequest(new { message = "Курс с указанным КодКурса не существует" });
            }

            _context.Занятияs.Add(занятие);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = занятие.КодЗанятия }, занятие);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Занятия обновленноеЗанятие)
        {
            if (обновленноеЗанятие == null || обновленноеЗанятие.КодЗанятия != id)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            var существующееЗанятие = _context.Занятияs.FirstOrDefault(z => z.КодЗанятия == id);
            if (существующееЗанятие == null)
            {
                return NotFound(new { message = "Занятие не найдено" });
            }

            существующееЗанятие.ДатаЗанятия = обновленноеЗанятие.ДатаЗанятия;
            существующееЗанятие.ВремяНачала = обновленноеЗанятие.ВремяНачала;
            существующееЗанятие.ВремяОкончания = обновленноеЗанятие.ВремяОкончания;
            существующееЗанятие.Аудитория = обновленноеЗанятие.Аудитория;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var занятие = _context.Занятияs.FirstOrDefault(z => z.КодЗанятия == id);
            if (занятие == null)
            {
                return NotFound(new { message = "Занятие не найдено" });
            }

            if (_context.Посещаемостьs.Any(p => p.КодЗанятия == id))
            {
                return Conflict(new { message = "Невозможно удалить занятие, так как с ним связаны записи посещаемости" });
            }

            _context.Занятияs.Remove(занятие);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
