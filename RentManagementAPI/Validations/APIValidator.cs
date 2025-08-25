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

            bool validCNH = ValidateCNHType(deliveryMan.CNHType);

            if (!validCNH)
                throw new Exception();
        }

        public static bool ValidateCNHType(string CNHType)
        {
            List<char> cnhTypesList = string.IsNullOrWhiteSpace(CNHType)
                ? new List<char>()
                : CNHType.ToCharArray().ToList();
            if (cnhTypesList == null || !cnhTypesList.Contains('A'))
                return false;

            return true;
        }

        #region Lease validations
        private static readonly Dictionary<int, decimal> PlanPrices = new()
        {
            { 7, 30.00m },
            { 15, 28.00m },
            { 30, 22.00m },
            { 45, 20.00m },
            { 50, 18.00m }
        };

        public static void ValidateLease(Lease lease, string CNHType)
        {
            if (lease == null)
                throw new Exception();

            bool validCNH = ValidateCNHType(CNHType);

            if (!validCNH)
                throw new Exception();

            // Validar campos obrigatórios
            if (string.IsNullOrWhiteSpace(lease.DeliveryManId) ||
                string.IsNullOrWhiteSpace(lease.MotorcycleId))
                throw new Exception();

            // Validar datas
            var tomorrow = DateTime.Today.AddDays(1);

            // Data de início deve ser amanhã
            if (lease.StartDate.Date != tomorrow)
                throw new Exception();

            // Data de término e previsão devem ser posteriores ao início
            if (lease.EndDate <= lease.StartDate ||
                lease.ExpectedEndDate <= lease.StartDate)
                throw new Exception();

            // Validar plano (quantidade de dias)
            var days = (lease.EndDate - lease.StartDate).Days;
            if (!PlanPrices.ContainsKey(days))
                throw new Exception();

            // A data de previsão de término deve ser igual ou posterior à data de término
            if (lease.ExpectedEndDate < lease.EndDate)
                throw new Exception();

            // O plano (número de dias) deve corresponder a um dos planos válidos
            if (lease.Plan != 7 && lease.Plan != 15 && lease.Plan != 30 &&
                lease.Plan != 45 && lease.Plan != 50)
                throw new Exception();

            // Validar se o número de dias do plano corresponde ao período entre início e fim
            if (lease.Plan != days)
                throw new Exception();
        }
        #endregion
    }
}
