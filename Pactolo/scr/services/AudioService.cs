using System;
using System.IO;
using System.Linq;

namespace Pactolo.scr.services {
    class AudioService {

        private static readonly string PASTA_IMAGENS = "Audios";

        private static string GetFullPath(string nomeAudio = "") {
            string caminhoPasta = Ambiente.GetDiretorioPastas() + "\\" + PASTA_IMAGENS;
            if (nomeAudio == "") {
                return caminhoPasta;
            }
            return caminhoPasta + "\\" + nomeAudio;
        }

        private static void CreateDirectoryIfNotExists() {
            if (!Directory.Exists(GetFullPath())) {
                Directory.CreateDirectory(GetFullPath());
            }
        }

        public static Byte[] GetAudioByName(string nomeImagem) {
            string caminhoCompleto = GetFullPath(nomeImagem);
            return File.ReadAllBytes(caminhoCompleto);
        }

        public static string CopiaImagemParaPasta(string caminhoAudio) {
            CreateDirectoryIfNotExists();
            string nome = Ambiente.GetNomeArquivo(caminhoAudio);

            string novoCaminho = GetFullPath(nome);

            if (File.Exists(novoCaminho) && File.ReadAllBytes(novoCaminho).SequenceEqual(File.ReadAllBytes(caminhoAudio))) {
                return nome;
            }
            if (File.Exists(novoCaminho)) {
                throw new Exception($"Já existe um audio com o nome {nome}! Por favor, o renomeie");
            }

            File.Copy(caminhoAudio, novoCaminho);
            return nome;
        }
    }
}
