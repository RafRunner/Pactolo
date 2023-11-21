using Pactolo.scr.enums;
using Pactolo.scr.utils;
using System.Collections.Generic;

namespace Pactolo.scr.dominio {

	public class Participante : Pessoa {

        public override string Email {
            get => email;
            set => email = StringUtils.ValideEmail(StringUtils.ValideNaoNuloNaoVazioENormalize(value, "email do participante"));
        }

		int idade;
		public int Idade {
			get => idade;
			set => idade = NumericUtils.ValidarInteiroPositivoDentroDeLimite(value, 120, "idade do participante");
		}

		string escolaridade;
		public string Escolaridade {
			get => escolaridade;
			set => escolaridade = EEscolaridade.ParseAndValidate(value);
		}

		string sexo;
		public string Sexo {
			get => sexo;
			set => sexo = ESexo.ParseAndValidate(value);
		}

        public static List<string> GetOrdemColunasGrid() {
            return new List<string> { "Nome", "Email", "Idade", "Sexo", "Escolaridade" };
        }
	}
}
