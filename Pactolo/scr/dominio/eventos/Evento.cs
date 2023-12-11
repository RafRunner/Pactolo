using Pactolo.scr.dominio.eventos;
using Pactolo.scr.services;
using System;

namespace Pactolo.scr.dominio {
    public class Evento {

        public Evento(string mensagem) {
            Mensagem = mensagem;
            HoraEvento = DateTime.Now;
            Tipo = TipoEvento.Outros;
        }

        public Evento(string mensagem, TipoEvento tipo) : this(mensagem) {
            Tipo = tipo;
        }

        public DateTime HoraEvento { get; set; }
        // -2 não é evento de toque em SC, -1 neutro, 0 erro, 1 acerto
        public TipoEvento Tipo { get; set; }
        public string Mensagem { get; set; }

        public string MontaMensagem() {
            return HoraEvento.ToString(RelatorioSessaoService.FORMATO_HORA) + ": " + Mensagem;
		}
    }
}
