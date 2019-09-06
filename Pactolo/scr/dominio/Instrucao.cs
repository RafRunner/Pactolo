using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {
    public class Instrucao : ElementoDeBanco {

        private string texto;
        public string Texto {
            get => texto;
            set => texto = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Texto da Instrução");
        }

        public static List<string> GetOrdemCulunasGrid() {
            return new List<string>() { "Texto" };
        }
    }
}
