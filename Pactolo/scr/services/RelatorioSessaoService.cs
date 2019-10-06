using Pactolo.scr.dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.services {
    class RelatorioSessaoService {

        private static string PASTA_RELATORIOS = "Relatorios";
        private static void CreateDirectoryIfNotExists() {
            if (!Directory.Exists(GetPath())) {
                Directory.CreateDirectory(GetPath());
            }
        }

        public static string GetPath(string nomeArquivo = "") {
            string caminho = Ambiente.GetDesktop() + "\\" + PASTA_RELATORIOS;
            if (!string.IsNullOrEmpty(nomeArquivo)) {
                caminho += "\\" + nomeArquivo;
            }
            return caminho;
        }

        public static void GeraRelatorio(RelatorioSessao relatorioSessao) {
            CreateDirectoryIfNotExists();
            StringBuilder relatorio = new StringBuilder();
            relatorio.Append(GetCabecalhoExperimento(relatorioSessao));
            relatorio.Append(GetInformacoesExperimentador(relatorioSessao.Experimentador));
            relatorio.Append(GetInformacoesParticipante(relatorioSessao.Participante));
            List<long> sessoesIds = relatorioSessao.IdSessoesSelecionadas;
            List<ContingenciaColateral> contingeciasColaterais = new List<ContingenciaColateral>();
            foreach (long sessaoId in sessoesIds) {
                contingeciasColaterais.AddRange(CCPorSessaoService.GetAllCCBySessaoId(sessaoId));
            }
            HashSet<ContingenciaColateral> contingenciasColateraisDoExperimento = new HashSet<ContingenciaColateral>(contingeciasColaterais);
            List<ContingenciaInstrucional> contingeciasInstrucionais = new List<ContingenciaInstrucional>();
            foreach (ContingenciaColateral contingenciaColateral in contingenciasColateraisDoExperimento) {
                if (contingenciaColateral.CI != null) {
                    contingeciasInstrucionais.Add(contingenciaColateral.CI);
                }
            }
            if (contingeciasInstrucionais.Count > 0) {
                HashSet<ContingenciaInstrucional> contingeciasInstrucionaisDoExperimento = new HashSet<ContingenciaInstrucional>(contingeciasInstrucionais);
                relatorio.Append(GetInformacoesCIs(contingeciasInstrucionaisDoExperimento));
            }
            relatorio.Append(GetInformacoesCCs(contingenciasColateraisDoExperimento));
            relatorio.Append(GetInformacoeSessoes(SessaoService.GetAllByIds(sessoesIds)));
            relatorio.Append(GetInformacoesEventos(relatorioSessao));
            File.WriteAllText(GetPath(relatorioSessao.GetNomeArquivo()) + ".txt", relatorio.ToString());
        }

        private static StringBuilder GetCabecalhoExperimento(RelatorioSessao relatorioSessao) {
            StringBuilder cabeçalhoExperimento = new StringBuilder();
            string dataRealizacao = relatorioSessao.HoraInicio.ToString("dd/MM/yyyy");
            string horaInicio = relatorioSessao.HoraInicio.ToString("hh:mm:ss");
            string horaFim = relatorioSessao.HoraFim.ToString("hh:mm:ss");
            cabeçalhoExperimento.AppendLine("Data realização: "+ dataRealizacao);
            cabeçalhoExperimento.AppendLine("Hora inicio: " + horaInicio);
            cabeçalhoExperimento.AppendLine("Hora fim: " + horaFim);
            cabeçalhoExperimento.AppendLine();
            return cabeçalhoExperimento;
        }

        private static StringBuilder GetInformacoesExperimentador(Experimentador experimentador) {
            StringBuilder informacoesExperimentador = new StringBuilder();
            informacoesExperimentador.AppendLine("Experimentador{");
            informacoesExperimentador.AppendLine("   -Nome: " + experimentador.Nome);
            informacoesExperimentador.AppendLine("   -Projeto: "+ experimentador.Projeto);
            informacoesExperimentador.AppendLine("   -Email: " + experimentador.Email);
            informacoesExperimentador.AppendLine("}");
            return informacoesExperimentador;
        }

        private static StringBuilder GetInformacoeSessoes(List<Sessao> sessoes) {
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
                informacoesCIs.AppendLine("      -Ordem Exposicao: " + (sessao.OrdemAleatoria ? "Aleatória" : "Agrupada"));
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

        private static StringBuilder GetInformacoesParticipante(Participante participante) {
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

        private static StringBuilder GetInformacoesCCs(HashSet<ContingenciaColateral> ContingenciasColaterais) {
            StringBuilder informacoesCIs = new StringBuilder();
            informacoesCIs.AppendLine("Contingencias colaterais{");

            foreach (ContingenciaColateral contingencia in ContingenciasColaterais) {
                string CINome = contingencia.CI != null ? contingencia.CI.Nome : "Não possui";
                string SC1feedback = contingencia.SC1.Feedback.ValorClick.ToString();
                string SC2feedback = contingencia.SC2.Feedback.ValorClick.ToString();
                string SC3feedback = contingencia.SC3.Feedback.ValorClick.ToString();
                informacoesCIs.AppendLine("   " + contingencia.Nome + "{");
                informacoesCIs.AppendLine("      -CI: " + CINome);
                informacoesCIs.AppendLine("      -SC1: " + SC1feedback + " pts");
                informacoesCIs.AppendLine("      -SC2: " + SC2feedback + " pts");
                informacoesCIs.AppendLine("      -SC3: " + SC3feedback + " pts");
                informacoesCIs.AppendLine("   }");
            }
            informacoesCIs.AppendLine("}");

            return informacoesCIs;
        }

        private static StringBuilder GetInformacoesCIs(HashSet<ContingenciaInstrucional> ContingenciasInstrucionais) {
            StringBuilder informacoesCIs = new StringBuilder();
            informacoesCIs.AppendLine("Contingencias instrucionais{");

            foreach (ContingenciaInstrucional contingencia in ContingenciasInstrucionais) {
                informacoesCIs.AppendLine("   " + contingencia.Nome);
            }
            informacoesCIs.AppendLine("}");

            return informacoesCIs;
        }

        private static StringBuilder GetInformacoesEventos(RelatorioSessao relatorio) {
            StringBuilder informacoesCIs = new StringBuilder();
            List<Evento> eventos = relatorio.Eventos;
            informacoesCIs.AppendLine("Eventos Realizados pelo participante{");
            informacoesCIs.AppendLine("   Iniciou(apos leitura das instruções): " + relatorio.HoraInicio.ToString("hh:mm:ss"));
            informacoesCIs.AppendLine("   Sessão | ContingenciaColateral | número SC | imagem SC | pontos feedback | tentativa | pontos totais | horário | tempo por evento |");
            DateTime eventoAnterior = relatorio.HoraInicio;
            foreach (Evento evento in eventos) {
                TimeSpan diferencaDoEventoAnterior = evento.HoraEvento - eventoAnterior;
                informacoesCIs.Append(evento.MontaMensagem()).Append("| Intervalor do último evento: ").Append(diferencaDoEventoAnterior.TotalSeconds.ToString()).AppendLine("s");
                eventoAnterior = evento.HoraEvento;
            }
            informacoesCIs.AppendLine();
            informacoesCIs.AppendLine("}");

            return informacoesCIs;
        }
    }
}
