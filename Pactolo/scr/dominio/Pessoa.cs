using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	abstract class Pessoa {

		// Id tem que ter set para, ao deletar um objeto, o métodod possa settar seu Id como zero para que se possa saber que aquele registro não existe mais no banco de dados
		public int Id { get; set; }

		protected string nome;
		public string Nome {
			get => nome;
			set => nome = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Nome");
		}

		// TODO decidir se deve ser não nulo
		private string email;
		public string Email {
			get => email;
			set => email = StringUtils.ValideEmail(value);
		}

		public override bool Equals(object obj) {
			return Id == ((Pessoa) obj).Id;
		}
	}
}
