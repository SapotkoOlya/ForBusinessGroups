using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Tests;
using Microsoft.Extensions.DependencyInjection;
using Lection11_Tests.Modules;

namespace Lection11_Tests.Fixtures
{
    public class TestFixture
    {
        public ServiceProvider Provider { get; }

        public TestFixture()
        {
            var services = new ServiceCollection();

            var dbPath = Path.Combine(AppContext.BaseDirectory, "marketplace.db");
            var conn = $"Data Source={dbPath}";
            services.AddDataAccess(conn);
            Provider = services.BuildServiceProvider();
        }
    }
}
