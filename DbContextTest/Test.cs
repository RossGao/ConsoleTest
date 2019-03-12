using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DbContextTest
{
    public class Test : ITest
    {
        public string Name { get; set; }
    }
}
