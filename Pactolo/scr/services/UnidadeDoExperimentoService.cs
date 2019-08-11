using Dapper;
using Pactolo.scr.dominio;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Pactolo.scr.services {
    class UnidadeDoExperimentoService : AbstractService {

        public static UnidadeDoExperimeto GetById(long id) {
            if (id <= 0) {
                return null;
            }
            UnidadeDoExperimeto unidadeDoExperimeto;
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                unidadeDoExperimeto = cnn.Query<UnidadeDoExperimeto>("SELECT * FROM UnidadeDoExperimeto WHERE Id = @Id", new { Id = id })?.Single();
            }
            unidadeDoExperimeto.feedback = FeedbackService.GetById(unidadeDoExperimeto.feedbackId);
            return unidadeDoExperimeto;
        }

        public static UnidadeDoExperimeto Salvar(UnidadeDoExperimeto unidadeDoExperimeto) {
            UnidadeDoExperimeto unidadeDoExperimetoExistente = GetById(unidadeDoExperimeto.Id);
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                long id;

                if (unidadeDoExperimetoExistente == null) {
                    id = cnn.Query<long>("INSERT INTO UnidadeDoExperimeto (Imagem, feedbackId, Audio) VALUES (@Imagem, @feedback, @Audio); SELECT CAST(last_insert_rowid() as int)", unidadeDoExperimeto).Single();
                } else {
                    cnn.Execute("UPDATE UnidadeDoExperimeto SET Imagem = @Imagem, feedbackId = @feedback, Audio = @Audio WHERE Id = @Id", unidadeDoExperimeto);
                    id = unidadeDoExperimeto.Id;
                    unidadeDoExperimeto = GetById(id);
                }

                return GetById(id);
            }
        }

        public static void Deletar(UnidadeDoExperimeto unidadeDoExperimeto) {
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                string command;
                command = "DELETE FROM UnidadeDoExperimeto WHERE Id = @Id";
                cnn.Execute(command, unidadeDoExperimeto);
                unidadeDoExperimeto.Id = 0;
            }
        }
    }
}
