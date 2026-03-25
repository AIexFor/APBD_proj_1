using UniversityEquipmentRental.Models;

namespace UniversityEquipmentRental;

public static class RentalPolicy
{
    private const int StudentMaxActiveRentals = 2;
    private const int EmployeeMaxActiveRentals = 5;
    public const decimal PenaltyPerDay = 10m;

    public static int GetLimitFor(User user)
    {
        return user.UserType switch
        {
            UserType.Student => StudentMaxActiveRentals,
            UserType.Employee => EmployeeMaxActiveRentals,
            _ => throw new InvalidOperationException("Unknown user type.")
        };
    }
}

public class AppDataContext
{
    public List<User> Users { get; } = [];
    public List<Equipment> EquipmentItems { get; } = [];
    public List<Rental> Rentals { get; } = [];
}

public class BusinessException(string message) : Exception(message);

public class IdGenerator
{
    private int _nextUserId = 1;
    private int _nextEquipmentId = 1;
    private int _nextRentalId = 1;

    public int NextUserId() => _nextUserId++;
    public int NextEquipmentId() => _nextEquipmentId++;
    public int NextRentalId() => _nextRentalId++;
}

public interface IPenaltyCalculator
{
    decimal Calculate(DateTime dueDate, DateTime returnDate);
}

public class StandardPenaltyCalculator : IPenaltyCalculator
{
    public decimal Calculate(DateTime dueDate, DateTime returnDate)
    {
        if (returnDate.Date <= dueDate.Date)
        {
            return 0m;
        }

        var overdueDays = (returnDate.Date - dueDate.Date).Days;
        return overdueDays * RentalPolicy.PenaltyPerDay;
    }
}