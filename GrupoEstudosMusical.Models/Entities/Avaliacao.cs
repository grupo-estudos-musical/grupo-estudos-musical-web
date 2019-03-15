

namespace GrupoEstudosMusical.Models.Entities
{
    public class Avaliacao: BaseEntity
    {
        public Avaliacao(string nome, double notaMaxima, int peso)
        {
            this.Nome = nome;
            this.NotaMaxima = notaMaxima;
            this.Peso = peso;
        }
        public string Nome { get; private set; }
        public double NotaMaxima { get; private set; }
        public int Peso { get; private set; }
    }
}
