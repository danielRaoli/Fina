using Fina.Core.Entities;
using Fina.Core.Requests.Categories;
using Fina.Core.Requests.Transactions;
using Fina.Core.Response;

namespace Fina.Core.Handlers
{
    public interface ITransactionHandler
    {
        Task<Response<Transaction>> GetByIdAsync(GetTransactionByIdRequest request);
        Task<Response<Transaction>> DeleteAsync(DeleteTransactionRequest request);
        Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request);
        Task<Response<Transaction>> UpdateAsync(UpdateTransactionRequest request);
        Task<Response<Transaction>> CreateAsync(CreateTransactionRequest request);
    }
}
