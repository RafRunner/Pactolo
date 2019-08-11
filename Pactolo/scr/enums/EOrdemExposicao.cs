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

        public static string ParseAndValidate(string OrdemExposicao) {
            OrdemExposicao = StringUtils.ValideNaoNuloNaoVazioENormalize(OrdemExposicao, "Sexo");
            try {
                string valorValidado;
                valorValidado = Enum.Parse(typeof(OrdemExposicao), OrdemExposicao).ToString();
                return valorValidado;
            } catch (Exception ignored) {
                throw new Exception($"Valor {OrdemExposicao} não é válido para Sexo!");
            }
        }
    }
}
