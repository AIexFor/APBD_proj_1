namespace UniversityEquipmentRental.Models;

public enum EquipmentStatus
{
    Available,
    Rented,
    Unavailable
}

public abstract class Equipment(int id, string name)
{
    public int Id { get; } = id;
    public string Name { get; set; } = name;
    public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;

    public abstract string GetSpecification();
}
public class Laptop(int id, string name, int ramGb, string processor)
    : Equipment(id, name)
{
    private int RamGb { get; set; } = ramGb;
    private string Processor { get; set; } = processor;

    public override string GetSpecification()
    {
        return $"Laptop | RAM: {RamGb} GB | CPU: {Processor}";
    }
}

public class Projector(int id, string name, string resolution, int brightnessLumens)
    : Equipment(id, name)
{
    private string Resolution { get; set; } = resolution;
    private int BrightnessLumens { get; set; } = brightnessLumens;

    public override string GetSpecification()
    {
        return $"Projector | Resolution: {Resolution} | Brightness: {BrightnessLumens} lm";
    }
}

public class Camera(int id, string name, int megapixels, string lensType)
    : Equipment(id, name)
{
    private int Megapixels { get; set; } = megapixels;
    private string LensType { get; set; } = lensType;

    public override string GetSpecification()
    {
        return $"Camera | MP: {Megapixels} | Lens: {LensType}";
    }
}