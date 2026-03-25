namespace UniversityEquipmentRental;

public enum EquipmentStatus
{
    Available,
    Rented,
    Unavailable
}

public abstract class Equipment
{
    public int Id { get; }
    public string Name { get; set; }
    public EquipmentStatus Status { get; set; }

    protected Equipment(int id, string name)
    {
        Id = id;
        Name = name;
        Status = EquipmentStatus.Available;
    }

    public abstract string GetSpecification();
}
public class Laptop : Equipment
{
    public int RamGb { get; set; }
    public string Processor { get; set; }

    public Laptop(int id, string name, int ramGb, string processor) : base(id, name)
    {
        RamGb = ramGb;
        Processor = processor;
    }

    public override string GetSpecification()
    {
        return $"Laptop | RAM: {RamGb} GB | CPU: {Processor}";
    }
}

public class Projector : Equipment
{
    public string Resolution { get; set; }
    public int BrightnessLumens { get; set; }

    public Projector(int id, string name, string resolution, int brightnessLumens) : base(id, name)
    {
        Resolution = resolution;
        BrightnessLumens = brightnessLumens;
    }

    public override string GetSpecification()
    {
        return $"Projector | Resolution: {Resolution} | Brightness: {BrightnessLumens} lm";
    }
}

public class Camera : Equipment
{
    public int Megapixels { get; set; }
    public string LensType { get; set; }

    public Camera(int id, string name, int megapixels, string lensType) : base(id, name)
    {
        Megapixels = megapixels;
        LensType = lensType;
    }

    public override string GetSpecification()
    {
        return $"Camera | MP: {Megapixels} | Lens: {LensType}";
    }
}
