using Dapper;
using Pactolo.scr.dominio;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Pactolo.scr.services {
    class FeedbackService : AbstractService {

        public static Feedback GetById(long id) {
            if (id <= 0) {
                return null;
            }
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                return cnn.Query<Feedback>("SELECT * FROM Feedback WHERE Id = @Id", new { Id = id })?.Single();
            }
        }

        public static Feedback Salvar(Feedback feedback) {

            Feedback feedbackExistente = GetById(feedback.Id);
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                long id;

                if (feedbackExistente == null) {
                    id = cnn.Query<long>("INSERT INTO Feedback (ValorClick, Neutro) VALUES (@ValorClick, @Neutro); SELECT CAST(last_insert_rowid() as int)", feedback).Single();
                } else {
                    cnn.Execute("UPDATE Feedback SET ValorClick = @ValorClick, Neutro = @Neutro WHERE Id = @Id", feedback);
                    id = feedback.Id;
                    feedback = GetById(id);
                }

                return GetById(id);
            }
        }

        public static void Deletar(Feedback feedback) {
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
                string command;
                command = "DELETE FROM Experimentador WHERE Id = @Id";    
                cnn.Execute(command, feedback);
                feedback.Id = 0;
            }
        }
    }
}
