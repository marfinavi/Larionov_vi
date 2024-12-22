using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Larionov_vi.Models;

namespace Larionov_vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class СобытияController : ControllerBase
    {
        private readonly VicourselaContext _context;

        public СобытияController(VicourselaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var события = _context.Событияs.ToList();
            return Ok(события);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var событие = _context.Событияs.FirstOrDefault(s => s.КодСобытия == id);
            if (событие == null)
            {
                return NotFound(new { message = "Событие не найдено" });
            }

            return Ok(событие);
        }

        [HttpPost]
        public IActionResult Create([FromBody] События событие)
        {
            if (событие == null)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            _context.Событияs.Add(событие);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = событие.КодСобытия }, событие);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] События обновленноеСобытие)
        {
            if (обновленноеСобытие == null || обновленноеСобытие.КодСобытия != id)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            var существующееСобытие = _context.Событияs.FirstOrDefault(s => s.КодСобытия == id);
            if (существующееСобытие == null)
            {
                return NotFound(new { message = "Событие не найдено" });
            }

            существующееСобытие.НазваниеСобытия = обновленноеСобытие.НазваниеСобытия;
            существующееСобытие.ДатаСобытия = обновленноеСобытие.ДатаСобытия;
            существующееСобытие.МестоПроведения = обновленноеСобытие.МестоПроведения;
            существующееСобытие.Описание = обновленноеСобытие.Описание;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var событие = _context.Событияs.FirstOrDefault(s => s.КодСобытия == id);
            if (событие == null)
            {
                return NotFound(new { message = "Событие не найдено" });
            }

            _context.Событияs.Remove(событие);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
