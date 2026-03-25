using System.Text;
using UniversityEquipmentRental.Models;

namespace UniversityEquipmentRental.Services;

public class ReportService(AppDataContext context)
{
    private readonly AppDataContext _context = context;

    public string GenerateSummary()
    {
        var totalEquipment = _context.EquipmentItems.Count;
        var availableEquipment = _context.EquipmentItems.Count(e => e.Status == EquipmentStatus.Available);
        var rentedEquipment = _context.EquipmentItems.Count(e => e.Status == EquipmentStatus.Rented);
        var unavailableEquipment = _context.EquipmentItems.Count(e => e.Status == EquipmentStatus.Unavailable);

        var totalUsers = _context.Users.Count;
        var activeRentals = _context.Rentals.Count(r => !r.IsReturned);
        var overdueRentals = _context.Rentals.Count(r => !r.IsReturned && r.IsOverdue);
        var totalPenalties = _context.Rentals.Sum(r => r.Penalty);

        var sb = new StringBuilder();
        sb.AppendLine("===== RENTAL REPORT =====");
        sb.AppendLine($"Total equipment: {totalEquipment}");
        sb.AppendLine($"Available equipment: {availableEquipment}");
        sb.AppendLine($"Rented equipment: {rentedEquipment}");
        sb.AppendLine($"Unavailable equipment: {unavailableEquipment}");
        sb.AppendLine($"Total users: {totalUsers}");
        sb.AppendLine($"Active rentals: {activeRentals}");
        sb.AppendLine($"Overdue rentals: {overdueRentals}");
        sb.AppendLine($"Total penalties: {totalPenalties} PLN");

        return sb.ToString();
    }
}