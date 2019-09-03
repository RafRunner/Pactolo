﻿using Dapper;
using Pactolo.scr.dominio;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Pactolo.scr.services {

    class UnidadeDoExperimentoService : AbstractService {

        public static UnidadeDoExperimento GetById(long id) {
            if (id == 0) {
                return null;
            }
            UnidadeDoExperimento unidadeDoExperimeto = AbstractService.GetById<UnidadeDoExperimento>(id, "UnidadeDoExperimento");
            unidadeDoExperimeto.Feedback = FeedbackService.GetById(unidadeDoExperimeto.FeedbackId);
            return unidadeDoExperimeto;
        }

        public static void Salvar(List<UnidadeDoExperimento> unidadesDoExperimento) {
            foreach (UnidadeDoExperimento ui in unidadesDoExperimento) {
                Salvar(ui);
            }
        }

        public static void Salvar(UnidadeDoExperimento unidadeDoExperimento) {
            AbstractService.Salvar(unidadeDoExperimento,
                "UnidadeDoExperimento",
                "INSERT INTO UnidadeDoExperimento (NomeImagem, FeedbackId, NomeAudio) VALUES (@NomeImagem, @FeedbackId, @NomeAudio); SELECT CAST(last_insert_rowid() as int)",
                "UPDATE UnidadeDoExperimento SET NomeImagem = @NomeImagem, FeedbackId = @FeedbackId, NomeAudio = @NomeAudio WHERE Id = @Id");
        }

        public static void Deletar(UnidadeDoExperimento unidadeDoExperimeto) {
            Feedback feedback = FeedbackService.GetById(unidadeDoExperimeto.FeedbackId);
            FeedbackService.Deletar(feedback);
            AbstractService.Deletar(unidadeDoExperimeto, "UnidadeDoExperimento");
        }
    }
}
