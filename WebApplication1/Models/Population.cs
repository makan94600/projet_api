using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Population
    {
        public int Id { get; set; }


        public string annee { get; set; }

        public ICollection<Individu> Individus { get; set; }

        public Pays pays2 { get; set; }

    }
}
