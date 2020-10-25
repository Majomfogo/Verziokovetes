using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gyak7_mikroszim.Entities
{
   public class Person
    {
        public int SzulEv { get; set; }
        public Gender Nem { get; set; }
        public int Gyerekszam { get; set; }
        public bool IsAlive { get; set; }

        public Person()
        {
            IsAlive = true;
        }
    }
}
