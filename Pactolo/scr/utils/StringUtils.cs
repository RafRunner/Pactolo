using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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

		public static string ValideEmail(string email) {
            email = Normalize(email);
			string emailRegex = string.Format("{0}{1}",
				 @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
				 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

			if (!Regex.IsMatch(email, emailRegex)) {
				throw new Exception("Este email é inválido!");
			}
			return email;
		}
	}
}
