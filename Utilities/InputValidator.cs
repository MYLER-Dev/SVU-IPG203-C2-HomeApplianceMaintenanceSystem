namespace HomeApplianceMaintenanceSystem.Utilities;


public static class InputValidator
{
    public static bool IsValidSerialNumber(string serial)
    {
        if (string.IsNullOrWhiteSpace(serial)  serial.Length < 4  serial.Length > 20)
            return false;

        return serial.All(c => char.IsLetterOrDigit(c) || c == '-');
    }

    public static bool IsValidYear(int year) =>
        year >= 1980 && year <= DateTime.Now.Year;

