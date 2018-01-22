using System;
using UserStore.BLL.Entities;

namespace UserStore.BLL.Interfaces
{
    public interface IClientManager:IDisposable
    {
        void Create(ClientProfile item);
    }
}
