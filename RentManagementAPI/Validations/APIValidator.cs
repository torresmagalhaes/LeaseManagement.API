using LeaseManagement.Domain.Entities;
using LeaseManagement.Domain.Entities.MongoDB;
using LeaseManagement.Infrastructure.MongoDB.Implementation;

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

            ValidateCNHType(deliveryMan.CNHType);
        }

        public static void ValidateCNHType(string CNHType)
        {
            List<char> cnhTypesList = string.IsNullOrWhiteSpace(CNHType)
                ? new List<char>()
                : CNHType.ToCharArray().ToList();
            if (cnhTypesList == null || !cnhTypesList.Contains('A'))
                throw new Exception($"Entregador {CNHType} não possui CNH tipo A");
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

        public static void ValidateLease(Lease lease, DeliveryManImplementation deliveryManImplementation, MotorcycleImplementation motorcycleImplementation, LeaseImplementation leaseImplementation)
        {
            if (lease == null) throw new Exception("Payload fornecido inválido");

            DeliveryManDocument deliveryMan = deliveryManImplementation.GetByIdentifier(lease.DeliveryManId);

            if (deliveryMan == null) throw new Exception($"Entregador {lease.DeliveryManId} não encontrado");

            ValidateCNHType(deliveryMan.CNHType);

            MotorcycleDocument motorcycle = motorcycleImplementation.GetById(lease.MotorcycleId);   
            if (motorcycle == null) 
                throw new Exception($"Não há moto com Id {lease.MotorcycleId} registrada em nosso sistema");

            LeaseDocument leaseDocument = leaseImplementation.GetByMotorcycleId(motorcycle.Identifier);
            var tomorrow = DateTime.Today.AddDays(1);

            if (leaseDocument != null && leaseDocument.EndDate.Date >= tomorrow)
                throw new Exception($"Moto {motorcycle.Identifier} já está alugada até {leaseDocument.EndDate.Date.ToShortDateString()}");

            if (lease.StartDate.Date != tomorrow)
                throw new Exception("Data de início deve ser amanhã");

            if (lease.EndDate <= lease.StartDate ||
                lease.ExpectedEndDate <= lease.StartDate)
                throw new Exception("Data de término e previsão devem ser posteriores ao início");

            var days = (lease.EndDate - lease.StartDate).Days;
            if (!PlanPrices.ContainsKey(days)) throw new Exception("Plano fornecido invalido");

            if (lease.ExpectedEndDate < lease.EndDate) 
                throw new Exception("A data de previsão de término deve ser igual ou posterior à data de término");

            if (lease.Plan != 7 && lease.Plan != 15 && lease.Plan != 30 &&
                lease.Plan != 45 && lease.Plan != 50)
                throw new Exception("Plano fornecido invalido");

            if (lease.Plan != days) 
                throw new Exception("Número de dias do plano não corresponde ao período entre início e fim");
        }
        #endregion
    }
}
