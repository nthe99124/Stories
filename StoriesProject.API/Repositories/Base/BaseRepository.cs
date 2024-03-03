using Microsoft.EntityFrameworkCore;
using StoriesProject.API.Common.Repository;
using StoriesProject.Model.DTO;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace StoriesProject.API.Repositories.Base
{
    public interface IBaseRepository<T>
    {
        Task<PagingResultDTO<T>?> GetPaging(int pageSize = 0, int pageIndex = 0, Expression<Func<T, bool>>? predicateFilter = null, List<SortedPaging>? sortList = null);
        Task<IEnumerable<T>?> GetDataLimit(int limitValue = 0, List<SortedPaging>? sortList = null, Expression<Func<T, bool>>? predicateFilter = null);
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
        protected readonly IUnitOfWork _entities;

        protected BaseRepository(IUnitOfWork entities)
        {
            _entities = entities;
            _dbset = _entities.Set<T>();
        }

        #region Paging
        /// <summary>
        /// Hàm xử lý lấy dữ liệu paging - chỉ lấy trong model (lưu ý chỉ làm với dữ liệu nhỏ)
        /// CreaetedBy ntthe 29.02.2024
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="predicateFilter"></param>
        /// <param name="sortList"></param>
        /// <returns></returns>
        public async Task<PagingResultDTO<T>?> GetPaging(int pageSize = 0, int pageIndex = 0, Expression<Func<T, bool>>? predicateFilter = null, List<SortedPaging>? sortList = null)
        {
            // phân 2 task => , 1 task lấy data

            // task đếm tổng bản ghi
            var taskCalTotalRecord = Task.Run(async() =>
            {
                 return await _dbset.CountAsync<T>();
            });

            // 1 task lấy data
            var taskGetDataRecord = Task.Run(async () =>
            {
                var data = await GetDataByConditionAndSorted(predicateFilter, sortList);

                // phân trang
                if (data != null && data.Count() > 0)
                {
                    if (pageIndex > 0 && pageSize > 0)
                    {
                        // tính số bản ghi bỏ qua
                        var numerRecoredSkip = pageIndex * pageSize;
                        // nếu có cả bộ 2 tham số thì thực hiện phân trang
                        data = data.Skip(numerRecoredSkip).Take(pageSize).ToList();
                    }
                    else if (pageSize > 0)
                    {
                        // nếu chỉ có pageSize thì thực hiện lấy top
                        data = data.Take(pageSize).ToList();
                    }
                }
                return data;
            });

            await Task.WhenAll(taskCalTotalRecord, taskGetDataRecord);

            var pagingResult = new PagingResultDTO<T>
            {
                Data = taskGetDataRecord?.Result,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = taskCalTotalRecord.Result
            };

            return pagingResult;
        }

        /// <summary>
        /// Hàm lấy dữ liệu bản theo số ghi cao nhất thấp nhất theo điều kiện lọc theo sort
        /// CreatedBy ntthe 29.02.2024
        /// </summary>
        /// <param name="limitValue"></param>
        /// <param name="predicateFilter"></param>
        /// <param name="sortList"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>?> GetDataLimit(int limitValue = 0, List<SortedPaging>? sortList = null, Expression<Func<T, bool>>? predicateFilter = null)
        {
            if (limitValue > 0)
            {
                var data = await GetDataByConditionAndSorted(predicateFilter, sortList);
                return await Task.Run(() => data?.Take(limitValue).AsEnumerable<T>());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Hàm lấy dữ liệu theo điều kiện lọc và sort
        /// CreatedBy ntthe 29.02.2024
        /// </summary>
        /// <param name="predicateFilter"></param>
        /// <param name="sortList"></param>
        /// <returns></returns>
        private async Task<IEnumerable<T>?> GetDataByConditionAndSorted(Expression<Func<T, bool>>? predicateFilter = null, List<SortedPaging>? sortList = null)
        {
            IEnumerable<T>? data = null;
            // lọc theo filter custom
            if (predicateFilter != null)
            {
                data = _dbset.Where(predicateFilter);
            }
            else
            {
                data = await GetAll();
            }

            // sort lại custom
            if (data != null && data.Count() > 0 && sortList != null && sortList.Count > 0)
            {
                IOrderedEnumerable<T>? orderedData = null;
                foreach (var item in sortList)
                {
                    // lần đầu thì order thường, sau đó thì then
                    if (orderedData == null)
                    {
                        if (item.IsAsc)
                        {
                            orderedData = data.OrderBy(s => s?.GetType()?.GetProperty(item.Field)?.GetValue(s, null));
                        }
                        else 
                        {
                            orderedData = data.OrderByDescending(s => s?.GetType()?.GetProperty(item.Field)?.GetValue(s, null));
                        }
                    }
                    else
                    {
                        if (item.IsAsc)
                        {
                            orderedData = orderedData.ThenBy(s => s?.GetType()?.GetProperty(item.Field)?.GetValue(s, null));
                        }
                        else
                        {
                            orderedData = orderedData.ThenByDescending(s => s?.GetType()?.GetProperty(item.Field)?.GetValue(s, null));
                        }
                    }
                };

                // thực hiện dùng orderedData thì nó sẽ giữ được dạng OrderBy().ThenBy().ThenBy()
                data = orderedData?.ToList() ?? data.ToList();
            }

            return await Task.Run(() => data);
        }

        #endregion

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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
