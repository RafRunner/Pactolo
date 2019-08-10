using Pactolo.scr.dominio;
using Pactolo.scr.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.testes {

	class TestesGerais {

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
			} else {
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

		static void Assert(bool expressao, string mensagem) {
			if (!expressao) {
				throw new Exception(mensagem);
			}
		}

		public static void Testes() {
			Participante participante = new Participante {
				Nome = "Rafael",
				Email = "rafaelns.br@gmail.com",
				Idade = 21,
				Escolaridade = EEscolaridade.Escolaridade.SuperiorIncompleto.ToString(),
				Sexo = ESexo.Sexo.Masculino.ToString()
			};

			Experimentador experimentador = new Experimentador {
				Nome = "Rafael",
				Email = "rafaelns.br@gmail.com",
				Projeto = "Pactolo"
			};

			TesteIndividualDeFalha(invalido => EEscolaridade.ParseAndValidate((string) invalido), "banana", "Deveria ter falhado para valor inválido de Escolaridade!");
			TesteIndividualDeFalha(invalido => ESexo.ParseAndValidate((string) invalido), "Masc", "Deveria ter falhado para valor inválido de Sexo!");
			TesteIndividualDeFalha(invalido => participante.Nome = (string) invalido, "", "Deveria ter falhado para valor em branco de nome em Participante!");
			TesteIndividualDeFalha(invalido => experimentador.Projeto = (string) invalido, null, "Deveria ter falhado para valor nulo de projeto em Experimentador!");
			TesteIndividualDeFalha(invalido => participante.Idade = (int) invalido, -1, "Deveria ter falhado para valor negativo de idade em Participante!");
			TesteIndividualDeFalha(invalido => participante.Idade = (int) invalido, 121, "Deveria ter falhado para valor acima de 120 anos de idade em Participante!");

			Assert("Médio Incompleto" == EEscolaridade.GetValue(EEscolaridade.Escolaridade.MedioIncompleto), "Erro em Escolaridade.MedioIncompleto!");
			Assert(!ESexo.Values().Any(it => ! new List<string> { "Feminino", "Masculino", "Outro" }.Contains(it) ), "Inconsistência nos valores de Sexo!");
			EEscolaridade.Values();
		}
	}
}
