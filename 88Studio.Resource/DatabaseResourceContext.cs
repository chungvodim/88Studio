using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _88Studio.Resource
{
    public class DatabaseResourceContext : DbContext
    {
        static DatabaseResourceContext()
        {
            Database.SetInitializer<DatabaseResourceContext>(null);
        }

        // Your context has been configured to use a 'BEGAIT' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // '_88Studio.Repository' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'BEGAIT' 
        // connection string in the application configuration file.
        public DatabaseResourceContext()
                : base("name=BEGAIT")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Resource> LocalizationResources { get; set; }
    }

    [Table("LocalizationResources")]
    public class Resource
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocalizationResourceID { get; set; }
        [Required, StringLength(5)]
        [Index("IX_UniqueResource", 1, IsUnique = true)]
        public string LanguageCode { get; set; }
        [Required, StringLength(100)]
        [Index("IX_UniqueResource", 2, IsUnique = true)]
        public string Key { get; set; }
        [Required(ErrorMessageResourceType = typeof(ErrorMessageResource), ErrorMessageResourceName = "IsRequired")]
        public string Value { get; set; }
        public LocalizationResourceGroup? Group { get; set; }
    }

    public enum LocalizationResourceGroup
    {
        Section = 1,
        SubSection = 2,
        Category = 3,
        HomePage = 4,
        AccountPage = 5,
        ListingPage = 6,
        LeadPage = 7,
        OptimizeAdPage = 8
    }

}
