using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	class ContingenciaInstrucional : ElementoDeBanco {

		protected string nome;
		public string Nome {
			get => nome;
			set => nome = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Nome");
		}

		public long Tato1Id { get; set; }
		private UnidadeDoExperimeto tato1;
		public UnidadeDoExperimeto Tato1 {
			get => tato1;
			set { tato1 = value; Tato1Id = GetId(value); }
		}

        public long AutocliticoId { get; set; }
		private UnidadeDoExperimeto autoclitico;
		public UnidadeDoExperimeto Autoclitico {
			get => autoclitico;
			set { autoclitico = value; AutocliticoId = GetId(value); }
		}

        public long Tato2Id { get; set; }
		private UnidadeDoExperimeto tato2;
		public UnidadeDoExperimeto Tato2 {
			get => tato2;
			set { tato2 = value; Tato2Id = GetId(value); }
		}
	}
}
