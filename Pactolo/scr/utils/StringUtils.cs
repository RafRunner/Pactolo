using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.utils {

	class StringUtils {

		public static string Normalize(string sequencia) {
			return sequencia?.Trim();
		}

		public static string ValideNaoNuloNaoVazioENormalize(string sequencia, string nomeCampo) {
			string normalizada = Normalize(sequencia);
			if (string.IsNullOrEmpty(normalizada)) {
				throw new Exception($"Campo normalizado {nomeCampo} está nulo ou vazio!");
			}
			return normalizada;
		}
	}
}
