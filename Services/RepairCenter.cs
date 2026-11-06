using HomeApplianceMaintenanceSystem.Events;
using HomeApplianceMaintenanceSystem.Models;

namespace HomeApplianceMaintenanceSystem.Services;

public sealed class RepairCenter
{
    // Polymorphism - ÊÚÏÏ ÇáÇÔßÇá 
    private readonly List<Appliance> _appliances = new();

    public event ApplianceStatusChangedHandler? ApplianceStatusChanged;

    public void AddAppliance(Appliance appliance)
    {
        ArgumentNullException.ThrowIfNull(appliance);
        if (FindBySerial(appliance.SerialNumber) is not null)
            throw new InvalidOperationException("An appliance with this serial number already exists.");

        appliance.StatusChanged += ForwardStatusChange;
        appliance.AddCostToStatistics();
        _appliances.Add(appliance);
        Console.WriteLine("Appliance registered successfully.");
    }

    public Appliance? FindBySerial(string serial) =>
        _appliances.FirstOrDefault(a =>
            a.SerialNumber.Equals(serial.Trim(), StringComparison.OrdinalIgnoreCase));

    public void DisplayAllAppliances()
    {
        if (_appliances.Count == 0)
        {
            Console.WriteLine("No appliances registered.");
            return;
        }

        foreach (Appliance appliance in _appliances)
            Console.WriteLine(appliance.GetDetails()); 
    }

    public int CountByStatus(string status) =>
        _appliances.Count(a => a.Status == status);

    public int CountCompletedOrDelivered() =>
        _appliances.Count(a => a.Status is "Repaired" or "Delivered");

    private void ForwardStatusChange(Appliance appliance, string oldStatus, string newStatus)
    {
        ApplianceStatusChanged?.Invoke(appliance, oldStatus, newStatus);
    }
}
