using AutoMapper;
using Server.DTOs.Payment;
using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using Server.Utils;

namespace Server.Services.Implementations;

public class PaymentService(IUnitOfWork unitOfWork, IMapper mapper) : IPaymentService
{
    public async Task<IEnumerable<GetPayment>> GetAllAsync()
    {
        var payments = await unitOfWork.Payments.GetAllAsync();
        return mapper.Map<IEnumerable<GetPayment>>(payments);
    }

    public async Task<GetPayment?> GetByIdAsync(int id)
    {
        var payment = await unitOfWork.Payments.GetByIdAsync(id);
        return mapper.Map<GetPayment>(payment);
    }

    public async Task<ServiceResponse> AddAsync(CreatePayment payment)
    {
        var entity = mapper.Map<Payment>(payment);
        unitOfWork.Payments.AddAsync(entity);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Pago registrado correctamente.")
            : new ServiceResponse(false, "No se pudo registrar el pago.");
    }

    public async Task<ServiceResponse> UpdateAsync(int id, UpdatePayment payment)
    {
        var existing = await unitOfWork.Payments.GetByIdAsync(id);

        mapper.Map(payment, existing);
        if (existing != null) unitOfWork.Payments.UpdateAsync(existing);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Pago actualizado correctamente.")
            : new ServiceResponse(false, "No se pudo actualizar el pago.");
    }

    public async Task<ServiceResponse> DeleteAsync(int id)
    {
        var existing = await unitOfWork.Payments.GetByIdAsync(id);

        if (existing != null) unitOfWork.Payments.DeleteAsync(existing.PaymentId);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Pago eliminado correctamente.")
            : new ServiceResponse(false, "No se pudo eliminar el pago.");
    }
}