using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Larionov_vi.Models;

namespace Larionov_vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ПрограммыКурсовController : ControllerBase
    {
        private readonly VicourselaContext _context;

        public ПрограммыКурсовController(VicourselaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var программыКурсов = _context.ПрограммыКурсовs.ToList();
            return Ok(программыКурсов);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var программаКурса = _context.ПрограммыКурсовs.FirstOrDefault(p => p.КодПрограммыКурса == id);
            if (программаКурса == null)
            {
                return NotFound(new { message = "Запись программы курсов не найдена" });
            }

            return Ok(программаКурса);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ПрограммыКурсов программаКурса)
        {
            if (программаКурса == null)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            if (!_context.Курсыs.Any(k => k.КодКурса == программаКурса.КодКурса))
            {
                return BadRequest(new { message = "Курс с указанным КодКурса не существует" });
            }

            if (!_context.Программыs.Any(p => p.КодПрограммы == программаКурса.КодПрограммы))
            {
                return BadRequest(new { message = "Программа с указанным КодПрограммы не существует" });
            }

            _context.ПрограммыКурсовs.Add(программаКурса);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = программаКурса.КодПрограммыКурса }, программаКурса);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ПрограммыКурсов обновленнаяПрограммаКурса)
        {
            if (обновленнаяПрограммаКурса == null || обновленнаяПрограммаКурса.КодПрограммыКурса != id)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            var существующаяПрограммаКурса = _context.ПрограммыКурсовs.FirstOrDefault(p => p.КодПрограммыКурса == id);
            if (существующаяПрограммаКурса == null)
            {
                return NotFound(new { message = "Запись программы курсов не найдена" });
            }

            существующаяПрограммаКурса.КодКурса = обновленнаяПрограммаКурса.КодКурса;
            существующаяПрограммаКурса.КодПрограммы = обновленнаяПрограммаКурса.КодПрограммы;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var программаКурса = _context.ПрограммыКурсовs.FirstOrDefault(p => p.КодПрограммыКурса == id);
            if (программаКурса == null)
            {
                return NotFound(new { message = "Запись программы курсов не найдена" });
            }

            _context.ПрограммыКурсовs.Remove(программаКурса);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
