using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Domain.Controllers
{
    internal interface IController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The next controller to run, or null if finished.</returns>
        IController? Execute();
    }
}
