using System;
using System.Collections.Generic;
using System.Text;

namespace Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
