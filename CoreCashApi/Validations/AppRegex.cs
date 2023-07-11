using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.Validations
{
    public class AppRegex : ValidationAttribute
    {
        public string RegexPattern { get; set; } = string.Empty;

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true;

            string inputValue = (string)value!;
            Regex regex = new(RegexPattern);

            return regex.IsMatch(inputValue);
        }
    }
}