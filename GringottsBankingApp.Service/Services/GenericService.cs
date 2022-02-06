using GringottsBankingApp.Core.Repositories;
using GringottsBankingApp.Core.Services;
using GringottsBankingApp.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GringottsBankingApp.Service.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _repository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);

            await _unitOfWork.CommitAsync();

            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public void Remove(TEntity entity)
        {
            _repository.Remove(entity);

            _unitOfWork.Commit();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.SingleOrDefaultAsync(predicate);
        }

        public TEntity Update(TEntity entity)
        {
            var updatedEntity = _repository.Update(entity);

            _unitOfWork.Commit();

            return updatedEntity;
        }

        public async Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.Where(predicate);
        }
    }
}
