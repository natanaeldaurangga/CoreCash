using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Data;

namespace CoreCashApi.Validations
{
    public class ContactExists : ValidationAttribute
    {
        // FIXME: Kenapa nge return terus contact id tidak exists
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dbContext = (AppDbContext)validationContext.GetService(typeof(AppDbContext))!;

            var contactId = value as string;

            var contact = dbContext.Contacts!.Where(ct => ct.Id.ToString().Equals(contactId) && ct.DeletedAt == null).FirstOrDefault();

            if (contact == default)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}