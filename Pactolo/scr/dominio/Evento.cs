using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {
    public class Evento {

        public Evento(Sessao sessao, ContingenciaColateral CC, string nomeSC, long pontosGanhos, long pontosAtuais, int tentativaAtual) {
            NomeSesssao = sessao.Nome;
            NomeCC = CC.Nome;
            NomeSC = nomeSC;
            PontosGanhos = pontosGanhos;
            PontosAtuais = pontosAtuais;
            TentativaAtual = tentativaAtual;
            HoraEvento = new DateTime();
        }

        public void MarcarComoEncerramento(string criterioEncerramento, string valorEncerramento) {
            EventoEncerramento = true;
            CriterioEncerramento = criterioEncerramento;
            ValorEncerramento = valorEncerramento;
            HoraEvento = new DateTime();
        }

        public string NomeSesssao { get; set; }
        public string NomeCC  { get; set; }
        public string NomeSC { get; set; } // SC1, SC2 ou SC3
        public long PontosGanhos { get; set; }
        public long PontosAtuais { get; set; }
        public int TentativaAtual { get; set; }
        public DateTime HoraEvento { get; set; }
        public Boolean EventoEncerramento { get; set; }
        public string CriterioEncerramento { get; set; }
        public string ValorEncerramento { get; set; }
    }
}
