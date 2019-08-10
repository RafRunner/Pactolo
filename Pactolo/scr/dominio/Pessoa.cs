using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	abstract class Pessoa {

		public int Id { get; }

		protected string nome;
		public string Nome {
			get => nome;
			set => nome = StringUtils.ValideNaoNuloNaoVazioENormalizeString(value, "Nome");
		}

		// TODO fazer um validador de Email (decidir também se deve ser não nulo)
		public string Email { get; set; }
	}
}
