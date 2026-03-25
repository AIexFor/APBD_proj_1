namespace UniversityEquipmentRental.Models;

public enum UserType
{
    Student,
    Employee
}

public abstract class User(int id, string firstName, string lastName, UserType userType)
{
    public int Id { get; } = id;
    private string FirstName { get; set; } = firstName;
    private string LastName { get; set; } = lastName;
    public UserType UserType { get; } = userType;

    public string FullName => $"{FirstName} {LastName}";
}

public class Student(int id, string firstName, string lastName) : User(id, firstName, lastName, UserType.Student);

public class Employee(int id, string firstName, string lastName) : User(id, firstName, lastName, UserType.Employee); 