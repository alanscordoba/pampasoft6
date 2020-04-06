using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pampasoft6.Models
{
    public partial class provincias
    {
        public int id { get; set; }

        [StringLength(100)]
        public string nombre { get; set; }
    }
}