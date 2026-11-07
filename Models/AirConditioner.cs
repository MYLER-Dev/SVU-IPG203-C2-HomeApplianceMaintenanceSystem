namespace HomeApplianceMaintenanceSystem.Models;

public sealed class AirConditioner : Appliance
{
    private int _coolingCapacityBtu;
    public int CoolingCapacityBtu
    {
        get => _coolingCapacityBtu; // Encapsulation - التغليف
        set => _coolingCapacityBtu = value > 0 ? value :
            throw new ArgumentException("Cooling capacity must be positive.");
    }

    public AirConditioner(string serial, string customer, string brand, int year, int btu)
        : base(serial, customer, brand, year)
    {
        CoolingCapacityBtu = btu;
    }

    public override decimal CalculateRepairCost() => 50m + (CoolingCapacityBtu / 1000m * 3m);
}

