namespace HomeApplianceMaintenanceSystem.Models;

public sealed class WashingMachine : Appliance
{
    private int _programCount;
    public int ProgramCount
    {
        get => _programCount;
        set => _programCount = value > 0 ? value :
            throw new ArgumentException("Program count must be positive.");
    }

    public WashingMachine(string serial, string customer, string brand, int year, int programs)
        : base(serial, customer, brand, year)
    {
        ProgramCount = programs;
    }

    public override decimal CalculateRepairCost() => 40m + (ProgramCount * 4m);
}

