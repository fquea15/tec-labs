using Server.DTOs.Payment;
using Server.Utils;

namespace Server.Services.Interfaces;

public interface IPaymentService
{
    Task<IEnumerable<GetPayment>> GetAllAsync();
    Task<GetPayment?> GetByIdAsync(int id);
    Task<ServiceResponse> AddAsync(CreatePayment payment);
    Task<ServiceResponse> UpdateAsync(int id, UpdatePayment payment);
    Task<ServiceResponse> DeleteAsync(int id);
}