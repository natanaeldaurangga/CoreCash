using System;
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

        public async Task<int> ForceDeleteContactAsync(Guid userId, Guid recordId)
        {
            try
            {
                var contact = await _dbContext.Contacts!.FirstOrDefaultAsync(ct => ct.Id.Equals(recordId) && ct.UserId.Equals(userId));

                if (contact == null) return 0;

                _dbContext.Contacts!.Remove(contact!);
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<int> RestoreContactAsync(Guid userId, Guid recordId)
        {
            return await SetDeletedAtAsync(userId, recordId, null);
        }

        public async Task<int> SoftDeleteContactAsync(Guid userId, Guid recordId)
        {
            return await SetDeletedAtAsync(userId, recordId, DateTime.UtcNow);
        }

        private async Task<int> SetDeletedAtAsync(Guid userId, Guid contactId, DateTime? dateTime = null)
        {
            try
            {
                var contact = await _dbContext.Contacts!.FirstOrDefaultAsync(ct => ct.Id.Equals(contactId) && ct.UserId.Equals(userId));

                contact!.DeletedAt = dateTime;

                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<ResponseContact?> GetContactDetailAsync(Guid userId, Guid contactId)
        {
            try
            {
                var contact = await _dbContext.Contacts!.FirstOrDefaultAsync(ct => ct.Id!.Equals(contactId) && ct.UserId.Equals(userId) && ct.DeletedAt == null);

                if (contact == default) return null;

                return new ResponseContact()
                {
                    ContactId = contact.Id,
                    Name = contact.Name,
                    PhoneNumber = contact.PhoneNumber,
                    Address = contact.Address
                };
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<ResponsePagination<ResponseContact>> GetContactResponsePagedAsync(Guid userId, RequestPagination request, TrashFilter trashFilter = TrashFilter.WITHOUT_TRASHED)
        {
            var query = _dbContext.Contacts!.Include(ct => ct.User).AsQueryable();

            query = query.Where(ct => ct.UserId.Equals(userId));

            query = trashFilter switch
            {
                TrashFilter.WITHOUT_TRASHED => query.Where(rc => rc.DeletedAt == default),
                TrashFilter.ONLY_TRASHED => query.Where(rc => rc.DeletedAt != default),
                _ => query
            };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(ct =>
                    ct.Name!.Equals(request.Keyword, StringComparison.OrdinalIgnoreCase) ||
                    ct.PhoneNumber!.Equals(request.Keyword, StringComparison.OrdinalIgnoreCase) ||
                    ct.Email!.Equals(request.Keyword, StringComparison.OrdinalIgnoreCase)
                );
            }

            var sortBy = request.SortBy;
            var direction = request.Direction;

            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(sortBy) && direction != null)
            {
                var sortExpression = $"{sortBy} {direction}";
                query = query.OrderBy(sortExpression);
            }

            query = query.Take(500);

            int totalData = query.Count();

            // ngambil data per page
            query = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize);

            var results = await query
            .Select(ct => new ResponseContact()
            {
                ContactId = ct.Id,
                Name = ct.Name,
                PhoneNumber = ct.PhoneNumber,
                Address = ct.Address
            }).ToListAsync();

            float totalPageFloat = (float)totalData / request.PageSize;
            int totalPage = (int)Math.Ceiling(totalPageFloat);

            return new ResponsePagination<ResponseContact>()
            {
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage,
                TotalPages = totalPage,
                Items = results
            };
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
                    Email = request!.Email!,
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