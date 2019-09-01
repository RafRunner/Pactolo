using System;
using System.Collections.Generic;
using System.Text;

namespace Pactolo.scr.dominio {
    class RelatorioSessao {
        public Participante participante { get; set; }
        public Experimentador experimentador { get; set; }
        public List<long> IdSessoesSelecionadas { get; set; }
        public DateTime horaInicio { get; set; }
        public DateTime horaFim { get; set; }
        public string nomeArquivoDigitado { get; set; }
        public List<Evento> eventos { get; set; }

        //arrumar esse nome default
        public string getNomeArquivo() {
            return nomeArquivoDigitado != null && nomeArquivoDigitado != "" ? nomeArquivoDigitado : "nome defaut";
        }

        public string getPath() {
            return "caminho" + getNomeArquivo();
        }
    }
}
