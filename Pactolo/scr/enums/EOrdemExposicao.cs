using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.enums {
    class EOrdemExposicao {
        public enum OrdemExposicao {
            Randomizadas,
            Agrupadas
        }

        public static List<string> Values() {
            return Enum.GetNames(typeof(OrdemExposicao)).ToList();
        }

        public static string ParseAndValidate(string ordemExposicao) {
            ordemExposicao = StringUtils.ValideNaoNuloNaoVazioENormalize(ordemExposicao, "Ordem de Exposição");
            try {
				string valorValidado = Enum.Parse(typeof(OrdemExposicao), ordemExposicao).ToString();
                return valorValidado;
            } catch (Exception ignored) {
                throw new Exception($"Valor {ordemExposicao} não é válido para Ordem de Exposição!");
            }
        }
    }
}
