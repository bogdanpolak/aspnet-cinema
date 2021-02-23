namespace API.Cinema.InitializeDatabase
{
    public interface IDbInitializer
    {
        void Initialize();
        void SeedData();
    }
}