using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Client> ClientRepository { get; }
        IRepository<Credit> CreditRepository { get; }
        void Commit();
    }
}
