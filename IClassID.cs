using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace o_2_C
{
    internal interface IClassGenerics<T>
    {
        int ID { get; }
        string ToCSVString();

        static T UserInput()
        {
            throw new NotImplementedException();
        }
    }
}
