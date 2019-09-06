using Pactolo.scr.dominio;
using System.Collections.Generic;

namespace Pactolo.scr.services {
    class SessaoService : AbstractService{

        public static Sessao GetById(long id) {
            if (id == 0) {
                return null;
            }
            Sessao sessao = AbstractService.GetById<Sessao>(id, "Sessao");
            ObterObjetosFilhos(sessao);
            return sessao;
        }

        public static List<Sessao> GetAllByIds(List<long> ids) {
            List<Sessao> sessoes = new List<Sessao>();
            foreach(long id in ids) {
                if (id == 0) {
                    return null;
                }
                Sessao sessao = AbstractService.GetById<Sessao>(id, "Sessao");
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
            List<Sessao> sessoes = AbstractService.GetAll<Sessao>("Sessao");
            foreach (Sessao sessao in sessoes) {
                ObterObjetosFilhos(sessao);
            }
            return sessoes;
        }

        public static void Salvar(Sessao sessao) {
            AbstractService.Salvar(sessao,
                "Sessao",
                "INSERT INTO Sessao (Nome, OrdemAleatoria, CriterioNumeroTentativas, CriterioDuracaoSegundos, CriterioAcertosConcecutivos, IdInstrucao) VALUES (@Nome, @OrdemAleatoria, @CriterioNumeroTentativas, @CriterioDuracaoSegundos, @CriterioAcertosConcecutivos, @IdInstrucao); SELECT CAST(last_insert_rowid() as int)",
                "UPDATE Sessao SET Nome = @Nome, OrdemAleatoria = @OrdemAleatoria, CriterioNumeroTentativas = @CriterioNumeroTentativas, CriterioDuracaoSegundos = @CriterioDuracaoSegundos, CriterioAcertosConcecutivos = @CriterioAcertosConcecutivos IdInstrucao = @IdInstrucao WHERE Id = @Id");
            CCPorSessaoService.SalvarAll(sessao.Id, sessao.CCs);
        }

        public static void Deletar(Sessao sessao) {
            AbstractService.Deletar(sessao, "Sessao");
            CCPorSessaoService.DeletarAllBySessaoId(sessao.Id);
        }
    }
}
