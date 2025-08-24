using LeaseManagement.Domain.Entities;
using Microsoft.OpenApi.Any;
using System.Collections.Generic;
using System.Linq;

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

        public static void ValidateDeliveryManRegister(DeliveryMan deliveryMan)
        {
            if (string.IsNullOrWhiteSpace(deliveryMan.Identifier) ||
                string.IsNullOrWhiteSpace(deliveryMan.Name) ||
                string.IsNullOrWhiteSpace(deliveryMan.TaxNumber))
                throw new Exception();

            List<char> cnhTypesList = string.IsNullOrWhiteSpace(deliveryMan.CNHType)
                ? new List<char>()
                : deliveryMan.CNHType.ToCharArray().ToList();
            
            if (cnhTypesList == null || !cnhTypesList.Contains('A'))
                throw new Exception();
        }
    }
}
