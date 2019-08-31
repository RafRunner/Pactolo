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

		public static void Salvar(Feedback feedback) {
			AbstractService.Salvar<Feedback>(
				feedback,
				"Feedback",
				"INSERT INTO Feedback (ValorClick, Neutro) VALUES (@ValorClick, @Neutro, @SemCor); SELECT CAST(last_insert_rowid() as int)",
				"UPDATE Feedback SET ValorClick = @ValorClick, Neutro = @Neutro, @SemCor WHERE Id = @Id"
			);
		}

		public static void Deletar(Feedback feedback) {
			AbstractService.Deletar(feedback, "Feedback");
		}
	}
}
