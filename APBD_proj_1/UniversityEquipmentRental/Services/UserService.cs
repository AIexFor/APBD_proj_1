using UniversityEquipmentRental.Models;

namespace UniversityEquipmentRental.Services;

public class UserService(AppDataContext context, IdGenerator idGenerator)
{
    public Student AddStudent(string firstName, string lastName)
    {
        var student = new Student(idGenerator.NextUserId(), firstName, lastName);
        context.Users.Add(student);
        return student;
    }

    public Employee AddEmployee(string firstName, string lastName)
    {
        var employee = new Employee(idGenerator.NextUserId(), firstName, lastName);
        context.Users.Add(employee);
        return employee;
    }

    public User GetById(int id)
    {
        return context.Users.FirstOrDefault(u => u.Id == id) ?? throw new BusinessException($"User with id {id} was not found.");
    }

    public List<User> GetAll()
    {
        return context.Users;
    }
}