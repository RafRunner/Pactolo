using Pactolo.scr.services;
using System.Drawing;
using System.Media;

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
                if (!string.IsNullOrEmpty(nomeAudio)) {
                    soundPlayer = new SoundPlayer(@AudioService.GetFullPath(nomeAudio));
                }
            }
        }

        public void PlayAudio() {
            if (soundPlayer != null) {
                soundPlayer.Stop();
                soundPlayer.Play();
            }
        }

		public override bool Equals(object obj) {
			if (obj.GetType() != GetType()) {
                return false;
			}

            return ((UnidadeDoExperimento)obj).Id.Equals(Id);
		}
	}
}
