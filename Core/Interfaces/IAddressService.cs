using Core.Dtos.Account.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces;

public interface IAddressService
{
    Task AddAddress(Guid userId, AddAddressDto dto);
    Task<List<AddressDto>> ListAddresses(Guid userId);
    Task DeleteAddress(Guid userId, int id);
}
