using Microsoft.EntityFrameworkCore;
using Purchase.Domain.Contracts;
using Purchase.Domain.Models;
using Purchase.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Infrastructure.Repository
{
    public class RepositoryBase<T, TId> : IRepositoryBase<T, TId> where T : BaseEntity<TId> where TId : IEquatable<TId>
    {
        protected readonly RepositoryContext _context;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;

        }
        public virtual ValidationResultInfo Validate(T itemToValidate)
        {
            return itemToValidate.BasicValidation();
        }

        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? _context.Set<T>().Where(s => s.Status != EntityStatus.Deleted).AsNoTracking() : _context.Set<T>().Where(s => s.Status != EntityStatus.Deleted);

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? _context.Set<T>().Where(s => s.Status != EntityStatus.Deleted).Where(expression).AsNoTracking() : _context.Set<T>().Where(s => s.Status != EntityStatus.Deleted).Where(expression);



        public virtual void Create(T entity)
        {
            ValidationResultInfo vri = Validate(entity);
            if (!vri.IsValid)
                throw new DomainValidationException(vri, string.Format("{0} not valid", entity.GetType().FullName));
            var date = DateTime.Now;
            entity.CreatedOn = date;
            entity.UpdatedOn = date;
            entity.Status = EntityStatus.Active;
            _context.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            ValidationResultInfo vri = Validate(entity);
            if (!vri.IsValid)
                throw new DomainValidationException(vri, string.Format("{0} not valid", entity.GetType().FullName));
            var date = DateTime.Now;
            entity.UpdatedOn = date;

            _context.Set<T>().Update(entity);
            _context.Entry(entity).Property(x => x.CreatedOn).IsModified = false;
            _context.Entry(entity).Property(x => x.Status).IsModified = false;
        }


        public virtual void Delete(T entity) => _context.Set<T>().Remove(entity);

        public T GetById(TId id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(TId id, bool trackChanges = false)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (!trackChanges && entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;

        }

        public Task<bool> ExistAsync(TId id)
        {
            return _context.Set<T>().AsNoTracking().AnyAsync(s => s.Id.Equals(id));
        }
    }

}
