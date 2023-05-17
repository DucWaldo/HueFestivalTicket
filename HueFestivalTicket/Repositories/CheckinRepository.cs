using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Repositories
{
    public class CheckinRepository : RepositoryBase<Checkin>, ICheckinRepository
    {
        public CheckinRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Checkin>> GetAllCheckinAsync()
        {
            return await GetAllWithIncludesAsync(c => c.Account!.Role!);
        }

        public async Task<Checkin?> GetCheckinAsync(string qrCodeContent)
        {
            var checkin = await _dbSet.FirstOrDefaultAsync(c => c.QRCodeContent == qrCodeContent);
            return checkin;
        }

        public async Task<Checkin?> GetCheckinByIdAsync(Guid id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(c => c.IdCheckin == id);
            return result;
        }

        public async Task<object> GetCheckinPagingAsync(int pageNumber, int pageSize)
        {
            List<Checkin> data = await GetPage(pageNumber, pageSize, c => c.TimeCheckin, c => c.Account!.Role!);
            return ReturnGetPage(data, pageNumber, pageSize);
        }

        public async Task<Checkin> InsertCheckinAsync(CheckinDTO Checkin, Guid IdAccount, bool status)
        {
            var newCheckin = new Checkin()
            {
                TimeCheckin = DateTime.UtcNow,
                IdAccount = IdAccount,
                Status = status,
                QRCodeContent = Checkin.QRCodeContent
            };
            await InsertAsync(newCheckin);
            return newCheckin;
        }
    }
}
