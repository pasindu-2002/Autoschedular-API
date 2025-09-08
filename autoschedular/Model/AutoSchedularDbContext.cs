using Microsoft.EntityFrameworkCore;

namespace autoschedular.Model
{
    public class AutoSchedularDbContext : DbContext
    {
        public AutoSchedularDbContext(DbContextOptions<AutoSchedularDbContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<PoStaff> PoStaffs { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<LecturerTimetable> LecturerTimetables { get; set; }
        public DbSet<AssignModule> AssignModules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure AssignModule foreign key relationships to prevent cascade cycles
            modelBuilder.Entity<AssignModule>()
                .HasOne(am => am.Batch)
                .WithMany()
                .HasForeignKey(am => am.BatchId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AssignModule>()
                .HasOne(am => am.Lecturer)
                .WithMany()
                .HasForeignKey(am => am.LecturerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AssignModule>()
                .HasOne(am => am.Module)
                .WithMany()
                .HasForeignKey(am => am.ModuleId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure LecturerTimetable foreign key relationship
            modelBuilder.Entity<LecturerTimetable>()
                .HasOne(lt => lt.Lecturer)
                .WithMany(l => l.LecturerTimetables)
                .HasForeignKey(lt => lt.LecturerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
