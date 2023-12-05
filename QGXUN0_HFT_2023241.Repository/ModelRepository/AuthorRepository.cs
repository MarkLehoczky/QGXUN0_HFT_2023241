using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023241.Repository.Database;
using QGXUN0_HFT_2023241.Repository.Template;
using System.Linq;

namespace QGXUN0_HFT_2023241.Repository.ModelRepository
{
    /// <inheritdoc/>
    public class AuthorRepository : Repository<Author>, IRepository<Author>
    {
        /// <inheritdoc/>
        public AuthorRepository(BookDbContext context) : base(context) { }


        /// <inheritdoc/>
        public override Author Read(int id)
        {
            return context.Authors.First(t => t.AuthorID == id);
        }

        /// <inheritdoc/>
        public override void Update(Author element)
        {
            var old = Read(element.AuthorID);

            foreach (var prop in old.GetType().GetProperties())
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                    prop.SetValue(old, prop.GetValue(element));

            context.SaveChanges();
        }
    }
}
