using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.utils {

	class StringUtils {

		public static string ValideNaoNuloNaoVazioENormalizeString(string palavra, string nomeCampo) {
			string normalizada = palavra?.Trim();
			if (string.IsNullOrEmpty(normalizada)) {
				throw new Exception($"Campo normalizado {nomeCampo} está nulo ou vazio!");
			}
			return normalizada;
		}
	}
}
