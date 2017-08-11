using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPlus
{
    public class DBConn : DbContext
    {
        public DBConn() : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=testedb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {
            Database.SetInitializer<DBConn>(new CreateDatabaseIfNotExists<DBConn>());
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Fechamento> Fechamentos { get; set; }
    }
}
