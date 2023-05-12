﻿using AutoMapper;
using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Repositories
{
    public class InvoiceRepository : RepositoryBase<Invoice>, IInvoiceRepository
    {
        private readonly IMapper _mapper;

        public InvoiceRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task DeleteInvoiceAsync(Invoice invoice)
        {
            await DeleteAsync(invoice);
        }

        public async Task<List<Invoice>> GetAllInvoiceAsync()
        {
            return await GetAllWithIncludesAsync(i => i.Customer!);
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(Guid id)
        {
            var result = await _dbSet.Include(i => i.Customer).FirstOrDefaultAsync(i => i.IdInvoice == id);
            return result;
        }

        public async Task<Invoice> InsertInvoiceAsync(InvoiceDTO invoice)
        {
            var newInvoice = new Invoice
            {
                TimeCreate = DateTime.UtcNow,
                Total = invoice.Total,
                IdCustomer = invoice.IdCustomer
            };
            await InsertAsync(newInvoice);
            return newInvoice;
        }

        public async Task UpdateInvoiceAsync(Invoice oldInvoice, InvoiceDTO newInvoice)
        {
            _mapper.Map(newInvoice, oldInvoice);
            await UpdateAsync(oldInvoice);
        }
    }
}
