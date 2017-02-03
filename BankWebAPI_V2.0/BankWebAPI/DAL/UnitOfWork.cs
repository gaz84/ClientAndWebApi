using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Repository;

namespace DAL
{
    public class UnitOfWork: DbContext, IUnitOfWork
    {
        private readonly BaseRepository<Client> clientRepo;
        private readonly BaseRepository<Credit> creditRepo;


        public DbSet<Client> Clients { get; set; }
        public DbSet<Credit> Credits { get; set; }
    
        public UnitOfWork():base("MyConnection")
        {
            clientRepo = new BaseRepository<Client>(Clients);
            creditRepo = new BaseRepository<Credit>(Credits);
        }

        public IRepository<Client> ClientRepository
        {
            get { return clientRepo; }
        }

        public IRepository<Credit> CreditRepository
        {
            get { return creditRepo; }
        }

        public void Commit()
        {
            this.SaveChanges();
        }
    }
}
