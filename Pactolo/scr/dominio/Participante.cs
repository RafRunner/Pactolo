using Pactolo.scr.enums;
using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	class Participante : Pessoa {

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
	}
}
