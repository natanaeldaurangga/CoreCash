using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.DTOs.Contacts
{
    public class ResponseContact
    {
        public Guid ContactId { get; set; }

        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
    }
}