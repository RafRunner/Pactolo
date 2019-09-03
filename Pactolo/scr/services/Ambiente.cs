using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pactolo.scr.services {
    class Ambiente {

        public static string GetDiretorioPastas() {
            return Directory.GetCurrentDirectory();
        }

        public static string GetNomeArquivo(string caminhoArquivo) {
            Match nome = Regex.Match(caminhoArquivo, @"[^\\]+$");
            return nome.Value;
        }
    }
}
