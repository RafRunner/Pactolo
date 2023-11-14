using Pactolo.scr.dominio;

namespace Pactolo.scr.services {
	class FeedbackService : AbstractService {

		public static Feedback GetById(long id) {
			return GetById<Feedback>(id, "Feedback");
		}

		public static void Salvar(Feedback feedback) {
			Salvar(
				feedback,
				"Feedback",
                "INSERT INTO Feedback (ValorClick, Neutro, SemCor, ProbabilidadeComplementar) VALUES (@ValorClick, @Neutro, @SemCor, @ProbabilidadeComplementar); SELECT CAST(last_insert_rowid() as int)",
                "UPDATE Feedback SET ValorClick = @ValorClick, Neutro = @Neutro, SemCor = @SemCor, ProbabilidadeComplementar = @ProbabilidadeComplementar WHERE Id = @Id"
            );
		}

		public static void Deletar(Feedback feedback) {
			Deletar(feedback, "Feedback");
		}
	}
}
