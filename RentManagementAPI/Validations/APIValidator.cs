using LeaseManagement.Domain.Entities;

namespace LeaseManagementAPI.Validations
{
    public class APIValidator
    {
        public static void ValidateMotorcycleRegister(Motorcycle motorcycle)
        {
            if (string.IsNullOrWhiteSpace(motorcycle.Identifier) ||
                string.IsNullOrWhiteSpace(motorcycle.Model) ||
                string.IsNullOrWhiteSpace(motorcycle.LicensePlate) ||
                motorcycle.Year == 0)
                throw new Exception();
        }
    }
}
