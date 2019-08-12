using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

    class UnidadeDoExperimeto : ElementoDeBanco {

        // TODO decidir se vamos pegar imagem por nome em uma pasta ou salvar no banco com Id
        // TODO fazer um validador dos foramatos de imagens
        protected string nomeImagem;
        public string NomeImagem {
            get => nomeImagem;
            set => nomeImagem = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Titulo da Imagem");
        }

        public Feedback feedback { get; set; }

        // TODO decidir se vamos pegar audio por nome em uma pasta ou salvar no banco com Id
        protected string nomeArquivoAudio;
        public string NomeArquivoAudio {
            get => nomeArquivoAudio;
            set => nomeArquivoAudio = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Titulo da Audio");
        }
    }
}
