using HomeApplianceMaintenanceSystem.Events;
using HomeApplianceMaintenanceSystem.Utilities;

namespace HomeApplianceMaintenanceSystem.Models;

// Abstract / التجريد
public abstract class Appliance : IRepairable
{
    private readonly string _serialNumber;
    private string _customerName;
    private string _brand;
    private int _manufacturingYear;
    private string _status;

    public static int TotalRegistered { get; private set; } // Encapsulation - ÇáÊÛáíÝ
    public static decimal TotalEstimatedRepairCost { get; private set; }// Encapsulation - ÇáÊÛáíÝ

    public string SerialNumber => _serialNumber; // Read-only - ááÞÑÇÁÉ ÝÞØ.
    public string CustomerName
    {
        get => _customerName;
        set => _customerName = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Customer name is required.") : value.Trim();
    }
    public string Brand
    {
        get => _brand;
        set => _brand = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Brand is required.") : value.Trim();
    }
    public int ManufacturingYear
    {
        get => _manufacturingYear;
        set => _manufacturingYear = value;
    }
    public string Status => _status;

    public event ApplianceStatusChangedHandler? StatusChanged;

    protected Appliance(string serialNumber, string customerName, string brand, int year)
    {
        if (!InputValidator.IsValidSerialNumber(serialNumber))
            throw new ArgumentException("Serial number must be 4-20 letters, digits or hyphens.");
        if (!InputValidator.IsValidYear(year))
            throw new ArgumentException("Manufacturing year is invalid.");

        _serialNumber = serialNumber.Trim().ToUpperInvariant();
        CustomerName = customerName;
        Brand = brand;
        ManufacturingYear = year;
        _status = "Received";
        TotalRegistered++;
    }

    
    public abstract decimal CalculateRepairCost();

    public void ChangeStatus(string newStatus)
    {
        string[] allowed = { "Received", "Under Maintenance", "Repaired", "Delivered" };
        if (!allowed.Contains(newStatus))
            throw new ArgumentException("Unsupported status.");

        SetStatus(newStatus);
    }

    public void MarkDeliveryDelayed() => SetStatus("Delivery Delayed");

    private void SetStatus(string newStatus)
    {
        string oldStatus = _status;
        if (oldStatus == newStatus) return;

        _status = newStatus;
        StatusChanged?.Invoke(this, oldStatus, newStatus);
    }

    public void AddCostToStatistics()
    {
        TotalEstimatedRepairCost += CalculateRepairCost();
    }

    public virtual string GetDetails() =>
        $"{GetType().Name} | Serial: {SerialNumber} | Customer: {CustomerName} | " +
        $"Brand: {Brand} | Year: {ManufacturingYear} | Status: {Status} | " +
        $"Estimated cost: {CalculateRepairCost():C}";
}
