using UniversityEquipmentRental.Models;

namespace UniversityEquipmentRental.Services;

public class EquipmentService(AppDataContext context, IdGenerator idGenerator)
{
    public Laptop AddLaptop(string name, int ramGb, string processor)
    {
        var laptop = new Laptop(idGenerator.NextEquipmentId(), name, ramGb, processor);
        context.EquipmentItems.Add(laptop);
        return laptop;
    }

    public Projector AddProjector(string name, string resolution, int brightnessLumens)
    {
        var projector = new Projector(idGenerator.NextEquipmentId(), name, resolution, brightnessLumens);
        context.EquipmentItems.Add(projector);
        return projector;
    }

    public Camera AddCamera(string name, int megapixels, string lensType)
    {
        var camera = new Camera(idGenerator.NextEquipmentId(), name, megapixels, lensType);
        context.EquipmentItems.Add(camera);
        return camera;
    }

    private Equipment GetById(int id)
    {
        return context.EquipmentItems.FirstOrDefault(e => e.Id == id) ?? throw new BusinessException($"Equipment with id {id} was not found.");
    }

    public List<Equipment> GetAll()
    {
        return context.EquipmentItems;
    }

    public List<Equipment> GetAvailable()
    {
        return context.EquipmentItems.Where(e => e.Status == EquipmentStatus.Available).ToList();
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