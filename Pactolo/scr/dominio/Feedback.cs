using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	public class Feedback : ElementoDeBanco {

		public int ValorClick { get; set; }
		public Boolean Neutro { get; set; }
        public Boolean SemCor { get; set; }
        public int ProbabilidadeComplementar { get; set; }
    }
}
