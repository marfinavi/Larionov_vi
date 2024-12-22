using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Larionov_vi.Models;


namespace Larionov_vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ПрограммыController : ControllerBase
    {
        private readonly VicourselaContext _context;

        public ПрограммыController(VicourselaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var программы = _context.Программыs.ToList();
            return Ok(программы);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var программа = _context.Программыs.FirstOrDefault(p => p.КодПрограммы == id);
            if (программа == null)
            {
                return NotFound(new { message = "Программа не найдена" });
            }

            return Ok(программа);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Программы программа)
        {
            if (программа == null)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            if (!_context.Факультетыs.Any(f => f.КодФакультета == программа.КодФакультета))
            {
                return BadRequest(new { message = "Факультет с указанным КодФакультета не существует" });
            }

            _context.Программыs.Add(программа);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = программа.КодПрограммы }, программа);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Программы обновленнаяПрограмма)
        {
            if (обновленнаяПрограмма == null || обновленнаяПрограмма.КодПрограммы != id)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            var существующаяПрограмма = _context.Программыs.FirstOrDefault(p => p.КодПрограммы == id);
            if (существующаяПрограмма == null)
            {
                return NotFound(new { message = "Программа не найдена" });
            }

            существующаяПрограмма.НазваниеПрограммы = обновленнаяПрограмма.НазваниеПрограммы;
            существующаяПрограмма.КодФакультета = обновленнаяПрограмма.КодФакультета;
            существующаяПрограмма.Продолжительность = обновленнаяПрограмма.Продолжительность;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var программа = _context.Программыs.FirstOrDefault(p => p.КодПрограммы == id);
            if (программа == null)
            {
                return NotFound(new { message = "Программа не найдена" });
            }

            _context.Программыs.Remove(программа);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
