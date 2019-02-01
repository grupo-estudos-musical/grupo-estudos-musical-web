namespace GrupoEstudosMusical.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableAluno : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alunos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        DataNascimento = c.DateTime(nullable: false, storeType: "date"),
                        Telefone = c.String(maxLength: 10, unicode: false),
                        Celular = c.String(maxLength: 11, unicode: false),
                        Rg = c.String(maxLength: 9, unicode: false),
                        Cpf = c.String(maxLength: 11, unicode: false),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                        Cep = c.String(maxLength: 8, unicode: false),
                        Endereco = c.String(nullable: false, maxLength: 180, unicode: false),
                        Bairro = c.String(nullable: false, maxLength: 60, unicode: false),
                        Complemento = c.String(maxLength: 50, unicode: false),
                        Cidade = c.String(nullable: false, maxLength: 60, unicode: false),
                        Uf = c.String(nullable: false, maxLength: 2, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Rg, unique: true)
                .Index(t => t.Cpf, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Alunos", new[] { "Cpf" });
            DropIndex("dbo.Alunos", new[] { "Rg" });
            DropTable("dbo.Alunos");
        }
    }
}
