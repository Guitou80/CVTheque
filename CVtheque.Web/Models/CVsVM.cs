using CVtheque.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVtheque.Web.Models
{
    public class CVsVM
    {
        public IEnumerable<CVVM> CVs { get; set; }

    }
}