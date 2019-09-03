using Pactolo.scr.services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pactolo.scr.dominio {
    public class RelatorioSessao {

        public RelatorioSessao(List<long> idSessoes, Experimentador experimentador, Participante participante) {
            IdSessoesSelecionadas = idSessoes;
            Participante = participante;
            Experimentador = experimentador;
            HoraInicio = new DateTime();
        }

        public Participante Participante { get; set; }
        public Experimentador Experimentador { get; set; }
        public List<long> IdSessoesSelecionadas { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }
        public string NomeArquivoDigitado { get; set; }
        public List<Evento> eventos { get; set; }
        private string PASTA_RELATORIOS = "Relatorios";

        public void FinalizarExperimento() {
            HoraFim = new DateTime();
        }

        //arrumar esse nome default
        private string GetNomeArquivo() {
            return NomeArquivoDigitado != null && NomeArquivoDigitado != "" ? NomeArquivoDigitado : $"Relatorio_Pactolo_{Experimentador.Nome}_{Participante.Nome}_{HoraInicio.ToString()}";
        }

        public string GetPath() {
            return Ambiente.GetDiretorioPastas() + "\\" + PASTA_RELATORIOS + GetNomeArquivo();
        }
    }
}
