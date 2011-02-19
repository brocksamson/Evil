using Evil.Infrastructure.Nhibernate;
using MbUnit.Framework;
using NHibernate;
using StructureMap;

namespace Evil.Infrastructure.Tests.DataAccess
{
    public class DbTest
    {
        private ITransactionBoundary _transactionBoundary;
        protected ISession _session;

        [SetUp]
        public void SetUp()
        {
            _transactionBoundary = ObjectFactory.GetInstance<ITransactionBoundary>();
            _transactionBoundary.Begin();
            _session = _transactionBoundary.CurrentSession;
            Arrange();
        }

        [TearDown]
        public void Dispose()
        {
            _transactionBoundary.RollBack();
        }

        protected virtual void Arrange()
        {
            
        }

        protected void FlushSession()
        {
            _session.Flush();
        }
    }
}