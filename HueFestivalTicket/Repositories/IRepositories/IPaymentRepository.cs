using HueFestivalTicket.Data;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface IPaymentRepository
    {
        public string Payment(Guid IdInvoice, decimal total, TicketDTO ticket);
    }
}
