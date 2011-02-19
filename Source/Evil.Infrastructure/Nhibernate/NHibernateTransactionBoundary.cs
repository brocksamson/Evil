using System;
using FluentNHibernate;
using NHibernate;

namespace Evil.Infrastructure.Nhibernate
{
    public class NHibernateTransactionBoundary : ITransactionBoundary
    {
        private readonly ISessionSource _sessionSource;
        private bool _begun;
        private bool _disposed;
        private bool _rolledBack;
        private ITransaction _transaction;

        public NHibernateTransactionBoundary(ISessionSource sessionSource)
        {
            _sessionSource = sessionSource;
        }

        #region ITransactionBoundary Members

        public void Begin()
        {
            CheckIsDisposed();

            CurrentSession = _sessionSource.CreateSession();

            BeginNewTransaction();
            _begun = true;
        }

        public void Commit()
        {
            CheckIsDisposed();
            CheckHasBegun();

            if (_transaction.IsActive && !_rolledBack)
            {
                _transaction.Commit();
            }

            BeginNewTransaction();
        }

        public void RollBack()
        {
            CheckIsDisposed();
            CheckHasBegun();

            if (_transaction.IsActive)
            {
                _transaction.Rollback();
                _rolledBack = true;
            }

            BeginNewTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ISession CurrentSession { get; private set; }

        #endregion

        private void BeginNewTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }

            _transaction = CurrentSession.BeginTransaction();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_begun || _disposed)
                return;

            if (disposing)
            {
                _transaction.Dispose();
                CurrentSession.Dispose();
            }

            _disposed = true;
        }

        private void CheckHasBegun()
        {
            if (!_begun)
                throw new InvalidOperationException("Must call Begin() on the unit of work before committing");
        }

        private void CheckIsDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }
    }
}