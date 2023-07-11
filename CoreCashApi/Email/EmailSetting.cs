using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.Email
{
    public class EmailSetting
    {
        public string? FromDisplayName { get; set; }

        public string? From { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Host { get; set; }

        public int Port { get; set; }
    }
}