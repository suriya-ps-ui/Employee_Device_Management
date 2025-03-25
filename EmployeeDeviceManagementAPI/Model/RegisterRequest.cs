namespace Model{
    public class RegisterRequest   {
        public required string username{get;set;}
        public required string Password{get;set;}
        public required string role{get;set;}
        public  string? empId{get;set;}
    }
}