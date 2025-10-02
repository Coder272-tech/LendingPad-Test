using System;
using System.Collections.Generic;
using System.Text;

namespace Data.UnitOfWork
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public void Commit()
        {
            // no-op or custom logic for persisting in-memory state
        }
    }

}
