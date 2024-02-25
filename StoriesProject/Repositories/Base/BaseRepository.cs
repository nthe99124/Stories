using Microsoft.EntityFrameworkCore;
using StoriesProject.Common.Repository;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace StoriesProject.Repositories.Base
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate);
        Task<T> Create(T entity);
        Task<int> Delete(T entity);
        Task<int> BulkDelete(Expression<Func<T, bool>> predicate);
        Task<int> Save();
        IEnumerable<N> ExecuteStoredProcedureObject<N>(string nameProcedure, SqlParameter[]? array) where N : class, new();

        /// <summary>
        /// Insert 1 list object
        /// </summary>
        /// <param name="listEntity">List object</param>
        /// <returns>Trả về 1: Thành công - 0: Thất bại</returns>
        Task BulkInsert(IEnumerable<T> listEntity);

        IEnumerable<N> SqlQuery<N>(string query, SqlParameter[]? array = null) where N : class, new();
        DataTable SqlQuery(string query, SqlParameter[]? array = null);
        Task<int> SqlCommand(string query, SqlParameter[]? array = null);
    }
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbSet<T> _dbset;
        private readonly IUnitOfWork _entities;


        protected BaseRepository(IUnitOfWork entities)
        {
            _entities = entities;
            _dbset = _entities.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {

            return await Task.Run(() => _dbset.AsEnumerable<T>());
        }

        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => _dbset.Where(predicate).AsEnumerable());
        }

        public async Task<T> Create(T entity)
        {
            _dbset.Add(entity);
            await Save();
            return entity;
        }
        public async Task<int> Delete(T entity)
        {
            _dbset.Remove(entity);
            return await Save();
        }

        public async Task<int> BulkDelete(Expression<Func<T, bool>> predicate)
        {
            _dbset.RemoveRange((IEnumerable<T>)_dbset.Where(predicate).AsEnumerable());
            return await Save();
        }

        public async Task<int> Save()
        {
            try
            {
                return await _entities.CommitAsync();
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public IEnumerable<N> ExecuteStoredProcedureObject<N>(string nameProcedure, SqlParameter[]? array) where N : class, new()
        {
            return _entities.ExecuteStoredProcedureObject<N>(nameProcedure, array);
        }


        /// <summary>
        /// Insert 1 list object
        /// </summary>
        /// <param name="listEntity">List object</param>
        /// <returns>Trả về 1: Thành công - 0: Thất bại</returns>
        public async Task BulkInsert(IEnumerable<T> listEntity)
        {
            await _entities.BulkInsert(listEntity);
        }


        /// <summary>
        /// excute ra dạng Datatabl, data sheet, string, object
        /// </summary>
        /// <typeparam name="N"></typeparam>
        /// <param name="query"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public IEnumerable<N> SqlQuery<N>(string query, SqlParameter[]? array = null) where N : class, new()
        {
            try
            {
                //array là mảng tham số truyền vào theo kiểu dữ liệu SqlParameter
                return _entities.SqlQuery<N>(query, array);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable SqlQuery(string query, SqlParameter[]? array = null)
        {
            try
            {
                //array là mảng tham số truyền vào theo kiểu dữ liệu SqlParameter
                return _entities.SqlQuery(query, array);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// excute các câu lệnh dạng delete, update
        /// </summary>
        /// <param name="query"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public async Task<int> SqlCommand(string query, SqlParameter[]? array = null)
        {
            try
            {
                return await _entities.SqlCommand(query, array);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
