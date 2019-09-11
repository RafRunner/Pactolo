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
            if (CI.Tato1.Id == tato.Id) {
                nomeTato = "Tato1";
            }
            else if (CI.Autoclitico.Id == tato.Id) {
                nomeTato = "Autoclítico";
            }
            else {
                nomeTato = "Tato2";
            }
        }

        public override string MontaMensagem() {
            return $"{NomeSesssao} | {nomeTato} da CI {CI.Nome} tocado! | {HoraEvento.ToString("HH:mm:ss")}";
        }
    }
}
