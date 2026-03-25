using UniversityEquipmentRental.Models;

namespace UniversityEquipmentRental.Services;

public class RentalService(AppDataContext context, IdGenerator idGenerator, IPenaltyCalculator penaltyCalculator)
{
    private readonly AppDataContext _context = context;
    private readonly IdGenerator _idGenerator = idGenerator;
    private readonly IPenaltyCalculator _penaltyCalculator = penaltyCalculator;

    public Rental RentEquipment(int userId, int equipmentId, int days, DateTime? rentDate = null)
    {
        var user = GetUser(userId);
        var equipment = GetEquipment(equipmentId);

        switch (equipment.Status)
        {
            case EquipmentStatus.Unavailable:
                throw new BusinessException("Equipment is unavailable.");
            case EquipmentStatus.Rented:
                throw new BusinessException("Equipment is already rented.");
        }

        var activeRentalsCount = _context.Rentals.Count(r => r.User.Id == userId && !r.IsReturned);
        var limit = RentalPolicy.GetLimitFor(user);

        if (activeRentalsCount >= limit)
        {
            throw new BusinessException($"{user.UserType} cannot have more than {limit} active rentals.");
        }

        var rental = new Rental(
            _idGenerator.NextRentalId(),
            user,
            equipment,
            rentDate ?? DateTime.Now,
            days);

        equipment.Status = EquipmentStatus.Rented;
        _context.Rentals.Add(rental);

        return rental;
    }

    public decimal ReturnEquipment(int rentalId, DateTime returnDate)
    {
        var rental = _context.Rentals.FirstOrDefault(r => r.Id == rentalId) ?? throw new BusinessException($"Rental with id {rentalId} was not found.");

        if (rental.IsReturned)
        {
            throw new BusinessException("This rental has already been returned.");
        }

        var penalty = _penaltyCalculator.Calculate(rental.DueDate, returnDate);

        rental.Return(returnDate, penalty);
        rental.Equipment.Status = EquipmentStatus.Available;

        return penalty;
    }

    public List<Rental> GetActiveRentalsForUser(int userId)
    {
        return _context.Rentals.Where(r => r.User.Id == userId && !r.IsReturned).ToList();
    }

    public List<Rental> GetOverdueRentals()
    {
        return _context.Rentals.Where(r => r is { IsReturned: false, IsOverdue: true }).ToList();
    }

    public List<Rental> GetAll()
    {
        return _context.Rentals;
    }

    private User GetUser(int userId)
    {
        return _context.Users.FirstOrDefault(u => u.Id == userId) ?? throw new BusinessException($"User with id {userId} was not found.");
    }

    private Equipment GetEquipment(int equipmentId)
    {
        return _context.EquipmentItems.FirstOrDefault(e => e.Id == equipmentId) ?? throw new BusinessException($"Equipment with id {equipmentId} was not found.");
    }
}