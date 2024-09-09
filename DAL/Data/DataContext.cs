using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public class DataContext : DbContext
{
    public DataContext()
    {
    }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Homework> Homeworks { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Level> Levels { get; set; }
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<GroupPermission> GroupPermissions { get; set; }
    public DbSet<Menu> Menu { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<CourseTemplate> CourseTemplates { get; set; }
    public DbSet<LessonTemplate> LessonTemplates { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<AdministrativeRegions> AdministrativeRegions { get; set; }
    public DbSet<AdministrativeUnits> AdministrativeUnits { get; set; }
    public DbSet<Provinces> Provinces { get; set; }
    public DbSet<Districts> Districts { get; set; }
    public DbSet<Wards> Wards { get; set; }
    
    
}