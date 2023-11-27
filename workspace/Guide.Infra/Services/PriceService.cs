using Guide.Domain.Entities;
using Guide.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Infra.Services
{
    public class PriceService
    {
        private readonly GuideContext _context;

        public PriceService(GuideContext context)
        {
            _context = context;
        }
        public async Task<List<PriceRecord>> GetAllAsync()
        {
            return _context.PriceRecords.ToList();
        }
        public async Task<int> CreateAsync(PriceRecord entity)
        {
            _context.PriceRecords.Add(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> RemoveAsync(PriceRecord entity)
        {
            _context.PriceRecords.Remove(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<List<PriceRecord>> GetForIdAsync(int id)
        {
            return _context.PriceRecords.Where(x => x.Id == id).ToList();
        }

    }
}
