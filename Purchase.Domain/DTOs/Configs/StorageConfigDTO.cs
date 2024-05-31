using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.DTOs.Configs
{
    public class StorageConfigDTO : ConfigBaseDTO
    {



        [Required]
        public string UploadPath { get; set; }

        [Required]
        public string OtherPath { get; set; }
        [Required]
        public string DocumentPath { get; set; }
    }
}
