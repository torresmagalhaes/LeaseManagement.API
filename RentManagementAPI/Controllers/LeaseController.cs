using Microsoft.AspNetCore.Mvc;
using LeaseManagement.Domain.Entities;
using LeaseManagement.Infrastructure.MongoDB.Implementation;
using LeaseManagementAPI.Validations;
using LeaseManagementAPI.Mappers;
using LeaseManagement.Domain.Entities.MongoDB;

namespace RentManagementAPI.Controllers
{
    [ApiController]
    [Route("locacao")]
    public class LeaseController : ControllerBase
    {
        private readonly LeaseImplementation _leaseImplementation;
        private readonly DeliveryManImplementation _deliveryManImplementation;

        public LeaseController(LeaseImplementation leaseImplementation, DeliveryManImplementation deliveryManImplementation)
        {
            _leaseImplementation = leaseImplementation;
            _deliveryManImplementation = deliveryManImplementation;
        }

        [HttpPost]
        public IActionResult CreateLease([FromBody] Lease lease)
        {
            try
            {
                if (lease == null)
                    throw new Exception();

                DeliveryManDocument deliveryMan = _deliveryManImplementation.GetByIdentifier(lease.DeliveryManId);
                APIValidator.ValidateLease(lease, deliveryMan.CNHType);

                var leaseDocument = LeaseMapper.JsonToDocumentMapper(lease);
                _leaseImplementation.InsertLease(leaseDocument);

                return StatusCode(201);
            }
            catch (Exception)
            {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }

        [HttpPut("{id}/devolucao")]
        public IActionResult EndLease(string id, [FromBody] LeaseReturn request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id) || request == null)
                    return BadRequest(new { mensagem = "Dados inválidos" });

                LeaseDocument lease = _leaseImplementation.GetById(id);
                if (lease == null)
                    return BadRequest(new { mensagem = "Dados inválidos" });

                int selectedPlan = lease.Plan;
                var dailyFee = selectedPlan switch
                {
                    7 => 30m,
                    15 => 28m,
                    30 => 22m,
                    45 => 20m,
                    50 => 18m,
                    _ => throw new Exception()
                };

                var effectiveDays = (request.DevolutionDay.Date - lease.StartDate.Date).Days;
                if (effectiveDays < 1) effectiveDays = 1;

                decimal totalValue = 0m;

                if (request.DevolutionDay.Date < lease.ExpectedEndDate.Date)
                {
                    var unusedDays = selectedPlan - effectiveDays;
                    if (unusedDays < 0) unusedDays = 0;
                    var dailyFeeValue = effectiveDays * dailyFee;
                    decimal multa = 0m;
                    if (selectedPlan == 7)
                        multa = unusedDays * dailyFee * 0.2m;
                    else if (selectedPlan == 15)
                        multa = unusedDays * dailyFee * 0.4m;
                    totalValue = dailyFeeValue + multa;
                }
                else if (request.DevolutionDay.Date > lease.ExpectedEndDate.Date)
                {
                    var extraDays = (request.DevolutionDay.Date - lease.ExpectedEndDate.Date).Days;
                    totalValue = (selectedPlan * dailyFee) + (extraDays * 50m);
                }
                else
                {
                    totalValue = selectedPlan * dailyFee;
                }

                return Ok(new { mensagem = "Data de devolução informada com sucesso", valor_total = totalValue });
            }
            catch
            {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetLeaseById(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                    return BadRequest(new { mensagem = "Dados inválidos" });

                LeaseDocument leaseDoc = _leaseImplementation.GetById(id);
                
                if (leaseDoc == null)
                    return NotFound(new { mensagem = "Locação não encontrada" });

                Lease lease = LeaseMapper.DocumentToJsonMapper(leaseDoc);
                return Ok(lease);
            }
            catch
            {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }
    }
}