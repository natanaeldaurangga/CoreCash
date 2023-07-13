using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Entities;

namespace CoreCashApi.Data.SeedEntity
{
    public class CashSeed
    {
        public Record[]? Records { get; set; }

        public Ledger[]? Ledgers { get; set; }
    }
}