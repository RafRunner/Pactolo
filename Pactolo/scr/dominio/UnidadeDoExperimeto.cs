using Pactolo.scr.services;
using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	class UnidadeDoExperimento : ElementoDeBanco {
	
        public string CaminhoImagem { get; set; }
        private Image cache;
		public Image Imagem {
            get {
                if (cache == null) {
                    cache = ImagemService.GetImageByName(CaminhoImagem);
                }
                return cache;
            }
        }

        public long FeedbackId { get; set; }
		private Feedback feedback;
        public Feedback Feedback {
			get => feedback;
			set { feedback = value; FeedbackId = GetId(value); }
		}

		public string CaminhoAudio { get; set; }
    }
}
