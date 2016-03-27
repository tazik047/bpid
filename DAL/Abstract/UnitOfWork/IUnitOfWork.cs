using System;
using DAL.Abstract.Repositories;

namespace DAL.Abstract.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        IMessageRepository MessageRepository { get; }

        int SaveChanges();
    }
}
