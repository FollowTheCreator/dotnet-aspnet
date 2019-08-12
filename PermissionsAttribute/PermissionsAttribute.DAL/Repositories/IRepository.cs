﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsAttribute.DAL.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task CreateAsync(T profile);

        Task UpdateAsync(T profile);

        Task DeleteAsync(int id);
    }
}
