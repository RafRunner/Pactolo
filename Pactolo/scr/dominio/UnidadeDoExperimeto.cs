using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	class UnidadeDoExperimeto : ElementoDeBanco {
	
		// TODO fazer um validador dos foramatos de imagens
		public Image Imagem { get; set; }

        public long FeedbackId { get; set; }
		private Feedback feedback;
        public Feedback Feedback {
			get => feedback;
			set => feedback = ElementoDeBanco.Set<Feedback>(FeedbackId, value);
		}

		// TODO decidir se vamos pegar audio por nome em uma pasta ou salvar no banco com Id
		public long AudioId { get; set; }
    }
}
