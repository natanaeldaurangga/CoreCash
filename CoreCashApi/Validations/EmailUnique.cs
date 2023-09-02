using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Data;

namespace CoreCashApi.Validations
{
    public class EmailUnique : ValidationAttribute
    {
        public bool MustUnique { get; set; } = true;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dbContext = (AppDbContext)validationContext.GetService(typeof(AppDbContext))!;

            var email = value as string;

            var isUnique = !dbContext.Users!.Any(u => u.Email.Equals(email) && u.VerifiedAt != default);

            if (!isUnique && MustUnique)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}