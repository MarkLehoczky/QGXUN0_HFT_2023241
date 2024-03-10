using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023241.Repository.Database;
using QGXUN0_HFT_2023241.Repository.Template;
using System.Linq;

namespace QGXUN0_HFT_2023241.Repository.ModelRepository
{
    /// <inheritdoc/>
    public class BookCollectionConnectorRepository : Repository<BookCollectionConnector>, IRepository<BookCollectionConnector>
    {
        /// <inheritdoc/>
        public BookCollectionConnectorRepository(BookDbContext context) : base(context) { }


        /// <inheritdoc/>
        public override BookCollectionConnector Read(int id)
        {
            return context.BookCollectionConnectors.First(t => t.BookCollectionConnectorID == id);
        }

        /// <inheritdoc/>
        public override void Update(BookCollectionConnector element)
        {
            var old = Read(element.BookCollectionConnectorID);
            old.BookID = element.BookID;
            old.CollectionID = element.CollectionID;
            old.PositionInSeries = element.PositionInSeries;

            context.SaveChanges();
        }
    }
}
