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
    public int RamGb { get; set; } = ramGb;
    public string Processor { get; set; } = processor;

    public override string ToString()
    {
        return ($"Laptop | RAM: {RamGb} GB | CPU: {Processor}");
    }

    public override string GetSpecification()
    {
        return $"Laptop | RAM: {RamGb} GB | CPU: {Processor}";
    }
}

public class Projector(int id, string name, string resolution, int brightnessLumens)
    : Equipment(id, name)
{
    private string _field;
    public string Field
    {
        get { return _field; }
        set { _field = value; }
    }
    public string Resolution { get; set; } = resolution;
    public int BrightnessLumens { get; set; } = brightnessLumens;

    public override string GetSpecification()
    {
        return $"Projector | Resolution: {Resolution} | Brightness: {BrightnessLumens} lm";
    }
}

public class Camera(int id, string name, int megapixels, string lensType)
    : Equipment(id, name)
{
    public int Megapixels { get; set; } = megapixels;
    public string LensType { get; set; } = lensType;

    public override string GetSpecification()
    {
        return $"Camera | MP: {Megapixels} | Lens: {LensType}";
    }
}