﻿using AutoMapper;
using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Repositories
{
    public class SupportRepository : RepositoryBase<Support>, ISupportRepository
    {
        private readonly IMapper _mapper;
        public SupportRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task DeleteSupportAsync(Support support)
        {
            await DeleteAsync(support);
        }

        public async Task<List<Support>> GetAllSupportAsync()
        {
            return await GetAllWithIncludesAsync(s => s.Account!.Role!);
        }

        public async Task<Support?> GetSupportByIdAsync(Guid id)
        {
            var support = await _dbSet.FirstOrDefaultAsync(s => s.IdSuport == id);
            return support;
        }

        public async Task<Support?> GetSupportByTitleAsync(string title)
        {
            var support = await _dbSet.FirstOrDefaultAsync(s => s.Title == title);
            return support;
        }

        public async Task<Support> InsertSupportAsync(SupportDTO support, Guid idAccount)
        {
            var newSupport = new Support
            {
                Title = support.Title,
                Content = support.Content,
                IdAccount = idAccount
            };
            await InsertAsync(newSupport);
            return newSupport;
        }

        public async Task UpdateSupportAsync(Support oldSupport, SupportDTO newSupport)
        {
            _mapper.Map(newSupport, oldSupport);
            await UpdateAsync(oldSupport);
        }
    }
}
