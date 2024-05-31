using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models.Configs
{
    public class StorageConfig : ConfigBase
    {

        public override ConfigType ConfigType { get => ConfigType.Storage; }

        [Required]
        public string UploadPath { get; set; }

        [Required]
        public string OtherPath { get; set; }

        public string DocumentPath { get; set; }
    }
}
