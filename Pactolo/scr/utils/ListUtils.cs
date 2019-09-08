using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.utils {
    class ListUtils {
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

        public static List<T> EmbaralhaMudandoPosicao<T>(List<T> lista) {
            if (lista.Count == 0) {
                return lista;
            }

            Random random = new Random();
            int tamanho = lista.Count;
            T[] embaralhada = new T[tamanho];

            List<string> posicoesDisponiveis = new List<string>();
            for (int i = 0; i < tamanho; i++) {
                posicoesDisponiveis.Add(i.ToString());
            }

            Dictionary<string, string> RelacaoPosicoes = new Dictionary<string, string>();
            int ultimaPosicaoImpar = -1;

            for (int i = 0; i < tamanho; i++) {
                if (posicoesDisponiveis.Count == 0) {
                    break;
                }

                int novaPosicao;

                if (!posicoesDisponiveis.Contains(i.ToString())) {
                    continue;
                }
                else {
                    if (posicoesDisponiveis.Count == 1) {
                        do {
                            novaPosicao = random.Next(0, tamanho);
                        } while (novaPosicao == i);

                        ultimaPosicaoImpar = novaPosicao;
                        break;
                    }
                    do {
                        novaPosicao = random.Next(i + 1, tamanho);
                    } while (!posicoesDisponiveis.Contains(novaPosicao.ToString()));
                }

                RelacaoPosicoes.Add(i.ToString(), novaPosicao.ToString());
                RelacaoPosicoes.Add(novaPosicao.ToString(), i.ToString());
                posicoesDisponiveis.Remove(novaPosicao.ToString());
                posicoesDisponiveis.Remove(i.ToString());
            }

            foreach (var entry in RelacaoPosicoes) {
                embaralhada[int.Parse(entry.Key)] = lista[int.Parse(entry.Value)];
            }

            if (ultimaPosicaoImpar != -1) {
                T temp = lista[int.Parse(posicoesDisponiveis.Single())];
                embaralhada[int.Parse(posicoesDisponiveis.Single())] = embaralhada[ultimaPosicaoImpar];
                embaralhada[ultimaPosicaoImpar] = temp;
            }

            return embaralhada.ToList();
        }
    }
}
