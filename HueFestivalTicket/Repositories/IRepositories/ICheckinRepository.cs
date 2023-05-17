using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface ICheckinRepository : IRepository<Checkin>
    {
        public Task<List<Checkin>> GetAllCheckinAsync();
        public Task<object> GetCheckinPagingAsync(int pageNumber, int pageSize);
        public Task<Checkin?> GetCheckinByIdAsync(Guid id);
        public Task<Checkin?> GetCheckinAsync(string qrCodeContent);
        public Task<Checkin> InsertCheckinAsync(CheckinDTO Checkin, Guid IdAccount, bool status);
    }
}
