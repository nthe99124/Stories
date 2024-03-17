using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoriesProject.API.Repositories;
using System.Data;

namespace StoriesProject.API.Common.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        public IAccountantsRepository AccountantsRepository { get; }
        public IAuthorRegisterRepository AuthorRegisterRepository { get; }
        public ILogEntryRepository LogEntryRepository { get; }
        public IRoleAccountantRepository RoleAccountantRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IStoriesRepository StoriesRepository { get; }
        public ITopicRepository TopicRepository { get; }
        public ITopicStoryRepository TopicStoryRepository { get; }
        public IChapterRepository ChapterRepository { get; }
        public IChapterContentRepository ChapterContentRepository { get; }
        DbSet<T> Set<T>() where T : class;
        int Commit();
        Task<int> CommitAsync();
        IEnumerable<T> SqlQuery<T>(string query, SqlParameter[]? array = null) where T : class, new();
        DataTable SqlQuery(string query, SqlParameter[]? array = null);
        Task<int> SqlCommand(string query, SqlParameter[]? array = null);
    }

    public class UnitOfWork : IUnitOfWork
    {
        readonly StoriesContext _context;
        private bool _isDisposed;

        
        public UnitOfWork(StoriesContext context)
        {
            _context = context;
        }

        #region Khai repository (không inject qua ctor nữa mà xử lý new theo từng lần sử dụng tránh cấp phát quá nhiều trong 1 lần khởi tạo UnitOfWork)

        private IAccountantsRepository _accountantsRepository;
        public IAccountantsRepository AccountantsRepository
        {
            get
            {
                if (_accountantsRepository == null)
                {
                    _accountantsRepository = new AccountantsRepository(_context);
                }
                return _accountantsRepository;
            }
        }

        private IAuthorRegisterRepository _authorRegisterRepository;
        public IAuthorRegisterRepository AuthorRegisterRepository
        {
            get
            {
                if (_authorRegisterRepository == null)
                {
                    _authorRegisterRepository = new AuthorRegisterRepository(_context);
                }
                return _authorRegisterRepository;
            }
        }

        private ILogEntryRepository _logEntryRepository;
        public ILogEntryRepository LogEntryRepository
        {
            get
            {
                if (_logEntryRepository == null)
                {
                    _logEntryRepository = new LogEntryRepository(_context);
                }
                return _logEntryRepository;
            }
        }

        private IRoleAccountantRepository _roleAccountantRepository;
        public IRoleAccountantRepository RoleAccountantRepository
        {
            get
            {
                if (_roleAccountantRepository == null)
                {
                    _roleAccountantRepository = new RoleAccountantRepository(_context);
                }
                return _roleAccountantRepository;
            }
        }

        private IRoleRepository _roleRepository;
        public IRoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new RoleRepository(_context);
                }
                return _roleRepository;
            }
        }

        private IStoriesRepository _storiesRepository;
        public IStoriesRepository StoriesRepository
        {
            get
            {
                if (_storiesRepository == null)
                {
                    _storiesRepository = new StoriesRepository(_context);
                }
                return _storiesRepository;
            }
        }

        private ITopicRepository _topicRepository;
        public ITopicRepository TopicRepository
        {
            get
            {
                if (_topicRepository == null)
                {
                    _topicRepository = new TopicRepository(_context);
                }
                return _topicRepository;
            }
        }

        private ITopicStoryRepository _topicStoryRepository;
        public ITopicStoryRepository TopicStoryRepository
        {
            get
            {
                if (_topicStoryRepository == null)
                {
                    _topicStoryRepository = new TopicStoryRepository(_context);
                }
                return _topicStoryRepository;
            }
        }

        private IChapterRepository _chapterRepository;
        public IChapterRepository ChapterRepository
        {
            get
            {
                if (_chapterRepository == null)
                {
                    _chapterRepository = new ChapterRepository(_context);
                }
                return _chapterRepository;
            }
        }

        private IChapterContentRepository _chapterContentRepository;
        public IChapterContentRepository ChapterContentRepository
        {
            get
            {
                if (_chapterContentRepository == null)
                {
                    _chapterContentRepository = new ChapterContentRepository(_context);
                }
                return _chapterContentRepository;
            }
        }

        #endregion

        public DbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;
            _context.Dispose();
        }

        /// <summary>
        /// IEnumerable type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public IEnumerable<T> SqlQuery<T>(string query, SqlParameter[]? array = null) where T : class, new()
        {
            try
            {
                //array là mảng tham số truyền vào theo kiểu dữ liệu SqlParameter
                _context.Database.SetCommandTimeout(1800);
                IEnumerable<T> obj;
                if (array != null)
                {
                    obj = Set<T>().FromSqlRaw(query, array).ToList();
                }
                else
                {
                    obj = Set<T>().FromSqlRaw(query).ToList();
                }

                return obj;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public DataTable SqlQuery(string query, SqlParameter[]? array = null)
        {
            var dt = new DataTable();
            try
            {
                //array là mảng tham số truyền vào theo kiểu dữ liệu SqlParameter
                _context.Database.SetCommandTimeout(1800);

                var conn = _context.Database.GetDbConnection();
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open) conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        if (array != null && array.Any())
                        {
                            cmd.Parameters.AddRange(array);
                        }
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // error handling
                    throw;
                }
                finally
                {
                    if (connectionState != ConnectionState.Closed) conn.Close();
                }

                return dt;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> SqlCommand(string query, SqlParameter[]? array = null)
        {
            try
            {
                //array là mảng tham số truyền vào theo kiểu dữ liệu SqlParameter
                _context.Database.SetCommandTimeout(1800);
                int rs;
                if (array != null)
                {
                    rs = await _context.Database.ExecuteSqlRawAsync(query, array);
                }
                else
                {
                    rs = await _context.Database.ExecuteSqlRawAsync(query);
                }

                return rs;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}