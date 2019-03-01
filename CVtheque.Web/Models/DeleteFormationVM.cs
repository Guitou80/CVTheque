using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVtheque.Web.Models
{
    public class DeleteFormationVM
    {

        public int Id { get; set; }
        public bool ToBeDeleted { get; set; }

        public DeleteFormationVM()
        {
            ToBeDeleted = false;
        }

    }
}