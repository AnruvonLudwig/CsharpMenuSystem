using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpMenuSystem.interfaces
{
    internal interface IFestivalEntity
    {
        int ID { get; }
        string Name { get; }
        bool IsValid();
    }
}
