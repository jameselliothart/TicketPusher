using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TicketPusher.API.Data;
using TicketPusher.Domain.Common;

namespace TicketPusher.API.Utils
{
    public static class ITicketPusherRepositoryExtensions
    {
        public static async Task<Result<T, Error>> EntityOrNotFound<T>(this ITicketPusherRepository repo, Guid id, Func<Guid, Task<T>> GetEntityAsync)
            where T: Entity
        {
            T entity = await GetEntityAsync(id);
            return Result.SuccessIf(entity != null, entity, Errors.General.NotFound(nameof(T), id));
        }
    }
}