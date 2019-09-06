using Dapper;
using Pactolo.scr.dominio;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Pactolo.scr.services {
    class CCPorSessaoService : AbstractService {

        private static List<CCPorSessao> GetPropriedadeByMembroId(long id, string nomeMembro) {
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                return cnn.Query<CCPorSessao>($"SELECT CCId, SessaoId, OrdemApresentacao FROM CCPorSessao WHERE {nomeMembro} = {id}").ToList<CCPorSessao>();
            }
        }

        public static List<ContingenciaColateral> GetAllCCBySessaoId(long id) {
            if (id == 0) {
                return null;
            }
            List<CCPorSessao> CCsPS = GetPropriedadeByMembroId(id, "SessaoId");
            if (CCsPS == null) {
                return null;
            }
            List<ContingenciaColateral> CCs = new List<ContingenciaColateral>();
            CCsPS.OrderBy(it => it.OrdemApresentacao);
            foreach (CCPorSessao CCPS in CCsPS) {
                CCs.Add(ContingenciaColateralService.GetById(CCPS.CCId));
            }
            return CCs;
        }

        public static void SalvarAll(long sessaoId, List<ContingenciaColateral> CCs) {
            for (int i = 0; i < CCs.Count; i++) {
                ContingenciaColateral CC = CCs[i];
                using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                    string sqlInsert = $"INSERT INTO CCPorSessao (sessaoId, CCId, OrdemApresentacao) VALUES ({sessaoId}, {CC.Id}, {i})";
                    cnn.Execute(sqlInsert);
                }
            }
        }

        public static void DeletarAllBySessaoId(long id) {
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                cnn.Execute($"DELETE FROM CCPorSessao WHERE SessaoId = {id}");
            }
        }

        public static void DeletarAllByCCId(long id) {
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                cnn.Execute($"DELETE FROM CCPorSessao WHERE CCId = {id}");
            }
        }

        public static void DeletarCCDaSessao(long CCid, long sessaoId) {
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                cnn.Execute($"DELETE FROM CCPorSessao WHERE CCId = {CCid} AND SessaoId = {sessaoId}");
            }
        }
    }
}
