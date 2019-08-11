using Dapper;
using Pactolo.scr.dominio;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Pactolo.scr.services {
    class ContingenciaInstrucionalService : AbstractService{

        public static ContingenciaInstrucional GetById(long id) {
            if (id <= 0) {
                return null;
            }
            ContingenciaInstrucional contingenciaInstrucional;
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                contingenciaInstrucional = cnn.Query<ContingenciaInstrucional>("SELECT * FROM ContingenciaInstrucional WHERE Id = @Id", new { Id = id })?.Single();
            }
            contingenciaInstrucional.Tato1 = UnidadeDoExperimentoService.GetById(contingenciaInstrucional.Tato1Id);
            contingenciaInstrucional.Autoclitico = UnidadeDoExperimentoService.GetById(contingenciaInstrucional.AutocliticoId);
            contingenciaInstrucional.Tato2 = UnidadeDoExperimentoService.GetById(contingenciaInstrucional.Tato2Id);
            return contingenciaInstrucional;
        }

        public static ContingenciaInstrucional Salvar(ContingenciaInstrucional contingenciaInstrucional) {

            ContingenciaInstrucional ContingenciaInstrucionalExistente = GetById(contingenciaInstrucional.Id);
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                long id;

                if (ContingenciaInstrucionalExistente == null) {
                    id = cnn.Query<long>("INSERT INTO contingenciaInstrucional (Tato1Id, AutocliticoId, Tato2Id) VALUES (@Tato1, @Autoclitico, @Tato2); SELECT CAST(last_insert_rowid() as int)", contingenciaInstrucional).Single();
                } else {
                    cnn.Execute("UPDATE contingenciaInstrucional SET Tato1Id = @Tato1, AutocliticoId = @Autoclitico, Tato2Id = @Tato2 WHERE Id = @Id", contingenciaInstrucional);
                    id = contingenciaInstrucional.Id;
                    contingenciaInstrucional = GetById(id);
                }

                return GetById(id);
            }
        }

        public static void Deletar(ContingenciaInstrucional contingenciaInstrucional) {
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                string command;
                command = "DELETE FROM ContingenciaInstrucional WHERE Id = @Id";
                cnn.Execute(command, contingenciaInstrucional);
                contingenciaInstrucional.Id = 0;
            }
        }
    }
}
