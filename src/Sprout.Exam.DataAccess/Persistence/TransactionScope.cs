using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Sprout.Exam.DataAccess.Persistence
{

    [ExcludeFromCodeCoverage]
    /// <summary>
    /// handles the UoW for writing data
    /// </summary>
    public sealed class TransactionScope : IDisposable
    {
        private IDbContextTransaction _transaction;
        private int _scopes = 0;
        private Object _lock = new();


        public TransactionScope(IDbContextTransaction transaction)
        {
            _transaction = transaction;
            _scopes = 1;
        }
        public void Commit()
        {
            lock (_lock)
            {
                if (_scopes == 1)
                    _transaction.Commit();
            }

        }

        public void Dispose()
        {
            lock (_lock)
            {
                _scopes--;
                if (_scopes == 0)
                    _transaction.Dispose();
            }
        }

        public void RollBack()
        {
            lock (_lock)
            {
                if (_scopes == 2)
                    _transaction.Rollback();
            }
        }

        public bool IsAlive => _transaction is not null;

        internal void Begin()
        {
            lock (_lock) { _scopes++; }
        }
    }
}