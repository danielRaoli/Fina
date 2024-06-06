using Fina.Core.Entities;
using Fina.Core.Handlers;
using Fina.Core.Requests.Transactions;
using Fina.Core.Response;
using System.Net.Http.Json;
using System.Runtime.InteropServices;

namespace Fina.App.Handlers
{
    public class TransactionHandler(IHttpClientFactory clientFactory) : ITransactionHandler
    {
        private readonly HttpClient _httpClient = clientFactory.CreateClient(AppConfiguration.HttpClientName);
        private const string endpoint = "v1/transactions";
        public async Task<Response<Transaction>> CreateAsync(CreateTransactionRequest request)
        {
            
            var result = await _httpClient.PostAsJsonAsync(endpoint, request);

            return await result.Content.ReadFromJsonAsync<Response<Transaction?>>() ?? new Response<Transaction?>(null, 400, "Nao foi possivel criar uma nova transacao");
        }

        public async Task<Response<Transaction>> DeleteAsync(DeleteTransactionRequest request)
        {
            var result = await _httpClient.DeleteAsync(endpoint + $"{request.Id}");

            return await result.Content.ReadFromJsonAsync<Response<Transaction?>>() ?? new Response<Transaction?>(null, 400, "Nao foi possivel excluir a categoria");
        }

        public async Task<Response<Transaction>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            var result = await _httpClient.GetAsync(endpoint+$"{request.Id}");

            return await result.Content.ReadFromJsonAsync<Response<Transaction?>>() ?? new Response<Transaction?>(null, 400, "erro ao tentar recuperar categoria");
        }

        public async Task<PagedResponse<List<Transaction>>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            var result = await _httpClient.GetAsync(endpoint);

            return await result.Content.ReadFromJsonAsync<PagedResponse<List<Transaction>?>>() ?? new PagedResponse<List<Transaction>?>(null, 400, "erro ao recuperar as transacoes");
        }

        public async Task<Response<Transaction>> UpdateAsync(UpdateTransactionRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync(endpoint + $"{request.Id}", request);

            return await result.Content.ReadFromJsonAsync<Response<Transaction?>>() ?? new Response<Transaction?>(null, 400, "erro ao atualizar transacao");
        }
    }
}
