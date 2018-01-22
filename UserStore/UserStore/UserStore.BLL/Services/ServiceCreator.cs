using UserStore.BLL.Interfaces;


namespace UserStore.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        //public IUserService CreateUserService(string connection)
        //{
        //    return new UserService(new IdentityUnitOfWork(connection));
        //}
        public IUserService CreateUserService(string connection)
        {
            throw new System.NotImplementedException();
        }
    }
}
