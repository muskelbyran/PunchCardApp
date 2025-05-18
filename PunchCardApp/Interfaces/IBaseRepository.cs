using PunchCardApp.Models;
using System.Linq.Expressions;

namespace PunchCardApp.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<ResponseResult> AlreadyExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<ResponseResult> CreateOneAsync(TEntity entity);
        Task<ResponseResult> DeleteOneAsync(Expression<Func<TEntity, bool>> predicate);
        Task<ResponseResult> GetAllAsync();
        Task<ResponseResult> GetOneAsync(Expression<Func<TEntity, bool>> predicate);
        Task<ResponseResult> UpdateOneAsync(Expression<Func<TEntity, bool>> predicate, TEntity updatedEntity);
    }
}