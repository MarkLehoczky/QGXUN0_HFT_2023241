using QGXUN0_HFT_2023241.Repository.Database;
using System.Linq;

namespace QGXUN0_HFT_2023241.Repository.Template
{
    /// <summary>
    /// Repository for the <typeparamref name="T"/> model <see langword="class"/>
    /// </summary>
    /// <typeparam name="T">model <see langword="class"/></typeparam>
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Database context
        /// </summary>
        protected BookDbContext context;


        /// <summary>
        /// Constructor that provides the database context
        /// </summary>
        /// <param name="context">database context</param>
        public Repository(BookDbContext context)
        {
            this.context = context;
        }


        /// <inheritdoc/>
        public void Create(T element)
        {
            context.Set<T>().Add(element);
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public void Delete(int id)
        {
            context.Set<T>().Remove(Read(id));
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public IQueryable<T> ReadAll()
        {
            return context.Set<T>();
        }

        /// <inheritdoc/>
        public abstract T Read(int id);

        /// <inheritdoc/>
        public abstract void Update(T element);

        /// <inheritdoc/>
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
