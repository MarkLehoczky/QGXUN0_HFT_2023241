using System.Linq;

namespace QGXUN0_HFT_2023241.Repository.Template
{
    /// <summary>
    /// Repository for the <typeparamref name="T"/> model <see langword="class"/>
    /// </summary>
    /// <typeparam name="T">model <see langword="class"/></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Creates a new <paramref name="element"/> into the database context
        /// </summary>
        /// <param name="element">new element</param>
        void Create(T element);

        /// <summary>
        /// Reads an <see langword="element"/> by the <paramref name="id"/> from the database context
        /// </summary>
        /// <param name="id">id of the readable <see langword="element"/></param>
        /// <returns>element with the <paramref name="id"/></returns>
        T Read(int id);

        /// <summary>
        /// Updates the <paramref name="element"/> with the same <see langword="id"/> in the database context
        /// </summary>
        /// <param name="element">new element with the same <see langword="id"/> as the old element</param>
        void Update(T element);

        /// <summary>
        /// Deletes an <see langword="element"/> by the <paramref name="id"/> from the database context
        /// </summary>
        /// <param name="id">id of the deletable <see langword="element"/></param>
        void Delete(int id);

        /// <summary>
        /// Reads all <see langword="element"/> from the database context
        /// </summary>
        /// <returns>all <see langword="element"/></returns>
        IQueryable<T> ReadAll();

        /// <summary>
        /// Saves the changes of the database
        /// </summary>
        void SaveChanges();
    }
}
