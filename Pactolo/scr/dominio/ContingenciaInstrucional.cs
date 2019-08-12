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

        protected UnidadeDoExperimeto tato1;
        public UnidadeDoExperimeto Tato1  {get; set; }

        protected UnidadeDoExperimeto autoclitico;
        public UnidadeDoExperimeto Autoclitico { get; set; }

        protected UnidadeDoExperimeto tato2;
        public UnidadeDoExperimeto Tato2 { get; set; }
    }
}
