﻿using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Requests.Storage;

namespace Wholesaler.Backend.Domain.Interfaces
{
    public interface IStorageService
    {
        Storage Add (CreateStorageRequest request);
        Storage Deliver (Guid storageId, int quantity);
        Storage Depart(Guid storageId, Requirement requirement);
        int GetSumOfStates();
        int GetCosts(int sumOfStates);
    }
}
