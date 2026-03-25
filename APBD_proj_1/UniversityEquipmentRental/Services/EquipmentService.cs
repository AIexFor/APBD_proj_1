using UniversityEquipmentRental.Models;

namespace UniversityEquipmentRental.Services;

public class EquipmentService(AppDataContext context, IdGenerator idGenerator)
{
    private readonly AppDataContext _context = context;
    private readonly IdGenerator _idGenerator = idGenerator;

    public Laptop AddLaptop(string name, int ramGb, string processor)
    {
        var laptop = new Laptop(_idGenerator.NextEquipmentId(), name, ramGb, processor);
        _context.EquipmentItems.Add(laptop);
        return laptop;
    }

    public Projector AddProjector(string name, string resolution, int brightnessLumens)
    {
        var projector = new Projector(_idGenerator.NextEquipmentId(), name, resolution, brightnessLumens);
        _context.EquipmentItems.Add(projector);
        return projector;
    }

    public Camera AddCamera(string name, int megapixels, string lensType)
    {
        var camera = new Camera(_idGenerator.NextEquipmentId(), name, megapixels, lensType);
        _context.EquipmentItems.Add(camera);
        return camera;
    }

    private Equipment GetById(int id)
    {
        return _context.EquipmentItems.FirstOrDefault(e => e.Id == id) ?? throw new BusinessException($"Equipment with id {id} was not found.");
    }

    public List<Equipment> GetAll()
    {
        return _context.EquipmentItems;
    }

    public List<Equipment> GetAvailable()
    {
        return _context.EquipmentItems.Where(e => e.Status == EquipmentStatus.Available).ToList();
    }

    public void MarkAsUnavailable(int equipmentId)
    {
        var equipment = GetById(equipmentId);

        if (equipment.Status == EquipmentStatus.Rented)
        {
            throw new BusinessException("Cannot mark rented equipment as unavailable.");
        }

        equipment.Status = EquipmentStatus.Unavailable;
    }
}