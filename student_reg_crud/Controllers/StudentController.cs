using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using student_reg_crud.Model;

namespace student_reg_crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _studentContext;
        public StudentController(StudentContext studentContext) 
        {
        _studentContext = studentContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            if(_studentContext.Students == null)
            {
                return NotFound();
            }
            return await _studentContext.Students.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            if (_studentContext.Students == null)
            {
                return NotFound();
            }
            var stu = await _studentContext.Students.FindAsync(id);
            if (stu == null)
            {
                return NotFound();
            }
            return stu;
        }
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _studentContext.Students.Add(student);
            await _studentContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutStudent(int id,Student student)
        {
            if(id != student.Id)
            {
                return BadRequest();
            }
            _studentContext.Entry(student).State = EntityState.Modified;
            try
            {
                await _studentContext.SaveChangesAsync();
            }catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            if(_studentContext.Students == null)
            {

            return NotFound(); 
            
            }

            var stu = await _studentContext.Students.FindAsync(id);
            if (stu == null)
            {
                return NotFound();
            }
            _studentContext.Students.Remove(stu);
            await _studentContext.SaveChangesAsync();

            return Ok();
        }
    }
}
