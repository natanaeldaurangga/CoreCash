using CoreCashApi.Data;
using CoreCashApi.DTOs.Contacts;
using CoreCashApi.DTOs.Pagination;
using CoreCashApi.Entities;
using CoreCashApi.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CoreCashApi.Services
{
    public class ContactService
    {
        private readonly AppDbContext _dbContext;

        private readonly ILogger<ContactService> _logger;

        public ContactService(AppDbContext dbContext, ILogger<ContactService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ResponsePagination<ResponseContact>?> GetContactResponsePagedAsync(Guid userId, RequestPagination request, TrashFilter trashFilter = TrashFilter.WITHOUT_TRASHED)
        {
            var query = _dbContext.Contacts!.Include(ct => ct.User).AsQueryable();

            query = query.Where(ct => ct.UserId.Equals(userId));

            query = trashFilter switch
            {
                TrashFilter.WITHOUT_TRASHED => query.Where(rc => rc.DeletedAt == default),
                TrashFilter.ONLY_TRASHED => query.Where(rc => rc.DeletedAt != default),
                _ => query
            };

            var sortBy = request.SortBy;
            var direction = request.Direction;

            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(sortBy) && direction != null)
            {
                var sortExpression = $"{sortBy} {direction}";
                query = query.OrderBy(sortExpression);
            }

            query = query.Take(500);

            // TODO: Lanjut untuk bikin pagination buat contacts

            return null;
        }

        public async Task<int> InsertNewContactAsync(Guid userId, RequestContactCreate request)
        {
            try
            {
                var contact = new Contact()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Name = request.Name,
                    PhoneNumber = request!.PhoneNumber!,
                    Address = request!.Address!,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _dbContext.Contacts!.Add(contact);
                await _dbContext.SaveChangesAsync();

                return 1;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}