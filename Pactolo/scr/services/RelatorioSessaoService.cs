using Pactolo.scr.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.services {
    class RelatorioSessaoService {

        public static StringBuilder getCabeçalhoExperimento(RelatorioSessao relatorioSessao) {
            StringBuilder cabeçalhoExperimento = new StringBuilder();
            string dataRealizacao = relatorioSessao.horaInicio.ToString("dd/MM/yyyy");
            string horaInicio = relatorioSessao.horaInicio.ToString("hh:mm:ss");
            string horaFim = relatorioSessao.horaFim.ToString("hh:mm:ss");
            cabeçalhoExperimento.AppendLine("Data realização: "+ dataRealizacao);
            cabeçalhoExperimento.AppendLine("Hora inicio: " + horaInicio);
            cabeçalhoExperimento.AppendLine("Hora fim: " + horaFim);
            cabeçalhoExperimento.AppendLine();
            return cabeçalhoExperimento;
        }

        public static StringBuilder getInformacoesExperimentador(RelatorioSessao relatorioSessao) {
            StringBuilder informacoesExperimentador = new StringBuilder();
            Experimentador experimentador = relatorioSessao.experimentador;
            informacoesExperimentador.AppendLine("Experimentador{");
            informacoesExperimentador.AppendLine("   -Nome: " + experimentador.Nome);
            informacoesExperimentador.AppendLine("   -Projeto: "+ experimentador.Projeto);
            informacoesExperimentador.AppendLine("   -Email: " + experimentador.Email);
            informacoesExperimentador.AppendLine("}");
            return informacoesExperimentador;
        }

        public static StringBuilder getInformacoesParticipante(RelatorioSessao relatorioSessao) {
            StringBuilder informacoesExperimentador = new StringBuilder();
            Participante participante = relatorioSessao.participante;
            informacoesExperimentador.AppendLine("Participante{");
            informacoesExperimentador.AppendLine("   -Nome: " + participante.Nome);
            informacoesExperimentador.AppendLine("   -Email: " + participante.Email);
            informacoesExperimentador.AppendLine("   -Idade: " + participante.Idade);
            informacoesExperimentador.AppendLine("   -Escolaridade: " + participante.Escolaridade);
            informacoesExperimentador.AppendLine("   -Sexo: " + participante.Sexo);
            informacoesExperimentador.AppendLine("}");
            return informacoesExperimentador;
        }

    }
}
