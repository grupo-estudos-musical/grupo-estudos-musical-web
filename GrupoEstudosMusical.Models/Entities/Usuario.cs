﻿using GrupoEstudosMusical.Models.Enums;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public NivelAcessoEnum NivelAcesso { get; set; }
        public string Guid { get; set; }
        public Aluno Aluno { get; set; }
        public Professor Professor { get; set; }
        public List<InstrumentoDoAluno> InstrumentosDoAluno { get; set; }
    }
}
