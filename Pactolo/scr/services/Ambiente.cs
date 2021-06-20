using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Pactolo.scr.services {
    class Ambiente {

        public static string GetDiretorioPastas() {
            return Directory.GetCurrentDirectory();
        }

        public static string GetDesktop() {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        public static string GetNomeArquivo(string caminhoArquivo) {
            Match nome = Regex.Match(caminhoArquivo, @"[^\\]+$");
            return nome.Value;
        }

        public static string GetFullPath(string nomePasta, string nomeArquivo = "") {
            string caminhoPasta = GetDiretorioPastas() + "\\" + nomePasta;
            if (string.IsNullOrEmpty(nomeArquivo)) {
                return caminhoPasta;
            }
            return caminhoPasta + "\\" + nomeArquivo;
        }
    }
}
