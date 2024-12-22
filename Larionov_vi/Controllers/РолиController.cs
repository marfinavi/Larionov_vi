using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Larionov_vi.Models;

namespace Larionov_vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class РолиController : ControllerBase
    {
        private readonly VicourselaContext _context;

        public РолиController(VicourselaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var роли = _context.Ролиs.ToList();
            return Ok(роли);
        }

        // GET: api/Роли
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var роль = _context.Ролиs.FirstOrDefault(r => r.КодРоли == id);
            if (роль == null)
            {
                return NotFound(new { message = "Роль не найдена" });
            }
            return Ok(роль);
        }

        // POST: api/Роли
        [HttpPost]
        public IActionResult Create([FromBody] Роли роль)
        {
            if (роль == null)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            _context.Ролиs.Add(роль);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = роль.КодРоли }, роль);
        }

        // PUT: api/Роли
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Роли обновленнаяРоль)
        {
            if (обновленнаяРоль == null || обновленнаяРоль.КодРоли != id)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            var существующаяРоль = _context.Ролиs.FirstOrDefault(r => r.КодРоли == id);
            if (существующаяРоль == null)
            {
                return NotFound(new { message = "Роль не найдена" });
            }

            существующаяРоль.НазваниеРоли = обновленнаяРоль.НазваниеРоли;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Роли
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var роль = _context.Ролиs.FirstOrDefault(r => r.КодРоли == id);
            if (роль == null)
            {
                return NotFound(new { message = "Роль не найдена" });
            }

            _context.Ролиs.Remove(роль);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
