using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023241.Repository.Database;
using QGXUN0_HFT_2023241.Repository.Template;
using System.Linq;

namespace QGXUN0_HFT_2023241.Repository.ModelRepository
{
    /// <inheritdoc/>
    public class BookAuthorConnectorRepository : Repository<BookAuthorConnector>, IRepository<BookAuthorConnector>
    {
        /// <inheritdoc/>
        public BookAuthorConnectorRepository(BookDbContext context) : base(context) { }


        /// <inheritdoc/>
        public override BookAuthorConnector Read(int id)
        {
            return context.BookAuthorConnectors.First(t => t.BookAuthorConnectorID == id);
        }

        /// <inheritdoc/>
        public override void Update(BookAuthorConnector element)
        {
            var old = Read(element.BookAuthorConnectorID);
            old.AuthorID = element.AuthorID;
            old.BookID = element.BookID;

            context.SaveChanges();
        }
    }
}
