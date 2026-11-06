using HomeApplianceMaintenanceSystem.Models;

namespace HomeApplianceMaintenanceSystem.Events;

// Delegate & Events -  ÇáÊİæíÖ æ ÇáÇÍÏÇË
public delegate void ApplianceStatusChangedHandler(
    Appliance appliance, string oldStatus, string newStatus);
