using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	public class ContingenciaColateral : ElementoDeBanco {

        protected string nome;
		public string Nome {
			get => nome;
			set => nome = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "nome da Contigência Colateral");
		}

        public long sModeloId { get; set; }
		private UnidadeDoExperimento _sModelo;
        public UnidadeDoExperimento sModelo {
			get => _sModelo;
			set { _sModelo = value; sModeloId = GetId(value); }
		}
        //Decidir se pode ser guardado em uma lista: Acho melhor não, já que vão ser sempre 3
        public long SC1Id { get; set; }
		private UnidadeDoExperimento sC1;
		public UnidadeDoExperimento SC1 {
			get => sC1;
			set { sC1 = value; SC1Id = GetId(value); }
		}

        public long SC2Id { get; set; }
		private UnidadeDoExperimento sC2;
		public UnidadeDoExperimento SC2 {
			get => sC2;
			set { sC2 = value; SC2Id = GetId(value); }
		}

        public long SC3Id { get; set; }
		private UnidadeDoExperimento sC3;
		public UnidadeDoExperimento SC3 {
			get => sC3;
			set { sC3 = value; SC3Id = GetId(value); }
		}

        public long CIId { get; set; }
		private ContingenciaInstrucional cI;
		public ContingenciaInstrucional CI {
			get => cI;
			set { cI = value; CIId = GetId(value); }
		}

        public int AcertosConcecutivos { get; set; }
	}
}
