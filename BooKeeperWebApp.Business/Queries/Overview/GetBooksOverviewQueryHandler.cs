using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models.Overview;
using BooKeeperWebApp.Infrastructure.Repositories;

namespace BooKeeperWebApp.Business.Queries.Overview;
public class GetBooksOverviewQueryHandler : IHandler<GetBooksOverviewQuery, IEnumerable<OverviewBookModel>>
{
    private readonly IGenericRepository<Infrastructure.Entities.Bank.Book> _bookRepository;

    public GetBooksOverviewQueryHandler(IGenericRepository<Infrastructure.Entities.Bank.Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<OverviewBookModel>> ExecuteAsync(GetBooksOverviewQuery query)
    {
        var books = await _bookRepository.GetAsync(x => x.UserId == query.UserId, null, "Mutations");

        var retVal = new List<OverviewBookModel>();

        Parallel.ForEach(books, book =>
        {
            var overviewBook = new OverviewBookModel
            {
                BookName = book.Name!,
                JanuariAmount = book.Mutations != null ? book.Mutations.Where(x => x.Date.Year == query.Date.Year && x.Date.Month == 1).Sum(x => x.Amount) : 0,
                FebruariAmount = book.Mutations != null ? book.Mutations.Where(x => x.Date.Year == query.Date.Year && x.Date.Month == 2).Sum(x => x.Amount) : 0,
                MarchAmount = book.Mutations != null ? book.Mutations.Where(x => x.Date.Year == query.Date.Year && x.Date.Month == 3).Sum(x => x.Amount) : 0,
                AprilAmount = book.Mutations != null ? book.Mutations.Where(x => x.Date.Year == query.Date.Year && x.Date.Month == 4).Sum(x => x.Amount) : 0,
                MayAmount = book.Mutations != null ? book.Mutations.Where(x => x.Date.Year == query.Date.Year && x.Date.Month == 5).Sum(x => x.Amount) : 0,
                JuneAmount = book.Mutations != null ? book.Mutations.Where(x => x.Date.Year == query.Date.Year && x.Date.Month == 6).Sum(x => x.Amount) : 0,
                JulyAmount = book.Mutations != null ? book.Mutations.Where(x => x.Date.Year == query.Date.Year && x.Date.Month == 7).Sum(x => x.Amount) : 0,
                AugustAmount = book.Mutations != null ? book.Mutations.Where(x => x.Date.Year == query.Date.Year && x.Date.Month == 8).Sum(x => x.Amount) : 0,
                SeptemberAmount = book.Mutations != null ? book.Mutations.Where(x => x.Date.Year == query.Date.Year && x.Date.Month == 9).Sum(x => x.Amount) : 0,
                OctoberAmount = book.Mutations != null ? book.Mutations.Where(x => x.Date.Year == query.Date.Year && x.Date.Month == 10).Sum(x => x.Amount) : 0,
                NovemberAmount = book.Mutations != null ? book.Mutations.Where(x => x.Date.Year == query.Date.Year && x.Date.Month == 11).Sum(x => x.Amount) : 0,
                DecemberAmount = book.Mutations != null ? book.Mutations.Where(x => x.Date.Year == query.Date.Year && x.Date.Month == 12).Sum(x => x.Amount) : 0,
                TotalAmount = book.Mutations != null ? book.Mutations.Where(x => x.Date.Year == query.Date.Year).Sum(x => x.Amount) : 0,
            };
            retVal.Add(overviewBook);
        });

        return retVal;
    }
}
