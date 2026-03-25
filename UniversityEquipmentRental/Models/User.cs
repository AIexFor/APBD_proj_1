namespace UniversityEquipmentRental;

public enum UserType
{
    Student,
    Employee
}

public abstract class User
{
    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserType UserType { get; }

    protected User(int id, string firstName, string lastName, UserType userType)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        UserType = userType;
    }

    public string FullName => $"{FirstName} {LastName}";
}

public class Student : User
{
    public Student(int id, string firstName, string lastName)
        : base(id, firstName, lastName, UserType.Student)
    {
    }
}

public class Employee : User
{
    public Employee(int id, string firstName, string lastName)
        : base(id, firstName, lastName, UserType.Employee)
    {
    }
}