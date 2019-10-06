using System;
using System.IO;
using System.Linq;

namespace Pactolo.scr.services {
    class AudioService {

        private static readonly string PASTA_AUDIOS = "Audios";

        public static string GetFullPath(string nomeAudio = "") {
            return Ambiente.GetFullPath(PASTA_AUDIOS, nomeAudio);
        }

        private static void CreateDirectoryIfNotExists() {
            if (!Directory.Exists(GetFullPath())) {
                Directory.CreateDirectory(GetFullPath());
            }
        }

        public static string CopiaAudioParaPasta(string caminhoAudio) {
			if (string.IsNullOrEmpty(caminhoAudio)) {
				return "";
			}
            CreateDirectoryIfNotExists();
            // Se não é um caminho já está na pasta e o caminho já é o nome
            if (!caminhoAudio.Contains("\\")) {
                return caminhoAudio;
            }
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
