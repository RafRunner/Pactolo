using System;
using System.Collections.Generic;

namespace Pactolo.scr.dominio {
    public class RelatorioSessao {

        public RelatorioSessao(List<long> idSessoes, Experimentador experimentador, Participante participante) {
            IdSessoesSelecionadas = idSessoes;
            Participante = participante;
            Experimentador = experimentador;
            HoraInicio = DateTime.Now;
            Eventos = new List<Evento>();
        }

        public Participante Participante { get; set; }
        public Experimentador Experimentador { get; set; }
        public List<long> IdSessoesSelecionadas { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }
        public string NomeArquivoDigitado { get; set; }
        public List<Evento> Eventos { get; set; }

        public void FinalizarExperimento() {
            HoraFim = DateTime.Now;
        }

        public void AdicionarEvento(Evento evento) {
            Eventos.Add(evento);
        }

        public string GetNomeArquivo() {
            return NomeArquivoDigitado != null && NomeArquivoDigitado != "" ? NomeArquivoDigitado : $"Relatorio_Pactolo_{Experimentador.Nome}_{Participante.Nome}_{HoraInicio.ToString("yyyy-MM-dd HH-mm-ss")}";
        }
    }
}
