using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Larionov_vi.Models;

namespace Larionov_vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ПосещаемостьController : ControllerBase
    {
        private readonly VicourselaContext _context;

        public ПосещаемостьController(VicourselaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var посещаемость = _context.Посещаемостьs.ToList();
            return Ok(посещаемость);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var запись = _context.Посещаемостьs.FirstOrDefault(p => p.КодПосещаемости == id);
            if (запись == null)
            {
                return NotFound(new { message = "Запись посещаемости не найдена" });
            }

            return Ok(запись);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Посещаемость посещение)
        {
            if (посещение == null)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            if (!_context.Занятияs.Any(z => z.КодЗанятия == посещение.КодЗанятия))
            {
                return BadRequest(new { message = "Занятие с указанным КодЗанятия не существует" });
            }

            if (!_context.Студентыs.Any(s => s.КодСтудента == посещение.КодСтудента))
            {
                return BadRequest(new { message = "Студент с указанным КодСтудента не существует" });
            }

            _context.Посещаемостьs.Add(посещение);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = посещение.КодПосещаемости }, посещение);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Посещаемость обновленноеПосещение)
        {
            if (обновленноеПосещение == null || обновленноеПосещение.КодПосещаемости != id)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            var существующееПосещение = _context.Посещаемостьs.FirstOrDefault(p => p.КодПосещаемости == id);
            if (существующееПосещение == null)
            {
                return NotFound(new { message = "Запись посещаемости не найдена" });
            }

            существующееПосещение.КодЗанятия = обновленноеПосещение.КодЗанятия;
            существующееПосещение.КодСтудента = обновленноеПосещение.КодСтудента;
            существующееПосещение.СтатусПосещаемости = обновленноеПосещение.СтатусПосещаемости;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var запись = _context.Посещаемостьs.FirstOrDefault(p => p.КодПосещаемости == id);
            if (запись == null)
            {
                return NotFound(new { message = "Запись посещаемости не найдена" });
            }

            _context.Посещаемостьs.Remove(запись);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
