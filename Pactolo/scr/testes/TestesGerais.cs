using Pactolo.scr.dominio;
using Pactolo.scr.enums;
using Pactolo.scr.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.testes {

	class TestesGerais {

		Participante participante = new Participante {
			Nome = "Rafael Nunes Santana",
			Email = "rafaelns.br@gmail.com",
			Idade = 21,
			Escolaridade = EEscolaridade.Escolaridade.SuperiorIncompleto.ToString(),
			Sexo = ESexo.Sexo.Masculino.ToString()
		};

		Experimentador experimentador = new Experimentador {
			Nome = "Rafael Nunes Santana",
			Email = "rafaelns.br@gmail.com",
			Projeto = "Pactolo"
		};

		static void TesteIndividual(Func<object, object> funcao, object parametro, string mensagem, bool falharNoSucesso) {
			bool excecaoLancada;
			try {
				funcao.Invoke(parametro);
				excecaoLancada = false;
			} catch {
				excecaoLancada = true;
			}

			if (falharNoSucesso) {
				if (!excecaoLancada) {
					throw new Exception(mensagem);
				}
			}
			else {
				if (excecaoLancada) {
					throw new Exception(mensagem);
				}
			}
		}

		static void TesteIndividualDeFalha(Func<object, object> funcao, object parametro, string mensagem) {
			TesteIndividual(funcao, parametro, mensagem, true);
		}

		static void TesteIndividualDeSucesso(Func<object, object> funcao, object parametro, string mensagem) {
			TesteIndividual(funcao, parametro, mensagem, false);
		}

		static void Assert(bool expressao, string mensagem = "Falha de assert!") {
			if (!expressao) {
				throw new Exception(mensagem);
			}
		}

		public void TodosOsTestes() {
			TestesDominio();
			ExperimentadorServiceTestes();
			ParticipanteServiceTestes();
		}

		private void TestesDominio() {
			TesteIndividualDeFalha(invalido => EEscolaridade.ParseAndValidate((string) invalido), "banana", "Deveria ter falhado para valor inválido de Escolaridade!");
			TesteIndividualDeFalha(invalido => ESexo.ParseAndValidate((string) invalido), "Masc", "Deveria ter falhado para valor inválido de Sexo!");
			TesteIndividualDeFalha(invalido => participante.Nome = (string) invalido, "", "Deveria ter falhado para valor em branco de nome em Participante!");
			TesteIndividualDeFalha(invalido => experimentador.Projeto = (string) invalido, null, "Deveria ter falhado para valor nulo de projeto em Experimentador!");
			TesteIndividualDeFalha(invalido => participante.Idade = (int) invalido, -1, "Deveria ter falhado para valor negativo de idade em Participante!");
			TesteIndividualDeFalha(invalido => participante.Idade = (int) invalido, 121, "Deveria ter falhado para valor acima de 120 anos de idade em Participante!");

			Assert("Médio Incompleto" == EEscolaridade.GetValue(EEscolaridade.Escolaridade.MedioIncompleto), "Erro em Escolaridade.MedioIncompleto!");
			Assert(!ESexo.Values().Any(it => !new List<string> { "Feminino", "Masculino", "Outro" }.Contains(it)), "Inconsistência nos valores de Sexo!");
			EEscolaridade.Values();

			var feed1 = new Feedback { Id = 1, ValorClick = 3, Neutro = false };
			var feed2 = new Feedback { Id = 2, ValorClick = 2, Neutro = false };
			var foo = new UnidadeDoExperimento { Id = 3, Feedback = feed1 };
			Assert(foo.FeedbackId == feed1.Id && foo.Feedback.ValorClick == feed1.ValorClick);
			foo.Feedback = feed2;
			Assert(foo.FeedbackId == feed2.Id && foo.Feedback.ValorClick == feed2.ValorClick);
		}

		private void ExperimentadorServiceTestes() {
			ExperimentadorService.Salvar(experimentador);
			long id = experimentador.Id;
			Assert(id != 0, "Falha ao inserir experimentador no banco!");

			var experimentadorDoBanco = ExperimentadorService.GetById(id);
			Assert(experimentador.Equals(experimentadorDoBanco), "Falha ao obter experimentador Pelo Id!");

			experimentadorDoBanco = ExperimentadorService.GetByNome("Raf").First();
			Assert(experimentador.Equals(experimentadorDoBanco), "Falha ao obter experimentador Pelo Nome!");

			experimentador.Projeto = "Outro projeto";
			ExperimentadorService.Salvar(experimentador);
			Assert(experimentador.Projeto == "Outro projeto", "Falha ao atualizar experimentador!");

			ExperimentadorService.Deletar(experimentador);
			Assert(experimentador.Id == 0, "Falha ao deletar experimentador!");
		}

		private void ParticipanteServiceTestes() {
			ParticipanteService.Salvar(participante);
			long id = participante.Id;
			Assert(id != 0, "Falha ao inserir participante no banco!");

			var participanteDoBanco = ParticipanteService.GetById(id);
			Assert(participante.Equals(participanteDoBanco), "Falha ao obter participante Pelo Id!");

			participanteDoBanco = ParticipanteService.GetByNome("Raf").First();
			Assert(participante.Equals(participanteDoBanco), "Falha ao obter participante Pelo Nome!");

			participante.Idade = 22;
			participante.Escolaridade = EEscolaridade.Escolaridade.Superior.ToString();
			ParticipanteService.Salvar(participante);
			Assert(participante.Idade == 22 && participante.Escolaridade == EEscolaridade.Escolaridade.Superior.ToString(), "Falha ao atualizar participante!");

			ParticipanteService.Deletar(participante);
			Assert(participante.Id == 0, "Falha ao deletar participante!");

            AudioService.CopiaImagemParaPasta(@"c:\Users\Igor Moraes\Downloads\musica.mp3");
		}
	}
}
