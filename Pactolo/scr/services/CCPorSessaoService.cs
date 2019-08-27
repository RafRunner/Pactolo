using Dapper;
using Pactolo.scr.dominio;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Pactolo.scr.services {
    class CCPorSessaoService : AbstractService{
 
        public static List<ContingenciaColateral> GetAllCCBySessaoId(long id) {
            if (id == 0) {
                return null;
            }
            List<long> CCsIds;
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                CCsIds = cnn.Query<long>($"SELECT CCId FROM CCPorSessao WHERE SessaoId = {id}").ToList<long>();
            }
            List<ContingenciaColateral> CCs = new List<ContingenciaColateral>();
            foreach (long CCId in CCsIds) {
                CCs.Add(ContingenciaColateralService.GetById(CCId));
            }
            return CCs;
        }

        public static void SalvarAll(long sessaoId, List<ContingenciaColateral> CCs) {
            foreach (ContingenciaColateral CC in CCs) {
                using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                    string sqlInsert = $"INSERT INTO CCPorSessao (sessaoId, CCId) VALUES ({sessaoId}, {CC.Id});)";
                    cnn.Query<long>(sqlInsert).Single();
                }
            }
        }

        public static void DeletarAllBySessaoId(long id) {
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                cnn.Execute($"DELETE FROM CCPorSessao WHERE SessaoId = {id}");
            }
        }

        public static void DeletarByCCId(long id) {
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                cnn.Execute($"DELETE FROM CCPorSessao WHERE CCId = {id}");
            }
        }
    }
}
