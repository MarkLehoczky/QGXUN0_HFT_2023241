using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023241.Repository.Database;
using QGXUN0_HFT_2023241.Repository.Template;
using System.Linq;

namespace QGXUN0_HFT_2023241.Repository.ModelRepository
{
    /// <inheritdoc/>
    public class CollectionRepository : Repository<Collection>, IRepository<Collection>
    {
        /// <inheritdoc/>
        public CollectionRepository(BookDbContext context) : base(context) { }


        /// <inheritdoc/>
        public override Collection Read(int id)
        {
            return context.Collections.First(t => t.CollectionID == id);
        }

        /// <inheritdoc/>
        public override void Update(Collection element)
        {
            var old = Read(element.CollectionID);

            foreach (var prop in old.GetType().GetProperties())
                prop.SetValue(old, prop.GetValue(element));

            context.SaveChanges();
        }
    }
}
