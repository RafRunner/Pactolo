using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {
    class Evento {
        public string NomeSesssao { get; set; }
        public string NomeCC  { get; set; }
        public string NomeSC { get; set; } // SC1, SC2 ou SC3
        public long pontosGanhos { get; set; }
        public long pontosAtuais { get; set; }
        public int tentativaAtual { get; set; }
        public DateTime horaEvento { get; set; }
        public Boolean eventoEncerramento { get; set; }
        public string criterioEncerramento { get; set; }
        public string valorEncerramento { get; set; }
    }
}
