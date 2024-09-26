using Microsoft.EntityFrameworkCore;

namespace student_reg_crud.Model
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext>options) : base(options) 
        {
            
        }

        public DbSet<Student> Students { get; set; }
    }
}
