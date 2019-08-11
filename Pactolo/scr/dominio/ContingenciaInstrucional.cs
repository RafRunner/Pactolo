using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {
    class ContingenciaInstrucional {
        public int Id { get; set; }

        protected string nome;
        public string Nome {
            get => nome;
            set => nome = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Nome");
        }

        public UnidadeDoExperimeto Tato1 {get;set;}
        public long Tato1Id { get; set; }

        public UnidadeDoExperimeto Autoclitico { get; set; }
        public long AutocliticoId { get; set; }

        public UnidadeDoExperimeto Tato2 { get; set; }
        public long Tato2Id { get; set; }
    }
}
