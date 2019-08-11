using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.enums {

	class EEscolaridade {

		public enum Escolaridade {
			FundamentalIncompleto,
			Fundamental,
			MedioIncompleto,
			Medio,
			SuperiorIncompleto,
			Superior,
			Acima
		}

		private static readonly Dictionary<Escolaridade, string> equivalencia = new Dictionary<Escolaridade, string> {
			{ Escolaridade.FundamentalIncompleto, "Fundamental Incompleto" },
			{ Escolaridade.Fundamental, "Fundamental" },
			{ Escolaridade.MedioIncompleto, "Médio Incompleto" },
			{ Escolaridade.Medio, "Médio" },
			{ Escolaridade.SuperiorIncompleto, "Superior Incompleto" },
			{ Escolaridade.Superior, "Superior" },
			{ Escolaridade.Acima, "Acima" }
		};

		public static List<string> Values() {
			return ((Escolaridade[]) Enum.GetValues(typeof(Escolaridade))).Select(it => GetValue(it)).ToList();
		}

		public static string GetValue(Escolaridade escolaridade) {
			return equivalencia[escolaridade];
		}

		public static string ParseAndValidate(string escolaridade) {
			escolaridade = StringUtils.ValideNaoNuloNaoVazioENormalize(escolaridade, "Escolaridade");
			try {
				string valorValidado;
				valorValidado = GetValue((Escolaridade) Enum.Parse(typeof(Escolaridade), escolaridade));
				return valorValidado;
			} catch (Exception ignored) {
				throw new Exception($"Valor {escolaridade} não é válido para Escolaridade!");
			}
		}
	}
}
