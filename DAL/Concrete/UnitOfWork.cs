using System.Data.Entity.Validation;
using System.Linq;
using DAL.Abstract.Repositories;
using DAL.Abstract.UnitOfWork;
using DAL.Concrete.EntityFramework;
using DAL.Concrete.Repositories;

namespace DAL.Concrete
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DbContext _context;
        private IUserRepository _userRepository;
        private IMessageRepository _messageRepository;

        public UnitOfWork()
        {
            _context = new DbContext();
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
        }

        public IMessageRepository MessageRepository
        {
            get { return _messageRepository ?? (_messageRepository = new MessageRepository(_context)); }
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
