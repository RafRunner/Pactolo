using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {
    public abstract class Evento {

        public Evento(Sessao sessao) {
            NomeSesssao = sessao.Nome;
            HoraEvento = DateTime.Now;
        }

        public string NomeSesssao { get; set; }
        public DateTime HoraEvento { get; set; }
        public Boolean EventoEncerramento { get; set; }

        public abstract string MontaMensagem();
    }
}
