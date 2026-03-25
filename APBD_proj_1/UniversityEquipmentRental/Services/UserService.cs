using UniversityEquipmentRental.Models;

namespace UniversityEquipmentRental.Services;

public class UserService(AppDataContext context, IdGenerator idGenerator)
{
    private readonly AppDataContext _context = context;
    private readonly IdGenerator _idGenerator = idGenerator;

    public Student AddStudent(string firstName, string lastName)
    {
        var student = new Student(_idGenerator.NextUserId(), firstName, lastName);
        _context.Users.Add(student);
        return student;
    }

    public Employee AddEmployee(string firstName, string lastName)
    {
        var employee = new Employee(_idGenerator.NextUserId(), firstName, lastName);
        _context.Users.Add(employee);
        return employee;
    }

    public User GetById(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id) ?? throw new BusinessException($"User with id {id} was not found.");
    }

    public List<User> GetAll()
    {
        return _context.Users;
    }
}