using Pactolo.scr.utils;
using System.Collections.Generic;

namespace Pactolo.scr.dominio {

	public abstract class Pessoa : ElementoDeBanco {

		protected string nome;
		public string Nome {
			get => nome;
			set => nome = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "nome do Participante ou Experimentador");
		}

		// TODO para participante é obrigatório, para experimentador não
		protected string email;
		public virtual string Email {
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
