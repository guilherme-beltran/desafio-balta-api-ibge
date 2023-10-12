﻿using Microsoft.EntityFrameworkCore;

using Desafio_Balta_IBGE.Shared.Entities;
using Desafio_Balta_IBGE.Infra.Data.Context;
using Desafio_Balta_IBGE.Domain.Interfaces.BaseRepository;

namespace Desafio_Balta_IBGE.Infra.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity, new()
{
    private readonly IbgeContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public BaseRepository(IbgeContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAsync() => await _dbSet.ToListAsync();

    public async ValueTask<TEntity?> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);

    public async Task AddAsync(TEntity entity)
        => await _dbSet.AddAsync(entity);

    public void Update(TEntity entity)
        => _dbSet.Update(entity);

    public void DeleteAsync(TEntity entity)
        => _dbSet.Remove(entity);
}
