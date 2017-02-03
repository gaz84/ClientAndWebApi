﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClient.Models
{
    public class Client
    {
        public int id { get; set; }
        [Required(ErrorMessage = "This field can't be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field can't be empty")]
        public string Surname { get; set; }

        public virtual IEnumerable<Credit> Credits { get; set; }
    }
}
