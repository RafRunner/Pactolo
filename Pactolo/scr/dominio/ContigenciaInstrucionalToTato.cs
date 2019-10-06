using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {
	public class ContigenciaInstrucionalToTato {

		private long idCI;
		public long IdCI {
			get => idCI;
			set {
				if (value <= 0) {
					throw new Exception($"O id da CI deve ser válido! IdCi: ${value}");
				}
				idCI = value;
			}
		}
		private long idUnidadeExperimento;
		public long IdUnidadeExperimento {
			get => idUnidadeExperimento;
			set {
				if (value <= 0) {
					throw new Exception($"O id da UnidadeDoExperimento deve ser válido! IdUnidadeExperimento: ${value}");
				}
				idUnidadeExperimento = value;
			}
		}
		private int ordem;
		public int Ordem {
			get => ordem;
			set {
				if (value <= 0) {
					throw new Exception($"A ordem deve ser válido! Ordem: ${value}");
				}
				ordem = value;
			}
		}
	}
}
