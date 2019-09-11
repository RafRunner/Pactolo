using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio.eventos {
    class EventoSModelo : Evento {

        public EventoSModelo(Sessao sessao, ContingenciaColateral CC, string nomeImagem) : base(sessao) {
            this.CC = CC;
            this.nomeImagem = nomeImagem;
        }

        private readonly ContingenciaColateral CC;
        private readonly string nomeImagem;

        public override string MontaMensagem() {
            return $"{NomeSesssao} | sModelo de imagem {nomeImagem} da CC {CC.Nome} tocado! | {HoraEvento.ToString("HH:mm:ss")}";
        }
    }
}
