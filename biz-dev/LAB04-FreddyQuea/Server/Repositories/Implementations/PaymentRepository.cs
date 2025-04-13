using Server.Data;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Implementations;

public class PaymentRepository(ApplicationDbContext context) : GenericRepository<Payment>(context), IPaymentRepository
{
}