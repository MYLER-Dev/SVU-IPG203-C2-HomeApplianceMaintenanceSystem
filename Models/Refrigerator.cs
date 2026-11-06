namespace HomeApplianceMaintenanceSystem.Models;

public sealed class Refrigerator : Appliance
{
    private decimal _capacityCubicFeet;
    public decimal CapacityCubicFeet
    {
        get => _capacityCubicFeet;
        set => _capacityCubicFeet = value > 0 ? value :
            throw new ArgumentException("Capacity must be positive.");
    }

    public Refrigerator(string serial, string customer, string brand, int year, decimal capacity)
        : base(serial, customer, brand, year)
    {
        CapacityCubicFeet = capacity;
    }

    public override decimal CalculateRepairCost() => 35m + (CapacityCubicFeet * 2.5m);
}
