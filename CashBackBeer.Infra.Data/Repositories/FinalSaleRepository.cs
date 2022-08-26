using CashbackBeer.Domain.Pagination;
using CashbackBeer.Infra.Data.Context;
using CashBackBeer.Domain.Entities.FinalSaleAggregate;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CashbackBeer.Infra.Data.Repositories
{
    public class FinalSaleRepository : IFinalSaleRepository
    {
        ApplicationDbContext _context;

        public FinalSaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Beer> CreateBeerAsync(Beer beer, CancellationToken cancellationToken)
        {
            _context.Beers.Add(beer);
            await _context.SaveChangesAsync(cancellationToken);
            return beer;
        }

        public async Task<IEnumerable<Beer>> GetBeersByTypeAsync(BeerType beerType, CancellationToken cancellationToken)
        {
            IQueryable<Beer> beers;
            beers = _context.Beers.AsNoTracking().Where(p => p.Type == beerType).OrderBy(p => p.Id);

            return beers;
        }

        public async Task<Beer> GetBeerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Beers.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Beer> GetBeerTypeAndWeekDayAsync(BeerType type, DayOfWeek dayOfWeek, CancellationToken cancellationToken)
        {
            return await _context.Beers.FirstOrDefaultAsync(p => p.Type == type && p.CashBack.Day == dayOfWeek);
        }

        public async Task<FinalSale> CreateFinalSaleAsync(FinalSale finalSale, CancellationToken cancellationToken)
        {
            _context.FinalSales.Add(finalSale);
            await _context.SaveChangesAsync(cancellationToken);
            return finalSale;
        }

        public async Task<FinalSale> GetFinalSaleByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.FinalSales
                .Include(h => h.PartialSales)
                .ThenInclude(h => h.Beer)
                .ThenInclude(h => h.CashBack)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public GeneralPagination<FinalSale> GetFinalSaleAll(PaginationParams pagination, CancellationToken cancellationToken)
        {
            IQueryable<FinalSale> sales = _context.FinalSales.AsNoTracking().Include(h => h.PartialSales).OrderBy(p => p.Id);

            return GetPaginationFinalSale(pagination, sales);
        }

        //public GeneralPagination<FinalSale> GetFinalSaleByDate(PaginationParams pagination, DateTime? minDate, DateTime? maxDate)
        //{

        //    IQueryable<FinalSale> sales = _finalSaleContext.FinalSales.Include(h => h.PartialSales);
        //    if (minDate.HasValue)
        //    {
        //        sales = sales.Where(p => p.DateSale >= minDate).OrderBy(p => p.DateSale);
        //    }
        //    if (maxDate.HasValue)
        //    {
        //        DateTime date = (DateTime)maxDate;
        //        date = date.AddHours(23).AddMinutes(59).AddSeconds(59);
        //        sales = sales.Where(p => p.DateSale <= date);
        //    }

        //    return GetPaginationFinalSale(pagination, sales);
        //}

        public GeneralPagination<FinalSale> GetPaginationFinalSale(PaginationParams pagination, IQueryable<FinalSale> sales)
        {
            int pageAmount = (int)Math.Ceiling((sales.Count()) / ((double)pagination.Limit));
            List<FinalSale> getSales = sales
                .Skip(pagination.GetOffset())
                .Take(pagination.Limit)
                .ToList();

            return new GeneralPagination<FinalSale>(pageAmount: pageAmount, items: getSales.ToArray());
        }

        public async Task<IEnumerable<Beer>> GetBeersAsync()
        {
            return await _context.Beers.ToListAsync();
        }
             
        public async Task<Beer> RemoveBeerAsync(Beer beer, CancellationToken cancellationToken)
        {
            _context.Remove(beer);
            await _context.SaveChangesAsync();
            return beer;
        }

        public async Task<Beer> UpdateBeerAsync(Beer beer, CancellationToken cancellationToken)
        {
            _context.Update(beer);
            await _context.SaveChangesAsync();
            return beer;
        }

    }
}
