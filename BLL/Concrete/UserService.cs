using System.Collections.Generic;
using BLL.Abstract;
using DAL.Abstract.UnitOfWork;
using Models;

namespace BLL.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetUsers()
        {
            return _unitOfWork.UserRepository.Get();
        }

        public User GetUser(string email)
        {
            return _unitOfWork.UserRepository.Find(email);
        }

        public User GetUserOrDefault(string email)
        {
            return _unitOfWork.UserRepository.SingleOrDefault(u => u.Email == email);
        }

        public void RegisterUser(User user)
        {
            _unitOfWork.UserRepository.Create(user);
            _unitOfWork.SaveChanges();
        }
    }
}
