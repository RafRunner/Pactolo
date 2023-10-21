using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Pactolo.scr.services {
    class Ambiente {

        private static readonly string NOME_PASTA_ERROS = "Erros";
        private static readonly string NOME_ARQUIVO_LOG_ERRO = "log erros.txt";

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

        public static void RegistraLogErro(Exception e) {
            if (!Directory.Exists(NOME_PASTA_ERROS)) {
                Directory.CreateDirectory(NOME_PASTA_ERROS);
			}
            using (var arquivoLog = File.AppendText(GetFullPath(NOME_PASTA_ERROS, NOME_ARQUIVO_LOG_ERRO))) {
                var agora = DateTime.Now;
                arquivoLog.WriteLine($"{agora}: Erro: {e.Message}");
                arquivoLog.WriteLine($"{agora}: Stack Trace: {e.StackTrace}");
            }
        }
    }
}
