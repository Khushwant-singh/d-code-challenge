using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyramid.Net.Models
{
    internal class PyramidItem
    {
        public int NumberValue { get; set; }
        public bool isEven => NumberValue % 2 == 0;
        public List<PyramidItem> Children { get; set; }
    }
}
