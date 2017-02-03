using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace DAL
{
   public class Client
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        
        public List<Credit> Credits { get; set; }
        //public virtual IEnumerable<Credit> Credits { get; set; }
       
    }
}
