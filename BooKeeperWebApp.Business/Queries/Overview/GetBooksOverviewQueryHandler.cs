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
                Amount = book.Mutations != null ? book.Mutations.Where(x => x.Date.Year == query.Date.Year).Sum(x => x.Amount) : 0,
            };
            retVal.Add(overviewBook);
        });

        return retVal;
    }
}
