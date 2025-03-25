namespace Model{
    public class Employee{
        public required string empId{get;set;}
        public required string empName{get;set;}
        public required string department{get;set;}
        public required bool isAvilable{get;set;}
        public virtual ICollection<Laptop> Laptops {get;set;}=new List<Laptop>();
        public virtual ICollection<Keyboard> Keyboards{get;set;}=new List<Keyboard>();
        public virtual ICollection<Mouse> Mouses{get;set;}=new List<Mouse>();
    }
    public class EmployeeRequest{
        public required string empId{get;set;}
        public required string empName{get;set;}
        public required string department{get;set;}
        public  required bool isAvilable{get;set;}
    }
}