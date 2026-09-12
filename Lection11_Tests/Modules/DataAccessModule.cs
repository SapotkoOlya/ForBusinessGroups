using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Interfaces.ForDapper;
using Microsoft.Extensions.DependencyInjection;
using Lection11_Tests.Repositories;

namespace Lection11_Tests.Modules
{
    public static class DataAccessModule
    {
        public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        string connection)
        {
            //когда попросят, создай такой-то объект
            services.AddScoped<IUsersRepository>(p => new UsersRepository(connection));
            services.AddScoped<IAddressesRepository>(p => new AddressesRepository(connection));
            services.AddScoped<ICategoryRepository>(p => new CategoryRepository(connection));
            services.AddScoped<IProductsRepository>(p => new ProductsRepository(connection));
            services.AddScoped<IOrdersRepository>(p => new OrdersRepository(connection));
            services.AddScoped<IReviewsRepository>(p => new ReviewsRepository(connection));
            return services;
        }
    }
}
