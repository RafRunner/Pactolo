using Pactolo.scr.utils;
using System;
using System.Collections.Generic;

namespace Pactolo.scr.dominio {

	public class ContingenciaInstrucional : ElementoDeBanco {

		protected string nome;
		public string Nome {
			get => nome;
			set => nome = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "nome do Estímulo Contexto");
		}

		private List<UnidadeDoExperimento> tatos;
		public List<UnidadeDoExperimento> Tatos { 
			get => tatos;
			set {
				if (value.Count == 0) {
					throw new Exception("Um EC deve ter no mínimo um tato! Para 'ter uma EC sem tatos', cadastre um MTS sem EC");
				}
				if (value.Count > 5) {
					throw new Exception("Muitos Tatos! Por favor, adicione no máximo 5 tatos para cada EC");
				}
				tatos = value;
			}
		}

		public bool SemCor { get; set; }
	}
}
