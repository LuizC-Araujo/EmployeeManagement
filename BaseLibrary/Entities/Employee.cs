namespace BaseLibrary.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Cpf { get; private set; }
        public string? FileNumber { get; private set; }
        public string? Fullname { get; private set; }
        public string? Address { get; private set; }
        public string? JobName { get; private set; }
        public string? TelephoneNumber { get; private set; }
        public string? Photo { get; private set; }
        public string? Other { get; private set; }

        // relação: muitos para um
        public GeneralDepartment? GeneralDepartment { get; set; }
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public Branch? Branch { get; set; }
        public int BranchId { get; set; }
        public Town? Town { get; set; }
        public int TownId { get; set; }
    }
}
