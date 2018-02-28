using System.Data.Entity;

namespace UsingExternalDataInCallPro.Models
{
    public class DataContext : DbContext
    {
    
        public DataContext() : base("name=DataContext")
        {
            // For production we disable EF migrations so the SQL Database dictates the model
            //Database.SetInitializer<DataContext>(null);
            // During development we want to have the database created with our changed model and start out with a clean database
            Database.SetInitializer<DataContext>(new DropCreateDatabaseAlways<DataContext>());
            //Database.SetInitializer<DataContext>(new CreateDatabaseIfNotExists<DataContext>());
            //Database.SetInitializer<DataContext>(new DropCreateDatabaseIfModelChanges<DataContext>());

        }

        public DbSet<Lookup> Lookup { get; set; }
    }
}
