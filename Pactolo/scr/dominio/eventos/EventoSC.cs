using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio.eventos {
    public class EventoSC : Evento {

        public EventoSC(Sessao sessao, ContingenciaColateral CC, string nomeSC, long pontosGanhos) : base(sessao) {
            this.CC = CC;
            NomeSC = nomeSC;
            PontosGanhos = pontosGanhos;
            PontosAtuais = sessao.NumeroPontos;
            TentativaAtual = sessao.NumeroTentativas;
        }

        public void MarcarComoEncerramento(string criterioEncerramento, string valorEncerramento) {
            EventoEncerramento = true;
            CriterioEncerramento = criterioEncerramento;
            ValorEncerramento = valorEncerramento;
            HoraEvento = DateTime.Now;
        }

        private readonly ContingenciaColateral CC;
        private readonly string NomeSC; // SC1, SC2 ou SC3
        private readonly long PontosGanhos;
        private readonly long PontosAtuais;
        private readonly int TentativaAtual;

        private string CriterioEncerramento;
        private string ValorEncerramento;

        public override string MontaMensagem() {
            StringBuilder mensagem = new StringBuilder();
            if (EventoEncerramento) {
                mensagem.AppendLine(CC.Nome + " encerrada, criterio: " + CriterioEncerramento + ", " + ValorEncerramento);
                mensagem.Append("Horário de encerramento: ").Append(HoraEvento);
            }
            else {
                mensagem.Append(NomeSesssao + "|"  + CC.Nome + "|" + NomeSC + "|" + PontosGanhos + "|" + TentativaAtual + "|" + PontosAtuais + "|" + HoraEvento.ToString("HH:mm:ss"));
            }

            return mensagem.ToString();
        }
    }
}
