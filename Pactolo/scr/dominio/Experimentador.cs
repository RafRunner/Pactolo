using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	class Experimentador : Pessoa {

		private string projeto;
		public string Projeto {
			get => projeto;
			set => projeto = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Projeto");
		}

		public override bool Equals(object obj) {
			Experimentador o = (Experimentador) obj;
			return base.Equals(obj) || (Nome == o.Nome && Email == o.Email && Projeto == o.Projeto);
		}
	}
}
