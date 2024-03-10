using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023241.Repository.Database;
using QGXUN0_HFT_2023241.Repository.Template;
using System.Linq;

namespace QGXUN0_HFT_2023241.Repository.ModelRepository
{
    /// <inheritdoc/>
    public class BookRepository : Repository<Book>, IRepository<Book>
    {
        /// <inheritdoc/>
        public BookRepository(BookDbContext context) : base(context) { }


        /// <inheritdoc/>
        public override Book Read(int id)
        {
            return context.Books.First(t => t.BookID == id);
        }

        /// <inheritdoc/>
        public override void Update(Book element)
        {
            var old = Read(element.BookID);
            old.Title = element.Title;
            old.Year = element.Year;
            old.PublisherID = element.PublisherID;
            old.Price = element.Price;
            old.Rating = element.Rating;

            context.SaveChanges();
        }
    }
}
