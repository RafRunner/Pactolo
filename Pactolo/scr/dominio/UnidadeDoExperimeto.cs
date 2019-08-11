using Pactolo.scr.utils;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pactolo.scr.dominio {
    class UnidadeDoExperimeto {
        public int Id { get; set; }
        // TODO fazer um validador dos foramatos de imagens
        public Image Imagem { get; set; }

        public long feedbackId { get; set; }
        public Feedback feedback { get; set; }

        //Achar uma bibioteca para audio estilo a de imagem
        protected string audio;
        public string Audio {
            get => audio;
            set => audio = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Titulo da Audio");
        }
    }
}
