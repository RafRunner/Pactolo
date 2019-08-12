using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	class ContingenciaColateral : ElementoDeBanco {
        public long Id { get; set; }

        protected string nome;
		public string Nome {
			get => nome;
			set => nome = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Nome");
		}
        public long sModeloId { get; set; }
        public UnidadeDoExperimeto sModelo { get; set; }
        //Decidir se pode ser guardado em uma lista:
        public long SC1Id { get; set; }
        public UnidadeDoExperimeto SC1 { get; set; }
        public long SC2Id { get; set; }
        public UnidadeDoExperimeto SC2 { get; set; }
        public long SC3Id { get; set; }
        public UnidadeDoExperimeto SC3 { get; set; }

        public long CIId { get; set; }
        public ContingenciaInstrucional CI { get; set; }
		//Colocar validação de probabilidade que deve ser entre 0 e 1
		public float ProbabilidadeComplementar { get; set; }
	}
}
