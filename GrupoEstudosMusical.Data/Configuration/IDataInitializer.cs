namespace GrupoEstudosMusical.Data.Configuration
{
    public interface IDataInitializer
    {
        void Seed();
        void ExecuteMigrations();
    }
}
