using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.enums {

	public class ESexo {

		public enum Sexo {
			Feminino,
			Masculino,
			Outro
		}

		public static List<string> Values() {
			return Enum.GetNames(typeof(Sexo)).ToList();
		}

		public static string ParseAndValidate(string sexo) {
			sexo = StringUtils.ValideNaoNuloNaoVazioENormalize(sexo, "Sexo");
			try {
				string valorValidado;
				valorValidado = Enum.Parse(typeof(Sexo), sexo).ToString();
				return valorValidado;
			} catch (Exception ignored) {
				throw new Exception($"Valor {sexo} não é válido para Sexo!");
			}
		}
	}
}
