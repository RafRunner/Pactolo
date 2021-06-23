using System;

namespace Pactolo.scr.dominio {
    public class Evento {

        public Evento(string mensagem) {
            Mensagem = mensagem;
            HoraEvento = DateTime.Now;
            Acerto = -2;
        }

        public Evento(string mensagem, int acerto) : this(mensagem) {
            Acerto = acerto;
        }

        public DateTime HoraEvento { get; set; }
        // -2 não é evento de toque em SC, -1 neutro, 0 erro, 1 acerto
        public int Acerto { get; set; }
        public string Mensagem { get; set; }

        public string MontaMensagem() {
            return HoraEvento.ToString("hh:mm:ss") + ": " + Mensagem;
		}
    }
}
