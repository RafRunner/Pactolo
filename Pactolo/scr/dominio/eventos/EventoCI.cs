using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio.eventos {
    class EventoCI : Evento {

        private readonly ContingenciaInstrucional CI;
        private readonly string nomeTato;

        public EventoCI(Sessao sessao, ContingenciaInstrucional CI, UnidadeDoExperimento tato) : base(sessao) {
            this.CI = CI;
			for (int i = 0; i < CI.Tatos.Count; i++) {
				if (CI.Tatos[i].Id == tato.Id) {
					nomeTato = "Tato " + (i + 1).ToString();
					break;
				}
			}
        }

        public override string MontaMensagem() {
            return $"{NomeSesssao} | {nomeTato} da CI {CI.Nome} tocado! | {HoraEvento.ToString("HH:mm:ss")}";
        }
    }
}
