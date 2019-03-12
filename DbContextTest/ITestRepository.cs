using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbContextTest
{
    public interface ITestRepository
    {
        TestDbContext DbContext { get; }

        bool AddTest(Test added);

        bool UpdateTest(Test updated);

        bool ChangeName(Test changed);
    }
}
