using HomeApplianceMaintenanceSystem.Models;

namespace HomeApplianceMaintenanceSystem.Events;

// Delegate & Events -  التفويض و الاحداث
public delegate void ApplianceStatusChangedHandler(
    Appliance appliance, string oldStatus, string newStatus);
