using Pactolo.scr.services;
using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	public class UnidadeDoExperimento : ElementoDeBanco {
	
        public string NomeImagem { get; set; }
        private Image cache;
		public Image Imagem {
            get {
                if (cache == null) {
                    cache = ImagemService.GetImageByName(NomeImagem);
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

        private SoundPlayer soundPlayer;
        private string nomeAudio;
        public string NomeAudio {
            get => nomeAudio;
            set {
                nomeAudio = value;
                soundPlayer = new SoundPlayer(@AudioService.GetFullPath(nomeAudio));
            }
        }

        public void PlayAudio() {
            if (soundPlayer != null) {
                soundPlayer.Play();
            }
        }
    }
}
