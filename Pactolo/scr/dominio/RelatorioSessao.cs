using System;
using System.Collections.Generic;
using System.IO;
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
        private string PASTA_RELATORIOS = "Relatorios";

        //arrumar esse nome default
        private string getNomeArquivo() {
            return nomeArquivoDigitado != null && nomeArquivoDigitado != "" ? nomeArquivoDigitado : $"Relatorio_Pactolo_{experimentador.Nome}_{participante.Nome}_{horaInicio.ToString()}";
        }

        public string getPath() {
            return Path.GetDirectoryName(Directory.GetCurrentDirectory()) + "\\" + PASTA_RELATORIOS + getNomeArquivo();
        }
    }
}
