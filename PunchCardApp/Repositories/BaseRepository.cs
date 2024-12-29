using Microsoft.EntityFrameworkCore;
using PunchCardApp.Data;
using PunchCardApp.Factories;
using PunchCardApp.Models;
using System.Linq.Expressions;

namespace PunchCardApp.Repositories;

public abstract class BaseRepository<TEntity>(ApplicationDbContext context) where TEntity : class
{
    private readonly ApplicationDbContext _context = context;

    // Create
    public virtual async Task<ResponseResult> CreateOneAsync(TEntity entity) // Single Responsibility Principle
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();

            return ResponseFactory.Ok(entity);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    // Read
    public virtual async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<TEntity> result = await _context.Set<TEntity>().ToListAsync();
            return ResponseFactory.Ok(result);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    // x = email, streetname, etc
    public virtual async Task<ResponseResult> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (result == null)
                return ResponseFactory.NotFound();

            return ResponseFactory.Ok(result);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    // Update
    public virtual async Task<ResponseResult> UpdateOneAsync(Expression<Func<TEntity, bool>> predicate, TEntity updatedEntity)
    {
        try
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (result != null)
            {
                _context.Entry(result).CurrentValues.SetValues(updatedEntity);
                await _context.SaveChangesAsync();
                return ResponseFactory.Ok(result);
            }

            return ResponseFactory.NotFound();
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    // Delete
    public virtual async Task<ResponseResult> DeleteOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);

            if (result != null)
            {
                _context.Set<TEntity>().Remove(result);
                await _context.SaveChangesAsync();
                return ResponseFactory.Ok("Successfully Removed!");
            }
            return ResponseFactory.NotFound();
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    // Exists
    public virtual async Task<ResponseResult> AlreadyExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<TEntity>().AnyAsync(predicate);

            if (result)
            {
                return ResponseFactory.Exists();
            }
            return ResponseFactory.NotFound();
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }
}