using System;
using NHibernate;

namespace Evil.Infrastructure.Nhibernate
{
    public interface ITransactionBoundary : IDisposable
    {
        ISession CurrentSession { get; }
        void Begin();
        void Commit();
        void RollBack();
    }
}