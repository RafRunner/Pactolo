using Pactolo.scr.dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.services {
    class RelatorioSessaoService {

        public static void geraRelatorio(RelatorioSessao relatorioSessao) {
            StringBuilder relatorio = new StringBuilder();
            relatorio.Append(getCabeçalhoExperimento(relatorioSessao));
            relatorio.Append(getInformacoesExperimentador(relatorioSessao.experimentador));
            relatorio.Append(getInformacoesParticipante(relatorioSessao.participante));
            List<long> sessoesIds = relatorioSessao.IdSessoesSelecionadas;
            List<ContingenciaColateral> contingeciasColaterais = new List<ContingenciaColateral>();
            foreach (long sessaoId in sessoesIds) {
                contingeciasColaterais.AddRange(CCPorSessaoService.GetAllCCBySessaoId(sessaoId));
            }
            HashSet<ContingenciaColateral> contingenciasColateraisDoExperimento = new HashSet<ContingenciaColateral>(contingeciasColaterais);
            List<ContingenciaInstrucional> contingeciasInstrucionais = new List<ContingenciaInstrucional>();
            foreach (ContingenciaColateral contingenciaColateral in contingenciasColateraisDoExperimento) {
                contingeciasInstrucionais.Add(contingenciaColateral.CI);
            }
            HashSet<ContingenciaInstrucional> contingeciasInstrucionaisDoExperimento = new HashSet<ContingenciaInstrucional>(contingeciasInstrucionais);
            relatorio.Append(getInformacoesCIs(contingeciasInstrucionaisDoExperimento));
            relatorio.Append(getInformacoesCCs(contingenciasColateraisDoExperimento));
            relatorio.Append(getInformacoeSessoes(SessaoService.GetAllByIds(sessoesIds)));
            relatorio.Append(getInformacoesEventos(relatorioSessao));
            File.WriteAllText(relatorioSessao.getPath(), relatorio.ToString());
        }

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

        public static StringBuilder getInformacoesExperimentador(Experimentador experimentador) {
            StringBuilder informacoesExperimentador = new StringBuilder();
            informacoesExperimentador.AppendLine("Experimentador{");
            informacoesExperimentador.AppendLine("   -Nome: " + experimentador.Nome);
            informacoesExperimentador.AppendLine("   -Projeto: "+ experimentador.Projeto);
            informacoesExperimentador.AppendLine("   -Email: " + experimentador.Email);
            informacoesExperimentador.AppendLine("}");
            return informacoesExperimentador;
        }

        public static StringBuilder getInformacoeSessoes(List<Sessao> sessoes) {
            StringBuilder informacoesCIs = new StringBuilder();
            informacoesCIs.AppendLine("Sessões Utilizadas{");

            foreach (Sessao sessao in sessoes) {
                informacoesCIs.AppendLine("   " + sessao.Nome + "{");
                List<ContingenciaColateral> contingeciasDaSessao = sessao.CCs;
                int i = 0;
                foreach (ContingenciaColateral contingencia in contingeciasDaSessao) {
                    i++;
                    informacoesCIs.AppendLine("      -CC" + i.ToString()+ ": " + contingencia.Nome);
                }
                informacoesCIs.AppendLine("      -Ordem Exposicao: " + sessao.OrdemExposicao);
                i = 0;
                if (sessao.NumeroTentativas != 0) {
                    i++;
                    informacoesCIs.AppendLine("      -Criterio" + i.ToString() + ": Numero Tentativas");
                    informacoesCIs.AppendLine("      -Numero Tentativas: " + sessao.NumeroTentativas.ToString());
                }
                if (sessao.DuracaoSegundos != 0) {
                    i++;
                    informacoesCIs.AppendLine("      -Criterio" + i.ToString() + ": Duracao Segundos");
                    informacoesCIs.AppendLine("      -Numero Tentativas: " + sessao.DuracaoSegundos.ToString());
                }
                if (sessao.AcertosConcecutivos != 0) {
                    i++;
                    informacoesCIs.AppendLine("      -Criterio" + i.ToString() + ": Acertos Concecutivos");
                    informacoesCIs.AppendLine("      -Acertos Concecutivos: " + sessao.DuracaoSegundos.ToString());
                }
                informacoesCIs.AppendLine("   }");
            }
            informacoesCIs.AppendLine("}");

            return informacoesCIs;
        }

        public static StringBuilder getInformacoesParticipante(Participante participante) {
            StringBuilder informacoesParticipante = new StringBuilder();
            informacoesParticipante.AppendLine("Participante{");
            informacoesParticipante.AppendLine("   -Nome: " + participante.Nome);
            informacoesParticipante.AppendLine("   -Email: " + participante.Email);
            informacoesParticipante.AppendLine("   -Idade: " + participante.Idade);
            informacoesParticipante.AppendLine("   -Escolaridade: " + participante.Escolaridade);
            informacoesParticipante.AppendLine("   -Sexo: " + participante.Sexo);
            informacoesParticipante.AppendLine("}");
            return informacoesParticipante;
        }

        public static StringBuilder getInformacoesCCs(HashSet<ContingenciaColateral> ContingenciasColaterais) {
            StringBuilder informacoesCIs = new StringBuilder();
            informacoesCIs.AppendLine("Contingencias colaterais{");

            foreach (ContingenciaColateral contingencia in ContingenciasColaterais) {
                string CINome = contingencia.CI.Nome != null && contingencia.CI.Nome != ""? contingencia.CI.Nome : "Não possui";
                string SC1feedback = contingencia.SC1.Feedback.ValorClick.ToString();
                string SC2feedback = contingencia.SC1.Feedback.ValorClick.ToString();
                string SC3feedback = contingencia.SC1.Feedback.ValorClick.ToString();
                informacoesCIs.AppendLine("   " + contingencia.Nome + "{");
                informacoesCIs.AppendLine("      -CI: " + CINome);
                informacoesCIs.AppendLine("      -SC1: " + SC1feedback + " pts");
                informacoesCIs.AppendLine("      -SC2: " + SC2feedback + " pts");
                informacoesCIs.AppendLine("      -SC3: " + SC3feedback + " pts");
                informacoesCIs.AppendLine("   }" + SC3feedback + " pts");
            }
            informacoesCIs.AppendLine("}");

            return informacoesCIs;
        }

        public static StringBuilder getInformacoesCIs(HashSet<ContingenciaInstrucional> ContingenciasInstrucionais) {
            StringBuilder informacoesCIs = new StringBuilder();
            informacoesCIs.AppendLine("Contingencias instrucionais{");

            foreach (ContingenciaInstrucional contingencia in ContingenciasInstrucionais) {
                informacoesCIs.AppendLine("   " + contingencia.Nome);
            }
            informacoesCIs.AppendLine("}");

            return informacoesCIs;
        }

        public static StringBuilder getInformacoesEventos(RelatorioSessao relatorio) {
            StringBuilder informacoesCIs = new StringBuilder();
            List<Evento> eventos = relatorio.eventos;
            informacoesCIs.AppendLine("Eventos Realizados pelo participante{");
            informacoesCIs.AppendLine("   Iniciou(apos leitura das instruções): " + relatorio.horaInicio.ToString("hh:mm:ss"));
            informacoesCIs.AppendLine("   Sessão|ContingenciaColateral|SC|feedback|tentativa|pontos totais|horario|tempo por evento|");
            DateTime eventoAnterior = relatorio.horaInicio;
            foreach (Evento evento in eventos) {
                if (evento.eventoEncerramento) {
                    informacoesCIs.AppendLine(evento.NomeSesssao + " encerrada, criterio: " + evento.criterioEncerramento+", " + evento.valorEncerramento);
                    informacoesCIs.AppendLine();
                }
                TimeSpan diferencaDoEventoAnterior = evento.horaEvento - eventoAnterior;
                informacoesCIs.AppendLine("   |" + evento.NomeSesssao + "|" + evento.NomeCC + "|" + evento.NomeSC + "|" + evento.pontosGanhos + "|" + evento.tentativaAtual + "|" + evento.pontosAtuais + "|" + evento.horaEvento.ToString() + "|" + diferencaDoEventoAnterior.ToString() + "|");
                eventoAnterior = evento.horaEvento;
            }
            informacoesCIs.AppendLine("}");

            return informacoesCIs;
        }
    }
}
