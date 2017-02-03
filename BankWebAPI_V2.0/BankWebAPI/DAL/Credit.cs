using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    public class Credit 
    {
        public int id { get; set; }
        public decimal Ammount { get; set; }
        public double Percent { get; set; }
        public string Description { get; set; }
        public DateTime DayOfCredit { get; set; }
        public int ClientId { get; set; }

        //[ForeignKey("ClientId")]
        //public virtual Client Client { get; set; }
    }
}
