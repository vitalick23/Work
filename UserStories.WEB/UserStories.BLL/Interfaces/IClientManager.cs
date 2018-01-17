﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.Entities;

namespace UserStories.BLL.Interfase
{
    public interface IClientManager: IDisposable
    {
        void Create(ClientProfile item);
        //void Create(ClientProfile clientProfile);
    }
}
