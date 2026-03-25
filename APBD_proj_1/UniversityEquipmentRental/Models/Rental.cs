namespace UniversityEquipmentRental.Models;

public class Rental(int id, User user, Equipment equipment, DateTime rentDate, int days)
{
    public int Id { get; } = id;
    public User User { get; } = user;
    public Equipment Equipment { get; } = equipment;
    public DateTime RentDate { get; } = rentDate;
    public DateTime DueDate { get; } = rentDate.AddDays(days);
    private DateTime? ReturnDate { get; set; }
    public decimal Penalty { get; private set; }

    public bool IsReturned => ReturnDate.HasValue;

    public bool IsOverdue
    {
        get
        {
            if (IsReturned)
            {
                return ReturnDate!.Value.Date > DueDate.Date;
            }

            return DateTime.Now.Date > DueDate.Date;
        }
    }

    public void Return(DateTime returnDate, decimal penalty)
    {
        ReturnDate = returnDate;
        Penalty = penalty;
    }
}