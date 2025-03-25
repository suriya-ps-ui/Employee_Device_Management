using Microsoft.EntityFrameworkCore;
using Model;

namespace Data{
    public class AssetManagementContext:DbContext{
        public DbSet<Employee> Employees{get;set;}
        public DbSet<Laptop> Laptops{get;set;}
        public DbSet<Mouse> Mouses{get;set;}
        public DbSet<Keyboard> Keyboards{get;set;}
        public DbSet<User> Users{get;set;}
        public AssetManagementContext(DbContextOptions<AssetManagementContext> options):base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.HasDefaultSchema("AssetManagement");
            modelBuilder.Entity<Employee>(Employee=>{
                Employee.HasKey(e=>e.empId);
                Employee.Property(e=>e.empName).IsRequired();
                Employee.Property(e=>e.department).IsRequired();
            });
            modelBuilder.Entity<Laptop>(Laptop=>{
                Laptop.HasKey(l=>new {l.empId,l.lapHostName});
                Laptop.HasOne<Employee>().WithMany(e=>e.Laptops).HasForeignKey(l=>l.empId).IsRequired();
            });
            modelBuilder.Entity<Keyboard>(Keyboard=>{
                Keyboard.HasKey(k=>k.keyId);
                Keyboard.HasOne<Employee>().WithMany(e=>e.Keyboards).HasForeignKey(k=>k.empId).IsRequired();
            });
            modelBuilder.Entity<Mouse>(Mouse=>{
                Mouse.HasKey(m=>m.mouseId);
                Mouse.HasOne<Employee>().WithMany(e=>e.Mouses).HasForeignKey(m=>m.empId).IsRequired();
            });
            modelBuilder.Entity<User>(User=>{
                User.HasKey(u =>u.id);
                User.Property(u=>u.userName).IsRequired();
                User.Property(u=>u.password).IsRequired();
                User.Property(u=>u.role).IsRequired();
                User.HasOne(u=>u.Employee).WithMany().HasForeignKey(u => u.empId).IsRequired(false);
            });

            // Seed initial users
            modelBuilder.Entity<Employee>().HasData(
                new Employee{empId="E1",empName="Suriya",department="DotNet",isAvilable=true}
            );
            modelBuilder.Entity<User>().HasData(
                new User{id=1,userName="admin",password="admin",role="Admin",empId=null},
                new User{id=2,userName="e1",password="e1",role="Employee",empId="E1" }
            );
        }
    }
}