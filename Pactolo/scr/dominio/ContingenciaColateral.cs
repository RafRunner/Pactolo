using Pactolo.scr.utils;
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

        public UnidadeDoExperimeto sModelo { get; set; }
        //Decidir se pode ser guardado em uma lista:
        public UnidadeDoExperimeto SC1 { get; set; }
        public UnidadeDoExperimeto SC2 { get; set; }
        public UnidadeDoExperimeto SC3 { get; set; }

        public ContingenciaInstrucional CI { get; set; }
        //Colocar validação de probabilidade que deve ser entre 0 e 1
        public float ProbabilidadeComplementar { get; set; }
    }
}
