using UniversityEquipmentRental.Models;
using UniversityEquipmentRental.Services;

namespace UniversityEquipmentRental;

internal class Program
{
    private static void Main(string[] args)
    {
        var context = new AppDataContext();
        var idGenerator = new IdGenerator();

        var userService = new UserService(context, idGenerator);
        var equipmentService = new EquipmentService(context, idGenerator);
        IPenaltyCalculator penaltyCalculator = new StandardPenaltyCalculator();
        var rentalService = new RentalService(context, idGenerator, penaltyCalculator);
        var reportService = new ReportService(context);

        Console.WriteLine("===== UNIVERSITY EQUIPMENT RENTAL SYSTEM =====");
        Console.WriteLine();

        var student1 = userService.AddStudent("Jan", "Kowalski");
        var student2 = userService.AddStudent("Anna", "Nowak");
        var employee1 = userService.AddEmployee("Piotr", "Zielinski");

        var laptop1 = equipmentService.AddLaptop("Dell Latitude", 16, "Intel i5");
        var laptop2 = equipmentService.AddLaptop("Lenovo ThinkPad", 8, "Intel i7");
        var projector1 = equipmentService.AddProjector("Epson X200", "1920x1080", 3200);
        var camera1 = equipmentService.AddCamera("Canon EOS", 24, "Zoom");
        var projector2 = equipmentService.AddProjector("BenQ M5", "1280x720", 2800);

        Console.WriteLine("1. ALL EQUIPMENT:");
        DisplayEquipment(equipmentService.GetAll());

        Console.WriteLine();
        Console.WriteLine("2. AVAILABLE EQUIPMENT:");
        DisplayEquipment(equipmentService.GetAvailable());

        Console.WriteLine();
        Console.WriteLine("3. CORRECT RENTAL:");

        Rental studentRental1 = null!;
        Rental studentRental2 = null!;

        try
        {
            studentRental1 = rentalService.RentEquipment(student1.Id, laptop1.Id, 7);
            Console.WriteLine($"Rental created: {studentRental1.User.FullName} -> {studentRental1.Equipment.Name}");
        }
        catch (BusinessException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine();
        Console.WriteLine("4. INVALID OPERATION - ALREADY RENTED EQUIPMENT:");

        try
        {
            rentalService.RentEquipment(student2.Id, laptop1.Id, 5);
        }
        catch (BusinessException ex)
        {
            Console.WriteLine($"Blocked: {ex.Message}");
        }

        Console.WriteLine();
        Console.WriteLine("5. STUDENT LIMIT CHECK:");

        try
        {
            studentRental2 = rentalService.RentEquipment(student1.Id, laptop2.Id, 3);
            Console.WriteLine($"Rental created: {studentRental2.User.FullName} -> {studentRental2.Equipment.Name}");

            rentalService.RentEquipment(student1.Id, projector1.Id, 4);
        }
        catch (BusinessException ex)
        {
            Console.WriteLine($"Blocked: {ex.Message}");
        }

        Console.WriteLine();
        Console.WriteLine("6. RETURN ON TIME:");

        try
        {
            var penalty = rentalService.ReturnEquipment(studentRental1.Id, studentRental1.DueDate);
            Console.WriteLine($"Returned on time. Penalty: {penalty} PLN");
        }
        catch (BusinessException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine();
        Console.WriteLine("7. LATE RETURN WITH PENALTY:");

        try
        {
            var pastRental = rentalService.RentEquipment(
                employee1.Id,
                camera1.Id,
                2,
                DateTime.Now.AddDays(-5));

            var penalty = rentalService.ReturnEquipment(pastRental.Id, DateTime.Now);
            Console.WriteLine($"Returned late. Penalty: {penalty} PLN");
        }
        catch (BusinessException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine();
        Console.WriteLine("8. MARK EQUIPMENT AS UNAVAILABLE:");

        try
        {
            equipmentService.MarkAsUnavailable(projector2.Id);
            Console.WriteLine($"{projector2.Name} marked as unavailable.");
        }
        catch (BusinessException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine();
        Console.WriteLine("9. CREATE AN OVERDUE ACTIVE RENTAL FOR DEMO:");

        try
        {
            var overdueRental = rentalService.RentEquipment(
                employee1.Id,
                laptop1.Id,
                1,
                DateTime.Now.AddDays(-4));

            Console.WriteLine($"Overdue rental created: {overdueRental.User.FullName} -> {overdueRental.Equipment.Name}");
        }
        catch (BusinessException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine();
        Console.WriteLine("10. ACTIVE RENTALS FOR FIRST STUDENT:");
        DisplayRentals(rentalService.GetActiveRentalsForUser(student1.Id));

        Console.WriteLine();
        Console.WriteLine("11. OVERDUE RENTALS:");
        DisplayRentals(rentalService.GetOverdueRentals());

        Console.WriteLine();
        Console.WriteLine("12. FINAL REPORT:");
        Console.WriteLine(reportService.GenerateSummary());
    }

    private static void DisplayEquipment(List<Equipment> equipmentItems)
    {
        foreach (var equipment in equipmentItems)
        {
            Console.WriteLine(
                $"ID: {equipment.Id} | Name: {equipment.Name} | Status: {equipment.Status} | {equipment.GetSpecification()}");
        }
    }

    private static void DisplayRentals(List<Rental> rentals)
    {
        if (!rentals.Any())//if (rentals.Count == 0)
        {
            Console.WriteLine("No rentals found.");
            return;
        }

        foreach (var rental in rentals)
        {
            Console.WriteLine(
                $"Rental ID: {rental.Id} | User: {rental.User.FullName} | Equipment: {rental.Equipment.Name} | Rent date: {rental.RentDate:d} | Due date: {rental.DueDate:d} | Returned: {rental.IsReturned} | Penalty: {rental.Penalty} PLN");
        }
    }
}