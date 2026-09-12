using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Models.ForDapper;

namespace Lection11_Tests.Interfaces.ForDapper
{
    public interface IAddressesRepository
    {
        Task<IEnumerable<(UserDTO user, AddressDTO address, OrderDTO order)>> GetAddressOrderCityMismatchesAsync();
        Task<IEnumerable<AddressDTO>> GetAllAddressesAsync();
        ///
        Task<IEnumerable<AddressDTO>> GetAllAsync();
        Task<IEnumerable<AddressDTO>> GetByUserAsync(int userId);
    }
}
