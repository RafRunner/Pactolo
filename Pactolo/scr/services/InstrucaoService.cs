using Dapper;
using Pactolo.scr.dominio;
using Pactolo.scr.dominio.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.services {
    class InstrucaoService : AbstractService {

        public static Instrucao GetById(long id) {
            return AbstractService.GetById<Instrucao>(id, "Instrucao");
        }

        public static List<object> GetAll() {
            return AbstractService.GetAll<Instrucao>("Instrucao").Cast<object>().ToList();
        }

        private static Instrucao GetByTexto(string texto) {
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                IEnumerable<Instrucao> instrucoes = cnn.Query<Instrucao>("SELECT * FROM Instrucao WHERE Texto = @Texto", new { Texto = texto });
                return instrucoes.Count() == 1 ? instrucoes.Single() : null;
            }
        }

        public static void Salvar(Instrucao instrucao) {
            Instrucao instrucaoExistente = GetByTexto(instrucao.Texto);
            if (instrucaoExistente != null) {
                instrucao.Id = instrucaoExistente.Id;
                return;
            }

            AbstractService.Salvar<Instrucao>(
                instrucao,
                "Instrucao",
                "INSERT INTO Instrucao (Texto) VALUES(@Texto); SELECT CAST(last_insert_rowid() as int)",
                "UPDATE Instrucao SET Texto = @Texto WHERE Id = @Id"
            );
        }

        public static void Deletar(Instrucao intrucao) {
            AbstractService.Deletar(intrucao, "Instrucao");
        }

        public static void DeletarPorId(long id) {
            AbstractService.DeletarPorId(id, "Instrucao");
        }

        public static List<object> FilterDataTable(DTOFiltro dtoFiltro) {
            List<Instrucao> instrucoes = dtoFiltro.Itens.Cast<Instrucao>().ToList();
            string textoDeBusca = dtoFiltro.TextoDeBusca.ToLower();

            return instrucoes.FindAll(instrucao => {
                if (instrucao.Texto.ToLower().Contains(textoDeBusca)) {
                    return true;
                }
                return false;
            }).Cast<object>().ToList();
        }
    }
}
