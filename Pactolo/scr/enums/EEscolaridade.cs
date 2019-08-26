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

		public static object[] Values() {
			return ((Escolaridade[]) Enum.GetValues(typeof(Escolaridade))).Select(it => GetValue(it)).ToArray();
		}

		public static string GetValue(Escolaridade escolaridade) {
			return equivalencia[escolaridade];
		}

		// Tenta obter string de equivalência de Escolaridade como foram declarados no Enum e depois como foram declarados em Equivalência. Se não conseguir retorna erro
		public static string ParseAndValidate(string escolaridade) {
			escolaridade = StringUtils.ValideNaoNuloNaoVazioENormalize(escolaridade, "Escolaridade");
			string valorValidado;
			try {
				valorValidado = GetValue((Escolaridade) Enum.Parse(typeof(Escolaridade), escolaridade));
				return valorValidado;
			} catch (Exception ignored) {
				try {
					valorValidado = equivalencia.Values.First(it => it.Equals(escolaridade));
					return valorValidado;
				} catch (Exception ignored1) {
					throw new Exception($"Valor {escolaridade} não é válido para Escolaridade!");
				}
			}
		}
	}
}
