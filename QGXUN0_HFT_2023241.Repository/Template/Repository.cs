using QGXUN0_HFT_2023241.Repository.Database;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace QGXUN0_HFT_2023241.Repository.Template
{
    /// <summary>
    /// Specifies a <typeparamref name="T"/> repository template, which provides <see langword="CRUD"/> actions to the <see cref="BookDbContext"/>.
    /// </summary>
    /// <typeparam name="T">model <see langword="class"/></typeparam>
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Provides the context for the database
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


        /// <summary>
        /// Creates a new <typeparamref name="T"/> instance in the database by the <paramref name="element"/>.
        /// </summary>
        /// <param name="element">New <typeparamref name="T"/> instance</param>
        public virtual void Create(T element)
        {
            context.Set<T>().Add(element);
            context.SaveChanges();
        }

        /// <summary>
        /// Reads a <typeparamref name="T"/> instance from the database by the <paramref name="id"/>.
        /// </summary>
        /// <param name="id">ID of the <typeparamref name="T"/> instance</param>
        /// <returns><typeparamref name="T"/> instance, which has the <paramref name="id"/></returns>
        /// <exception cref="ArgumentNullException">the <typeparamref name="T"/> <see langword="class"/> does not have at least one property which implements the <see cref="KeyAttribute"/></exception>
        /// <exception cref="InvalidOperationException">the <typeparamref name="T"/> instance is not found</exception>
        public virtual T Read(int id)
        {
            return context.Set<T>().First(t => (int)typeof(T).GetProperties().Where(t => t.GetCustomAttribute<KeyAttribute>() != null).First().GetValue(t) == id);
        }

        /// <summary>
        /// Updates the <typeparamref name="T"/> instance in the database by the <paramref name="element"/>.
        /// </summary>
        /// <param name="element">Updated <typeparamref name="T"/> instance</param>
        /// <exception cref="ArgumentNullException">the <typeparamref name="T"/> <see langword="class"/> does not have at least one property which implements the <see cref="KeyAttribute"/></exception>
        /// <exception cref="InvalidOperationException">the <typeparamref name="T"/> instance is not found</exception>
        public virtual void Update(T element)
        {
            var old = Read((int)typeof(T).GetProperties().Where(t => t.GetCustomAttribute<KeyAttribute>() != null).FirstOrDefault().GetValue(element));

            foreach (var prop in old.GetType().GetProperties())
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                    prop.SetValue(old, prop.GetValue(element));

            context.SaveChanges();
        }

        /// <summary>
        /// Deletes a <typeparamref name="T"/> instance from the database by the <paramref name="id"/>.
        /// </summary>
        /// <param name="id">ID of the <typeparamref name="T"/> instance</param>
        /// <exception cref="ArgumentNullException">the <typeparamref name="T"/> <see langword="class"/> does not have at least one property which implements the <see cref="KeyAttribute"/></exception>
        /// <exception cref="InvalidOperationException">the <typeparamref name="T"/> instance is not found</exception>
        public virtual void Delete(int id)
        {
            context.Set<T>().Remove(Read(id));
            context.SaveChanges();
        }

        /// <summary>
        /// Reads all <typeparamref name="T"/> instances from the database
        /// </summary>
        /// <returns>Every <typeparamref name="T"/> instances</returns>
        public virtual IQueryable<T> ReadAll()
        {
            return context.Set<T>();
        }
    }
}
