using Pactolo.scr.enums;
using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	class Participante : Pessoa {

        public override string Email {
            get => email;
            set => email = StringUtils.ValideEmail(StringUtils.ValideNaoNuloNaoVazioENormalize(value, "email"));
        }

		int idade;
		public int Idade {
			get => idade;
			set => idade = NumericUtils.ValidarInteiroPositivoDentroDeLimite(value, 120, "Idade");
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
