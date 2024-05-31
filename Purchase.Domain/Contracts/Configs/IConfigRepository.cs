using Purchase.Domain.Models.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Contracts.Configs
{
    public interface IConfigRepository
    {
        void Save<T>(T entity) where T : ConfigBase;
        Task<T> Load<T>() where T : ConfigBase;
    }
}
