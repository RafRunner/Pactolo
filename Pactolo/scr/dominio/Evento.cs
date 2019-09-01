using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {
    class Evento {
        public string NomeSesssao { get; set; }
        public string NomeCC  { get; set; }
        public long pontosGanhos { get; set; }
        DateTime horaEvento { get; set; }
    }
}
