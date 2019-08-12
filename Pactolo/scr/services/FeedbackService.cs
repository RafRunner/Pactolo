using Dapper;
using Pactolo.scr.dominio;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Pactolo.scr.services {
    class FeedbackService : AbstractService {

        public static Feedback GetById(long id) {
			return AbstractService.GetById<Feedback>(id, "Feedback");
		}

		public static List<Feedback> GetAll() {
			return AbstractService.GetAll<Feedback>("Feedback");
		}

		public static void Salvar(Feedback feedback) {

            Feedback feedbackExistente = GetById(feedback.Id);
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {

                if (feedbackExistente == null) {
					long id = cnn.Query<long>("INSERT INTO Feedback (ValorClick, Neutro) VALUES (@ValorClick, @Neutro); SELECT CAST(last_insert_rowid() as int)", feedback).Single();
					feedback.Id = id;
				} else {
                    cnn.Execute("UPDATE Feedback SET ValorClick = @ValorClick, Neutro = @Neutro WHERE Id = @Id", feedback);
                }
            }
        }

        public static void Deletar(Feedback feedback) {
			AbstractService.Deletar(feedback, "Feedback");
		}
    }
}
