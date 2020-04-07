using Pampasoft6.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pampasoft6.Models
{
    public partial class provincias : TablaBase
    {
        [StringLength(100)]
        public string Nombre { get; set; }
    }
}