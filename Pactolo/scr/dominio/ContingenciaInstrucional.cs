using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	public class ContingenciaInstrucional : ElementoDeBanco {

		protected string nome;
		public string Nome {
			get => nome;
			set => nome = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "nome da Contigência Instrucional");
		}

		private List<UnidadeDoExperimento> tatos;
		public List<UnidadeDoExperimento> Tatos { 
			get => tatos;
			set {
				if (value.Count == 0) {
					throw new Exception("Uma CI deve ter no mínimo um tato! Para 'ter uma CI sem tatos', cadastre uma CC sem CI");
				}
				if (value.Count > 5) {
					throw new Exception("Muitos Tatos! Por favor, adicione no máximo 5 tatos para cada CI");
				}
				tatos = value;
			}
		}
	}
}
