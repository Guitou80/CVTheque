using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVtheque.Web.Models
{
    public class DeleteCompetenceVM
    {

        public int Id { get; set; }
        public bool ToBeDeleted { get; set; }

        public DeleteCompetenceVM()
        {
            ToBeDeleted = false;
        }

    }
}