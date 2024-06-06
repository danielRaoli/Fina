using Fina.API.Data;
using Fina.Core.Common;
using Fina.Core.Entities;
using Fina.Core.Handlers;
using Fina.Core.Requests.Transactions;
using Fina.Core.Response;
using Microsoft.EntityFrameworkCore;

namespace Fina.API.Handlers
{
    public class TransactionHandler : ITransactionHandler
    {
        private readonly AppDbContext _context;

        public TransactionHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Response<Transaction>> CreateAsync(CreateTransactionRequest request)
        {
            try
            {
               
                if (request.Type == Core.Enums.TransactionType.WithDraw)
                    request.Amount *= -1;

                var transaction = new Transaction
                {
                    Title = request.Title,
                    Amount = request.Amount,
                    Type = request.Type,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                    CategoryId = request.CategoryId,
                    UserId = request.UserId,
                };

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return new Response<Transaction>(transaction, 201, "Transacao criada com sucesso");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Erro ao tentar criar nova transacao");
            }

        }

        public async Task<Response<Transaction>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
             
                var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "transacao nao encontrada");

                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, message: "transacao removida com sucesso");



            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Erro ao tentar remover transacao");
            }
        }

        public async Task<Response<Transaction>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
               
                var transaction = await _context.Transactions.AsNoTracking().FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

                return transaction is null
                    ? new Response<Transaction?>(null, 404, "transacao nao encontrada")
                    : new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Erro ao tentar remover transacao");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "nao foi possivel determinar a data de inicio ou termino");
            }
            try
            {
                var query = _context.Transactions.AsNoTracking().Where(t =>
                t.PaidOrReceivedAt >= request.StartDate
                && t.PaidOrReceivedAt <= request.EndDate
                && t.UserId == request.UserId);

                var transactions = await query.Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize).ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(transactions, count, request.PageSize, request.PageNumber);
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "nao foi possivel obter as transacoes");
            }
        }

        public async Task<Response<Transaction>> UpdateAsync(UpdateTransactionRequest request)
        {
            try
            {
                
                var transaction = await _context.Transactions.FirstOrDefaultAsync(t=> t.Id == request.Id && t.UserId == request.UserId);

                if (request.Type == Core.Enums.TransactionType.WithDraw && request.Amount > 0)
                {
                    request.Amount *= -1;
                }

                if (transaction is null)
                    return new Response<Transaction?>(null,404,"transacao nao encontrada");

                transaction.CategoryId = request.CategoryId;
                transaction.Title = request.Title;
                transaction.Amount = request.Amount;
                transaction.Type = request.Type;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();

                return new Response<Transaction>(transaction, message: "Transacao atualizada com sucesso");
            }
            catch
            {
                return new Response<Transaction>(null, 500, "Nao foi possivel atualizar a transacao");
            }
        }
    }
}
