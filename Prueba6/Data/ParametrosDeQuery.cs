﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace pampasoft6.Data
{
    public class ParametrosDeQuery<T>
    {
        public ParametrosDeQuery(int pagina, int top)
        {
            Pagina = pagina;
            Top = top;
            Include = null;
            Where = null;
            OrderBy = null;
            OrderByDescending = null;
        }

        public int Pagina { get; set; }
        public int Top { get; set; }
        public string Include { get; set; }
        public Expression<Func<T, bool>> Where { get; set; }
        public Func<T, object> OrderBy { get; set; }
        public Func<T, object> OrderByDescending { get; set; }
    }
}