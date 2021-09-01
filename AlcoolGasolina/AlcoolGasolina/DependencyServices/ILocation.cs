using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App5.DependencyServices
{
    public interface ILocation
    {
        void OpenSettings();
        bool IsEnabled();
    }
}
