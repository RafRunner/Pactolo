using System;

namespace Pactolo.scr.utils {

	class NumericUtils {

		public static int ValidarInteiroPositivoDentroDeLimite(int numero, int limite, string nomeCampo) {
			if (numero < 0) {
				throw new Exception($"Campo numérico {nomeCampo} não pode ser negativo!");
			}
			if (numero > limite) {
				throw new Exception($"Campo numérico {nomeCampo} não pode ser maior que {limite}!");
			}
			return numero;
		}
	}
}
