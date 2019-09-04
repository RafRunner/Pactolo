using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pactolo.scr.services {
    class ImagemService {

        private static readonly string PASTA_IMAGENS = "Imagens";

        public static string GetFullPath(string nomeImagem = "") {
            return Ambiente.GetFullPath(PASTA_IMAGENS, nomeImagem);
        }

        private static void CreateDirectoryIfNotExists() {
            if (!Directory.Exists(GetFullPath())) {
                Directory.CreateDirectory(GetFullPath());
            }
        }

        public static Image GetImageByName(string nomeImagem) {
            string caminhoCompleto = GetFullPath(nomeImagem);
            return Image.FromFile(caminhoCompleto);
        }

        public static string CopiaImagemParaPasta(string caminhoImagem) {
            CreateDirectoryIfNotExists();
            // Se não é um caminho já está na pasta e o caminho já é o nome
            if (!caminhoImagem.Contains("\\")) {
                return caminhoImagem;
            }

            string nome = Ambiente.GetNomeArquivo(caminhoImagem);
            string novoCaminho = GetFullPath(nome);

            if (File.Exists(novoCaminho) && ImageUtils.ImageToByteArray(Image.FromFile(novoCaminho)).SequenceEqual(ImageUtils.ImageToByteArray(Image.FromFile(caminhoImagem)))) {
                return nome;
            }
            if (File.Exists(novoCaminho)) {
                throw new Exception($"Já existe uma imagem com o nome {nome}! Por favor, a renomeie");
            }

            File.Copy(caminhoImagem, novoCaminho);
            return nome;
        }
    }
}
