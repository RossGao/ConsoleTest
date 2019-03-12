using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DbContextTest
{
    public class TestRepository : ITestRepository
    {
        private ThreadLocal<TestDbContext> threadInstance = new ThreadLocal<TestDbContext>(() => null);

        public TestRepository(TestDbContext theContext)
        {
            threadInstance.Value = theContext;
        }

        public TestDbContext DbContext { get { return threadInstance.Value; } }

        public bool AddTest(Test added)
        {
            if (added != null)
            {
                threadInstance.Value.Add(added);
                threadInstance.Value.SaveChanges();
                return true;
            }

            return false;
        }

        public bool UpdateTest(Test updated)
        {
            if (updated != null)
            {
                threadInstance.Value.Entry(updated).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                threadInstance.Value.SaveChanges();
                return true;
            }

            return false;
        }

        public bool ChangeName(Test changed)
        {
            if (changed != null)
            {
                var testItem = new Test() { Id = changed.Id };
                threadInstance.Value.Attach(testItem);
                testItem.Name = changed.Name;
                threadInstance.Value.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
