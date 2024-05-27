using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models.Counters
{
    public enum CounterType
    {
        Task = 1,
        FeeNote = 2,
        Assessment = 3,
    }
    [Table("xpa_Counter")]
    public class AppCounter
    {
        public Guid Id { get; set; }
        public CounterType CounterType { get; set; }
        public int Value { get; set; }

    }
}
