﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Services
{
    internal interface IMessageService
    {
        void Send(string toAddress, string contents);
    }
}