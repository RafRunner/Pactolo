﻿using System;
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

        public static Boolean ValideEmail(string email)
        {
            string emailRegex = string.Format("{0}{1}",
                 @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            if(emailValido = Regex.IsMatch(email,emailRegex))
            {
                return true;
            }
            return false;
        }
    }
}
