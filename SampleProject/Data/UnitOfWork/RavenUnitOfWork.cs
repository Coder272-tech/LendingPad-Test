using Raven.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.UnitOfWork
{
    public class RavenUnitOfWork : IUnitOfWork
    {
        private readonly IDocumentSession _session;

        public RavenUnitOfWork(IDocumentSession session)
        {
            _session = session;
        }

        public void Commit()
        {
            _session.SaveChanges();
        }
    }

}
