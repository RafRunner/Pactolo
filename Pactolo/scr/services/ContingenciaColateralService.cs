using Dapper;
using Pactolo.scr.dominio;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Pactolo.scr.services {

    class ContingenciaColateralService : AbstractService {

        public static ContingenciaColateral GetById(long id) {
            if (id == 0) {
                return null;
            }
            ContingenciaColateral contingenciaColateral = AbstractService.GetById<ContingenciaColateral>(id, "ContingenciaColateral");
            ObterObjetosFilhos(contingenciaColateral);
            return contingenciaColateral;
        }

        private static void ObterObjetosFilhos(ContingenciaColateral contingenciaColateral) {
            contingenciaColateral.sModelo = UnidadeDoExperimentoService.GetById(contingenciaColateral.sModeloId);
            contingenciaColateral.SC1 = UnidadeDoExperimentoService.GetById(contingenciaColateral.SC1Id);
            contingenciaColateral.SC2 = UnidadeDoExperimentoService.GetById(contingenciaColateral.SC2Id);
            contingenciaColateral.SC3 = UnidadeDoExperimentoService.GetById(contingenciaColateral.SC3Id);
            contingenciaColateral.CI = ContingenciaInstrucionalService.GetById(contingenciaColateral.CIId);
        }

        public static List<ContingenciaColateral> GetAll() {
            List<ContingenciaColateral> contingenciasColaterais = AbstractService.GetAll<ContingenciaColateral>("ContingenciaColateral");
            foreach (ContingenciaColateral CC in contingenciasColaterais) {
                ObterObjetosFilhos(CC);
            }
            return contingenciasColaterais;
        }

        public static List<ContingenciaColateral> GetAllByCI(ContingenciaInstrucional CI) {
            if (CI == null || CI.Id == 0) {
                return new List<ContingenciaColateral>();
            }

            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                return cnn.Query<ContingenciaColateral>($"SELECT * FROM ContingenciaColateral WHERE @CIId = @Id", CI).ToList<ContingenciaColateral>();
            }
        }

        // TODO ao salvar o objeto pai, deverá dar cascade nos objetos filhos e salvar eles também? Acho melhor sim
        public static void Salvar(ContingenciaColateral contingenciaColateral) {
            AbstractService.Salvar(contingenciaColateral,
                "ContingenciaColateral",
                "INSERT INTO ContingenciaColateral (Nome, sModeloId, SC1Id, SC2Id, SC3Id, CIId) VALUES (@Nome, @sModeloId, @SC1Id, @SC2Id, @SC3Id, @CIId); SELECT CAST(last_insert_rowid() as int)",
                "UPDATE ContingenciaColateral SET Nome = @Nome, sModeloId = @sModeloId, SC1Id = @SC1Id, SC2Id = @SC2Id, SC3Id = @SC3Id, CIId = @CIId WHERE Id = @Id");
        }

        public static void Deletar(ContingenciaColateral CC) {
            CCPorSessaoService.DeletarAllByCCId(CC.Id);
            UnidadeDoExperimentoService.DeletarAll(new List<UnidadeDoExperimento>() { CC.sModelo, CC.SC1, CC.SC2, CC.SC3 });
            AbstractService.Deletar(CC, "ContingenciaColateral");
        }
    }
}
