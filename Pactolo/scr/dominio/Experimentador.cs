﻿using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	public class Experimentador : Pessoa {

		private string projeto;
		public string Projeto {
			get => projeto;
			set => projeto = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "projeto do experimentador");
		}

        public static List<string> GetOrdemCulunasGrid() {
            return new List<string>() { "Nome", "Email", "Projeto" };
        }
	}
}
