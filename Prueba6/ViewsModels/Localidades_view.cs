using pampasoft6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pampasoft6.ViewsModels
{
    public class Localidades_view : ModeloPaginador
    {
        public List<localidades> Localidades { get; set; }
    }
}