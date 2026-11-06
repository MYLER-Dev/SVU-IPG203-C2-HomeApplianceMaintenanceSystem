using HomeApplianceMaintenanceSystem.Models;
using HomeApplianceMaintenanceSystem.Services;
using HomeApplianceMaintenanceSystem.Utilities;

namespace HomeApplianceMaintenanceSystem;

internal static class Program
{
    private static readonly RepairCenter Center = new();

    private static void Main()
    {
        Center.ApplianceStatusChanged += OnApplianceStatusChanged;
        SeedSampleData();

        while (true)
        {
            Console.WriteLine("\n=== HOME APPLIANCE MAINTENANCE SYSTEM ===");
            Console.WriteLine("1. Register a new appliance");
            Console.WriteLine("2. Display all appliances");
            Console.WriteLine("3. Change appliance status");
            Console.WriteLine("4. Display statistics");
            Console.WriteLine("5. Exit");
            Console.Write("Choose: ");

            switch (Console.ReadLine())
            {
                case "1": RegisterAppliance(); break;
                case "2": Center.DisplayAllAppliances(); break;
                case "3": ChangeStatus(); break;
                case "4": DisplayStatistics(); break;
                case "5": return;
                default: Console.WriteLine("Invalid menu choice."); break;
            }
        }
    }
    // ÏÇÊÇ ÊÌÑíÈíÉ 
    private static void SeedSampleData()
    {
        Center.AddAppliance(new Refrigerator("REF-1001", "Ahmad", "Samsung", 2019, 25));
        Center.AddAppliance(new WashingMachine("WM-2001", "Mona", "LG", 2021, 2));
        Center.AddAppliance(new AirConditioner("AC-3001", "Khaled", "Gree", 2020, 18000));
    }

    private static void RegisterAppliance()
    {
        Console.Write("Serial number: ");
        string serial = Console.ReadLine() ?? "";
        Console.Write("Customer name: ");
        string customer = Console.ReadLine() ?? "";
        Console.Write("Brand: ");
        string brand = Console.ReadLine() ?? "";
        Console.Write("Manufacturing year: ");
        if (!int.TryParse(Console.ReadLine(), out int year))
        {
            Console.WriteLine("Year must be a number.");
            return;
        }

        Console.WriteLine("Type: 1-Refrigerator  2-Washing machine  3-Air conditioner");
        Console.Write("Choose type: ");
        string type = Console.ReadLine() ?? "";
        Appliance? appliance = null;

        switch (type)
        {
            case "1":
                Console.Write("Capacity in cubic feet: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal capacity))
                    appliance = new Refrigerator(serial, customer, brand, year, capacity);
                break;
            case "2":
                Console.Write("Number of programs: ");
                if (int.TryParse(Console.ReadLine(), out int programs))
                    appliance = new WashingMachine(serial, customer, brand, year, programs);
                break;
            case "3":
                Console.Write("Cooling capacity in BTU: ");
                if (int.TryParse(Console.ReadLine(), out int btu))
                    appliance = new AirConditioner(serial, customer, brand, year, btu);
                break;
            default:
                Console.WriteLine("Unknown appliance type.");
                return;
        }

        if (appliance is null)
        {
            Console.WriteLine("Invalid appliance details.");
            return;
        }

        Center.AddAppliance(appliance);
    }

    private static void ChangeStatus()
    {
        Console.Write("Enter appliance serial number: ");
        string serial = Console.ReadLine() ?? "";
        Appliance? appliance = Center.FindBySerial(serial);
        if (appliance is null)
        {
            Console.WriteLine("Appliance not found.");
            return;
        }

        Console.WriteLine("1-Under Maintenance  2-Repaired  3-Delivered  4-Delivery Delayed");
        Console.Write("Choose new status: ");
        string choice = Console.ReadLine() ?? "";
        switch (choice)
        {
            case "1": appliance.ChangeStatus("Under Maintenance"); break;
            case "2": appliance.ChangeStatus("Repaired"); break;
            case "3": appliance.ChangeStatus("Delivered"); break;
            case "4": appliance.MarkDeliveryDelayed(); break;
            default: Console.WriteLine("Invalid status."); break;
        }
    }

    private static void DisplayStatistics()
    {
        Console.WriteLine($"\nRegistered appliances: {Appliance.TotalRegistered}");
        Console.WriteLine($"Total estimated repair cost: {Appliance.TotalEstimatedRepairCost:C}");
        Console.WriteLine($"Currently under maintenance: {Center.CountByStatus("Under Maintenance")}");
        Console.WriteLine($"Repaired or delivered: {Center.CountCompletedOrDelivered()}");
    }

    private static void OnApplianceStatusChanged(Appliance appliance, string oldStatus, string newStatus)
    {
        Console.WriteLine($"\n[EVENT] {appliance.SerialNumber}: {oldStatus} -> {newStatus}");
        if (newStatus == "Repaired")
            Console.WriteLine("[NOTICE] Repair has been completed.");
        else if (newStatus == "Delivery Delayed")
            Console.WriteLine("[NOTICE] Delivery is delayed. Contact the customer.");
    }
}
