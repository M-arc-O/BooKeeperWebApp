using System.Net;
using AutoMapper;
using BooKeeperWebApp.Business.Commands.Book;
using BooKeeperWebApp.Business.CQRS;
using BooKeeperWebApp.Business.Models;
using BooKeeperWebApp.Business.Queries.Book;
using BooKeeperWebApp.Business.Services;
using BooKeeperWebApp.Shared.Dtos;
using BooKeeperWebApp.Shared.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Api
{
    public class BookFunctions : FunctionBase
    {
        private readonly IExecutor _excecutor;
        private readonly IMapper _mapper;

        public BookFunctions(IExecutor executor, IUserService userService, IMapper mapper)
            : base(userService)
        {
            _excecutor = executor;
            _mapper = mapper;
        }

        [Function("GetBooks")]
        public async Task<HttpResponseData> GetBankAccountsAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "book/getall")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            var user = await GetUserAsync(req);
            var query = new GetAllBooksQuery(user.Id);
            var books = await _excecutor.ExecuteAsync<GetAllBooksQuery, IEnumerable<BookModel>>(query);
            await response.WriteAsJsonAsync(books.Select(x => _mapper.Map<BookDto>(x)));

            return response;
        }

        [Function("CreateBook")]
        public async Task<HttpResponseData> CreateBookAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "book/create")] HttpRequestData req)
        {
            var book = await req.ReadFromJsonAsync<AddBookModel>() ?? throw new Exception();

            var user = await GetUserAsync(req);

            var command = new AddBookCommand(user.Id, book.Name);
            var result = await _excecutor.ExecuteAsync<AddBookCommand, BookModel>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(result);

            return response;
        }

        [Function("UpdateBook")]
        public async Task<HttpResponseData> UpdateBookAsync([HttpTrigger(AuthorizationLevel.Function, "put", Route = "book/{id}/update")] HttpRequestData req, Guid id)
        {
            var book = await req.ReadFromJsonAsync<AddBookModel>() ?? throw new Exception();

            var user = await GetUserAsync(req);

            var command = new UpdateBookCommand(user.Id, id, book.Name);
            var result = await _excecutor.ExecuteAsync<UpdateBookCommand, BookModel>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(result);

            return response;
        }

        [Function("DeleteBook")]
        public async Task<HttpResponseData> DeleteBookAsync([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "book/{id}/delete")] HttpRequestData req, Guid id)
        {
            var user = await GetUserAsync(req);

            var command = new DeleteBookCommand(user.Id, id);
            var result = await _excecutor.ExecuteAsync<DeleteBookCommand, Guid>(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(result);

            return response;
        }
    }
}
