using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatfsToSpiffsConverter.Communication
{
    public interface IProto
    {
       bool Send(byte[] data);
    }
}
