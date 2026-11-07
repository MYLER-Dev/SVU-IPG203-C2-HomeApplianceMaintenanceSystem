namespace HomeApplianceMaintenanceSystem.Models;

// Encapsulation / التغليف

public interface IRepairable
{
    string SerialNumber { get; }
    string Status { get; }
    decimal CalculateRepairCost();
    void ChangeStatus(string newStatus);
}
