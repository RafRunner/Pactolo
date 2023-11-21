using Pactolo.scr.dominio;
using System;
using System.Collections.Generic;

namespace Pactolo.scr.services {
    class SessaoService : AbstractService{

        public static Sessao GetById(long id) {
            if (id == 0) {
                return null;
            }
            Sessao sessao = GetById<Sessao>(id, "Sessao");
            ObterObjetosFilhos(sessao);
            return sessao;
        }

        public static List<Sessao> GetAllByIds(List<long> ids) {
            List<Sessao> sessoes = new List<Sessao>();
            foreach(long id in ids) {
                if (id == 0) {
                    return null;
                }
                Sessao sessao = GetById<Sessao>(id, "Sessao");
                ObterObjetosFilhos(sessao);
                sessoes.Add(sessao);
            }           
            return sessoes;
        }

        public static void ObterObjetosFilhos(Sessao sessao) {
            sessao.CCs = CCPorSessaoService.GetAllCCBySessaoId(sessao.Id);
            sessao.Instrucao = InstrucaoService.GetById(sessao.IdInstrucao);
        }

        public static List<Sessao> GetAll() {
            List<Sessao> sessoes = GetAll<Sessao>("Sessao");
            foreach (Sessao sessao in sessoes) {
                ObterObjetosFilhos(sessao);
            }
            return sessoes;
        }

        public static void Salvar(Sessao sessao) {
            if(sessao.CriterioNumeroTentativas == 0 && sessao.CriterioAcertosConcecutivos == 0 && sessao.CriterioDuracaoSegundos == 0) {
                throw new Exception("Uma sessão não pode ser criada sem possuir ao menos um critério de encerramento.");
            }
            if (sessao.CCs.Count == 0) {
                throw new Exception("Uma sessão deve possuir pelo menos um MTS");
            }
            Salvar(sessao,
                "Sessao",
                "INSERT INTO Sessao (Nome, OrdemAleatoria, CriterioNumeroTentativas, CriterioDuracaoSegundos, CriterioAcertosConcecutivos, IdInstrucao, SegundosPorTentativa) VALUES (@Nome, @OrdemAleatoria, @CriterioNumeroTentativas, @CriterioDuracaoSegundos, @CriterioAcertosConcecutivos, @IdInstrucao, @SegundosPorTentativa); SELECT CAST(last_insert_rowid() as int)",
                "UPDATE Sessao SET Nome = @Nome, OrdemAleatoria = @OrdemAleatoria, CriterioNumeroTentativas = @CriterioNumeroTentativas, CriterioDuracaoSegundos = @CriterioDuracaoSegundos, CriterioAcertosConcecutivos = @CriterioAcertosConcecutivos IdInstrucao = @IdInstrucao, SegundosPorTentativa = @SegundosPorTentativa WHERE Id = @Id");
            CCPorSessaoService.SalvarAll(sessao.Id, sessao.CCs);
        }

        public static void Deletar(Sessao sessao) {
            Deletar(sessao, "Sessao");
            CCPorSessaoService.DeletarAllBySessaoId(sessao.Id);
        }
    }
}
