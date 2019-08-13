using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	abstract class Pessoa : ElementoDeBanco {

		protected string nome;
		public string Nome {
			get => nome;
			set => nome = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Nome");
		}

		// TODO para participante é obrigatório, para experimentador não
		private string email;
		public string Email {
			get => email;
			set => email = StringUtils.ValideEmail(value);
		}

		public override bool Equals(object obj) {
			return GetHashCode() == obj.GetHashCode();
		}

		public override int GetHashCode() {
			var hashCode = 1649639622;
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nome);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
			return hashCode;
		}
	}
}
