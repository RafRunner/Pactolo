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
            UnidadeDoExperimeto unidadeDoExperimeto = AbstractService.GetById<UnidadeDoExperimeto>(id, "UnidadeDoExperimeto");
            unidadeDoExperimeto.Feedback = FeedbackService.GetById(unidadeDoExperimeto.FeedbackId);
            return unidadeDoExperimeto;
        }

        public static void Salvar(UnidadeDoExperimeto unidadeDoExperimeto) {
            AbstractService.Salvar(unidadeDoExperimeto,
                "UnidadeDoExperimeto",
                "INSERT INTO UnidadeDoExperimeto (Imagem, FeedbackId, AudioId) VALUES (@Imagem, @Feedback, @AudioId); SELECT CAST(last_insert_rowid() as int)",
                "UPDATE UnidadeDoExperimeto SET Imagem = @Imagem, FeedbackId = @Feedback, AudioId = @AudioId WHERE Id = @Id");
        }

        public static void Deletar(UnidadeDoExperimeto unidadeDoExperimeto) {
            Feedback feedback = FeedbackService.GetById(unidadeDoExperimeto.FeedbackId);
            FeedbackService.Deletar(feedback);
            AbstractService.Deletar(unidadeDoExperimeto, "UnidadeDoExperimeto");
        }
    }
}
