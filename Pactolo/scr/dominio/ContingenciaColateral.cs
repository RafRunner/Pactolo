﻿using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	class ContingenciaColateral : ElementoDeBanco {

        protected string nome;
		public string Nome {
			get => nome;
			set => nome = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Nome");
		}

        public long sModeloId { get; set; }
		private UnidadeDoExperimeto _sModelo;
        public UnidadeDoExperimeto sModelo {
			get => _sModelo;
			set => _sModelo = ElementoDeBanco.Set<UnidadeDoExperimeto>(sModeloId, value);
		}
        //Decidir se pode ser guardado em uma lista: Acho melhor não, já que vão ser sempre 3
        public long SC1Id { get; set; }
		private UnidadeDoExperimeto sC1;
		public UnidadeDoExperimeto SC1 {
			get => sC1;
			set => sC1 = ElementoDeBanco.Set<UnidadeDoExperimeto>(SC1Id, value);
		}

        public long SC2Id { get; set; }
		private UnidadeDoExperimeto sC2;
		public UnidadeDoExperimeto SC2 {
			get => sC2;
			set => sC2 = ElementoDeBanco.Set<UnidadeDoExperimeto>(SC2Id, value);
		}

        public long SC3Id { get; set; }
		private UnidadeDoExperimeto sC3;
		public UnidadeDoExperimeto SC3 {
			get => sC3;
			set => sC3 = ElementoDeBanco.Set<UnidadeDoExperimeto>(SC3Id, value);
		}

        public long CIId { get; set; }
		private ContingenciaInstrucional cI;
		public ContingenciaInstrucional CI {
			get => cI;
			set => cI = ElementoDeBanco.Set<ContingenciaInstrucional>(CIId, value);
		}

		//Colocar validação de probabilidade que deve ser entre 0 e 1
		public float ProbabilidadeComplementar { get; set; }
	}
}
