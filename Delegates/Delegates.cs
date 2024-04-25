using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NameParser.Generators.PersonErrorGenerator;

namespace NameParser.Delegates
{
    public delegate void RandomAction(GlobalIndexData globalIndexData, int seed);
}
