using QGXUN0_HFT_2023241.Models;
using QGXUN0_HFT_2023241.Repository.Database;
using QGXUN0_HFT_2023241.Repository.Template;
using System.Linq;

namespace QGXUN0_HFT_2023241.Repository.ModelRepository
{
    /// <inheritdoc/>
    public class PublisherRepository : Repository<Publisher>, IRepository<Publisher>
    {
        /// <inheritdoc/>
        public PublisherRepository(BookDbContext context) : base(context) { }


        /// <inheritdoc/>
        public override Publisher Read(int id)
        {
            return context.Publishers.First(t => t.PublisherID == id);
        }

        /// <inheritdoc/>
        public override void Update(Publisher element)
        {
            var old = Read(element.PublisherID);

            foreach (var prop in old.GetType().GetProperties())
                prop.SetValue(old, prop.GetValue(element));

            context.SaveChanges();
        }
    }
}
