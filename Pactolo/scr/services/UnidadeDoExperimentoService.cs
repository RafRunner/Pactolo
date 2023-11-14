using Pactolo.scr.dominio;
using System.Collections.Generic;
namespace Pactolo.scr.services {

    class UnidadeDoExperimentoService : AbstractService {

        public static UnidadeDoExperimento GetById(long id) {
            if (id == 0) {
                return null;
            }
            var unidadeDoExperimeto = GetById<UnidadeDoExperimento>(id, "UnidadeDoExperimento");
            unidadeDoExperimeto.Feedback = FeedbackService.GetById(unidadeDoExperimeto.FeedbackId);
            return unidadeDoExperimeto;
        }

        public static void Salvar(List<UnidadeDoExperimento> unidadesDoExperimento) {
            foreach (UnidadeDoExperimento ui in unidadesDoExperimento) {
				Salvar(ui);
            }
        }

        public static void Salvar(UnidadeDoExperimento unidadeDoExperimento) {
            if (string.IsNullOrEmpty(unidadeDoExperimento.NomeImagem)) {
                throw new System.Exception("Toda unidade do experimento deve ter uma imagem!");
            }

			unidadeDoExperimento.NomeImagem = ImagemService.CopiaImagemParaPasta(unidadeDoExperimento.NomeImagem);
			unidadeDoExperimento.NomeAudio = AudioService.CopiaAudioParaPasta(unidadeDoExperimento.NomeAudio);

            Salvar(unidadeDoExperimento,
                "UnidadeDoExperimento",
                "INSERT INTO UnidadeDoExperimento (NomeImagem, FeedbackId, NomeAudio) VALUES (@NomeImagem, @FeedbackId, @NomeAudio); SELECT CAST(last_insert_rowid() as int)",
                "UPDATE UnidadeDoExperimento SET NomeImagem = @NomeImagem, FeedbackId = @FeedbackId, NomeAudio = @NomeAudio WHERE Id = @Id");
        }

        public static void Deletar(UnidadeDoExperimento unidadeDoExperimeto) {
            Feedback feedback = FeedbackService.GetById(unidadeDoExperimeto.FeedbackId);
            FeedbackService.Deletar(feedback);
            Deletar(unidadeDoExperimeto, "UnidadeDoExperimento");
        }

        public static void DeletarAll(List<UnidadeDoExperimento> unidadesDoExperimento) {
            foreach (UnidadeDoExperimento unidade in unidadesDoExperimento) {
                Deletar(unidade);
            }
        }

		public static List<UnidadeDoExperimento> GetAllByCI(ContingenciaInstrucional contingenciaInstrucional) {
			return GetByObj<UnidadeDoExperimento>(
				"SELECT ue.* FROM UnidadeDoExperimento ue JOIN ContigenciaInstrucionalToTato CiTT ON ue.Id = CiTT.IdUnidadeExperimento WHERE CiTT.IdCI = @Id ORDER BY CiTT.Ordem",
				contingenciaInstrucional);
		}
    }
}
