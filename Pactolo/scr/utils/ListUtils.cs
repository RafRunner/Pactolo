using System;
using System.Collections.Generic;
using System.Text;

namespace Pactolo.scr.utils {
    class ListUtils {

        private static readonly Random random = new Random(Guid.NewGuid().GetHashCode());

        public static string Join<T>(List<T> lista, string token) {
            StringBuilder resultado = new StringBuilder();
            int tamanho = lista.Count;

            for (int i = 0; i < tamanho; i++) {
                T obj = lista[i];

                if (i + 1 == tamanho) {
                    resultado.Append(obj.ToString());
                }
                else {
                    resultado.Append(obj.ToString()).Append(token);
                }
            }
            return resultado.ToString();
        }

        // Só funciona com objetos
        public static void EmbaralhaMudandoPosicao<T>(List<T> lista) {
            List<T> temp = new List<T>(lista);
            int tamanho = lista.Count;

            for (int i = 0; i < tamanho; i++) {
                int newPos = random.Next(tamanho);

                while (newPos == i || temp[newPos] == null) {
                    newPos = random.Next(tamanho);
                }

                lista[i] = temp[newPos];
                temp[newPos] = default(T);
            }
        }
    }
}
