using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private TransactionScope _transaction;

        public void StartTransaction()
        {
            _transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        public void CommitTransaction()
        {
            _transaction.Complete();
        }

        public void Dispose()
        {
            if( _transaction != null )
            {
                _transaction.Dispose();
            }
        }
    }
}
