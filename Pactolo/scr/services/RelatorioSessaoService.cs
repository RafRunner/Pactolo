using Pactolo.scr.dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pactolo.scr.services {
    class RelatorioSessaoService {

        private static readonly string PASTA_RELATORIOS = "Relatorios";
        public static readonly string FORMATO_HORA = "HH:mm:ss";

        private static void CreateDirectoryIfNotExists() {
            if (!Directory.Exists(GetPath())) {
                Directory.CreateDirectory(GetPath());
            }
        }

        public static string GetPath(string nomeArquivo = "") {
            return Ambiente.GetFullPath(PASTA_RELATORIOS, nomeArquivo);
        }

        public static void GeraRelatorio(RelatorioSessao relatorioSessao) {
            CreateDirectoryIfNotExists();

            relatorioSessao.FinalizarExperimento();

            StringBuilder relatorio = new StringBuilder();
            relatorio.Append(GetCabecalhoExperimento(relatorioSessao));
            relatorio.Append(GetInformacoesExperimentador(relatorioSessao.Experimentador));
            relatorio.Append(GetInformacoesParticipante(relatorioSessao.Participante));

            List<long> sessoesIds = relatorioSessao.IdSessoesSelecionadas;
            List<Sessao> sessoes = SessaoService.GetAllByIds(sessoesIds);

            foreach (var sessao in sessoes) {
                List<ContingenciaColateral> contingeciasColaterais = sessao.CCs;

                List<ContingenciaInstrucional> contingeciasInstrucionais = new List<ContingenciaInstrucional>();

                foreach (ContingenciaColateral contingenciaColateral in contingeciasColaterais) {
                    if (contingenciaColateral.CI != null) {
                        contingeciasInstrucionais.Add(contingenciaColateral.CI);
                    }
                }

                relatorio.Append(GetInformacoeSessoes(sessao));

                if (contingeciasInstrucionais.Count > 0) {
                    relatorio.Append(GetInformacoesCIs(contingeciasInstrucionais));
                }

                relatorio.Append(GetInformacoesCCs(contingeciasColaterais));
                relatorio.Append(GetInformacoesEventos(sessao.Id, relatorioSessao));
                relatorio.AppendLine("///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////\n");
            }

            File.WriteAllText(GetPath(relatorioSessao.GetNomeArquivo()) + ".txt", relatorio.ToString());
        }

        private static StringBuilder GetCabecalhoExperimento(RelatorioSessao relatorioSessao) {
            StringBuilder cabeçalhoExperimento = new StringBuilder();

            string dataRealizacao = relatorioSessao.HoraInicio.ToString("dd/MM/yyyy");
            string horaInicio = relatorioSessao.HoraInicio.ToString(FORMATO_HORA);
            string horaFim = relatorioSessao.HoraFim.ToString(FORMATO_HORA);

            cabeçalhoExperimento.AppendLine("Data realização: "+ dataRealizacao);
            cabeçalhoExperimento.AppendLine("Hora inicio: " + horaInicio);
            cabeçalhoExperimento.AppendLine("Hora fim: " + horaFim);
            cabeçalhoExperimento.AppendLine();

            return cabeçalhoExperimento;
        }

        private static StringBuilder GetInformacoesExperimentador(Experimentador experimentador) {
            StringBuilder informacoesExperimentador = new StringBuilder();

            informacoesExperimentador.AppendLine("Experimentador {");
            informacoesExperimentador.AppendLine("   -Nome: " + experimentador.Nome);
            informacoesExperimentador.AppendLine("   -Projeto: "+ experimentador.Projeto);
            informacoesExperimentador.AppendLine("   -Email: " + experimentador.Email);
            informacoesExperimentador.AppendLine("}\n");

            return informacoesExperimentador;
        }

        private static StringBuilder GetInformacoesParticipante(Participante participante) {
            StringBuilder informacoesParticipante = new StringBuilder();

            informacoesParticipante.AppendLine("Participante {");
            informacoesParticipante.AppendLine("   -Nome: " + participante.Nome);
            informacoesParticipante.AppendLine("   -Email: " + participante.Email);
            informacoesParticipante.AppendLine("   -Idade: " + participante.Idade);
            informacoesParticipante.AppendLine("   -Escolaridade: " + participante.Escolaridade);
            informacoesParticipante.AppendLine("   -Sexo: " + participante.Sexo);
            informacoesParticipante.AppendLine("}\n");

            return informacoesParticipante;
        }


        private static StringBuilder GetInformacoesCIs(List<ContingenciaInstrucional> ContingenciasInstrucionais) {
            StringBuilder informacoesCIs = new StringBuilder();
            informacoesCIs.AppendLine("Estímulos Contexto {");

            foreach (ContingenciaInstrucional contingencia in ContingenciasInstrucionais) {
                informacoesCIs.AppendLine("   " + contingencia.Nome);
            }
            informacoesCIs.AppendLine("}\n");

            return informacoesCIs;
        }

        private static StringBuilder GetInformacoesCCs(List<ContingenciaColateral> ContingenciasColaterais) {
            StringBuilder informacoesCIs = new StringBuilder();
            informacoesCIs.AppendLine("Matches to Sample {");

            foreach (ContingenciaColateral contingencia in ContingenciasColaterais) {
                string CINome = contingencia.CI != null ? contingencia.CI.Nome : "Não possui";
                string SC1feedback = contingencia.SC1.Feedback.ValorClick.ToString();
                string SC2feedback = contingencia.SC2.Feedback.ValorClick.ToString();
                string SC3feedback = contingencia.SC3.Feedback.ValorClick.ToString();

                informacoesCIs.AppendLine("   " + contingencia.Nome + " {");
                informacoesCIs.AppendLine("      -EC: " + CINome);
                informacoesCIs.AppendLine("      -SC1: " + SC1feedback + " pts");
                informacoesCIs.AppendLine("      -SC2: " + SC2feedback + " pts");
                informacoesCIs.AppendLine("      -SC3: " + SC3feedback + " pts");
                informacoesCIs.AppendLine("   }");
            }
            informacoesCIs.AppendLine("}\n");

            return informacoesCIs;
        }

        private static StringBuilder GetInformacoeSessoes(Sessao sessao) {
            StringBuilder informacoesCIs = new StringBuilder();
            informacoesCIs.AppendLine("Sessão Atual {");

            informacoesCIs.AppendLine("   " + sessao.Nome + " {");
            List<ContingenciaColateral> contingeciasDaSessao = sessao.CCs;
            int i = 0;

            foreach (ContingenciaColateral contingencia in contingeciasDaSessao) {
                i++;
                informacoesCIs.AppendLine("      -MTS" + i + ": " + contingencia.Nome);
            }

            informacoesCIs.AppendLine("      -Ordem Exposicao: " + (sessao.OrdemAleatoria ? "Aleatória" : "Agrupada"));

            i = 0;
            if (sessao.NumeroTentativas != 0) {
                i++;
                informacoesCIs.AppendLine("      -Criterio " + i + ": Numero Tentativas");
                informacoesCIs.AppendLine("      -Numero Tentativas: " + sessao.NumeroTentativas);
            }
            if (sessao.DuracaoSegundos != 0) {
                i++;
                informacoesCIs.AppendLine("      -Criterio " + i + ": Duracao Segundos");
                informacoesCIs.AppendLine("      -Numero Tentativas: " + sessao.DuracaoSegundos);
            }
            if (sessao.AcertosConcecutivos != 0) {
                i++;
                informacoesCIs.AppendLine("      -Criterio " + i + ": Acertos Concecutivos");
                informacoesCIs.AppendLine("      -Acertos Concecutivos: " + sessao.DuracaoSegundos);
            }

            informacoesCIs.AppendLine("   }");

            informacoesCIs.AppendLine("}\n");

            return informacoesCIs;
        }

        private static StringBuilder GetInformacoesEventos(long sessaoId, RelatorioSessao relatorio) {
            StringBuilder infoEventos = new StringBuilder();
            List<Evento> eventos = relatorio.Eventos[sessaoId];

            int totalAcertos = 0;
            int totalErros = 0;
            int totalNeutros = 0;
            double latenciaTotal = 0.0;

            infoEventos.AppendLine("Eventos Realizados pelo participante {");

            DateTime eventoAnterior = relatorio.HoraInicio;
            foreach (Evento evento in eventos) {
                infoEventos.Append("   ").Append(evento.MontaMensagem());

                // É um evento de SC, registramos o intervalo
                if (evento.Acerto != -2) {
                    double diferencaDoEventoAnterior = (evento.HoraEvento - eventoAnterior).TotalSeconds;
                    infoEventos.Append(" | Intervalo do último evento: ").Append(diferencaDoEventoAnterior).AppendLine("s");

                    switch (evento.Acerto) {
                        case -1: totalNeutros++; break;
                        case 0: totalErros++; break;
                        case 1: totalAcertos++; break;
					}

                    latenciaTotal += diferencaDoEventoAnterior;
                }
                else {
                    infoEventos.AppendLine();
				}

                eventoAnterior = evento.HoraEvento;
            }

            infoEventos.AppendLine("}\n");

            infoEventos.AppendLine("Resumo dos eventos {");
            infoEventos.Append("   Total de Acetos: ").AppendLine(totalAcertos.ToString());
            infoEventos.Append("   Total de Erros: ").AppendLine(totalErros.ToString());
            infoEventos.Append("   Total de Neutros: ").AppendLine(totalNeutros.ToString());
            infoEventos.Append("   Latência média: ").Append(latenciaTotal / (totalAcertos + totalErros + totalNeutros)).AppendLine("s");
            infoEventos.AppendLine("}\n");

            return infoEventos;
        }
    }
}
