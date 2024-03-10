using System.Linq;

namespace QGXUN0_HFT_2023241.Repository.Template
{
    /// <summary>
    /// Specifies a <typeparamref name="T"/> repository template, which provides <see langword="CRUD"/> actions.
    /// </summary>
    /// <typeparam name="T">model <see langword="class"/></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Creates a new <typeparamref name="T"/> instance by the <paramref name="element"/>.
        /// </summary>
        /// <param name="element">New <typeparamref name="T"/> instance</param>
        void Create(T element);

        /// <summary>
        /// Reads a <typeparamref name="T"/> instance by the <paramref name="id"/>.
        /// </summary>
        /// <param name="id">ID of the <typeparamref name="T"/> instance</param>
        /// <returns><typeparamref name="T"/> instance, which has the <paramref name="id"/></returns>
        T Read(int id);

        /// <summary>
        /// Updates the <typeparamref name="T"/> instance by the <paramref name="element"/>.
        /// </summary>
        /// <param name="element">Updated <typeparamref name="T"/> instance</param>
        void Update(T element);

        /// <summary>
        /// Deletes a <typeparamref name="T"/> instance by the <paramref name="id"/>.
        /// </summary>
        /// <param name="id">ID of the <typeparamref name="T"/> instance</param>
        void Delete(int id);

        /// <summary>
        /// Reads all <typeparamref name="T"/> instances
        /// </summary>
        /// <returns>Every <typeparamref name="T"/> instances</returns>
        IQueryable<T> ReadAll();
    }
}
