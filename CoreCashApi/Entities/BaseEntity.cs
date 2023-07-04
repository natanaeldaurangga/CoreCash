using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreCashApi.Entities
{
    public abstract class BaseEntity
    {
        [DataType(DataType.DateTime)]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [DefaultValue(null)]
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
    }
}