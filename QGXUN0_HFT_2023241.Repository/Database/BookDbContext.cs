using Microsoft.EntityFrameworkCore;
using QGXUN0_HFT_2023241.Models.Models;

namespace QGXUN0_HFT_2023241.Repository.Database
{
    /// <summary>
    /// Database context for the <see cref="Book"/>, <see cref="Author"/>, <see cref="Collection"/>, <see cref="Publisher"/>, and their connection
    /// </summary>
    public class BookDbContext : DbContext
    {
        /// <summary>
        /// Books of the database
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Authors of the database
        /// </summary>
        public DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Connector for the <see cref="Book"></see> instances and <see cref="Author"></see> instances 
        /// </summary>
        public DbSet<BookAuthorConnector> BookAuthorConnectors { get; set; }

        /// <summary>
        /// Collections of the database
        /// </summary>
        public DbSet<Collection> Collections { get; set; }

        /// <summary>
        /// Connector for the <see cref="Book"></see> instances and <see cref="Collection"></see> instances 
        /// </summary>
        public DbSet<BookCollectionConnector> BookCollectionConnectors { get; set; }

        /// <summary>
        /// Publishers of the database
        /// </summary>
        public DbSet<Publisher> Publishers { get; set; }

        /// <summary>
        /// Empty constructor, which ensures the database creation
        /// </summary>
        public BookDbContext()
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Configures for InMemoryDatabase and LazyLoading
        /// </summary>
        /// <param name="optionsBuilder">Options builder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseInMemoryDatabase("book").UseLazyLoadingProxies();
        }

        /// <summary>
        /// Creates the database model
        /// </summary>
        /// <param name="modelBuilder">Database model</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Book-Author connector

            modelBuilder.Entity<Book>()
                .HasMany(book => book.Authors)
                .WithMany(author => author.Books)
                .UsingEntity<BookAuthorConnector>(
                    author => author
                        .HasOne(connector => connector.Author)
                        .WithMany()
                        .HasForeignKey(connector => connector.AuthorID)
                        .OnDelete(DeleteBehavior.NoAction),
                    book => book
                        .HasOne(connector => connector.Book)
                        .WithMany()
                        .HasForeignKey(connector => connector.BookID)
                        .OnDelete(DeleteBehavior.NoAction)
                );

            modelBuilder.Entity<BookAuthorConnector>()
                .HasOne(connector => connector.Author)
                .WithMany(author => author.BookConnector)
                .HasForeignKey(connector => connector.AuthorID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookAuthorConnector>()
                .HasOne(connector => connector.Book)
                .WithMany(author => author.AuthorConnector)
                .HasForeignKey(connector => connector.BookID)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Book-Collection connector

            modelBuilder.Entity<Book>()
                .HasMany(book => book.Collections)
                .WithMany(collection => collection.Books)
                .UsingEntity<BookCollectionConnector>(
                    collection => collection
                        .HasOne(connector => connector.Collection)
                        .WithMany()
                        .HasForeignKey(connector => connector.CollectionID)
                        .OnDelete(DeleteBehavior.NoAction),
                    book => book
                        .HasOne(connector => connector.Book)
                        .WithMany()
                        .HasForeignKey(connector => connector.BookID)
                        .OnDelete(DeleteBehavior.NoAction)
                );

            modelBuilder.Entity<BookCollectionConnector>()
                .HasOne(connector => connector.Collection)
                .WithMany(collection => collection.BookConnector)
                .HasForeignKey(connector => connector.CollectionID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookCollectionConnector>()
                .HasOne(connector => connector.Book)
                .WithMany(book => book.CollectionConnector)
                .HasForeignKey(connector => connector.BookID)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Book-Publisher connector

            modelBuilder.Entity<Book>(book => book
                .HasOne(book => book.Publisher)
                .WithMany(publisher => publisher.Books)
                .HasForeignKey(book => book.PublisherID)
                .OnDelete(DeleteBehavior.NoAction)
                );

            #endregion

            #region Database loading

            modelBuilder.Entity<Book>().HasData(DbSeed.Books);
            modelBuilder.Entity<Author>().HasData(DbSeed.Authors);
            modelBuilder.Entity<Collection>().HasData(DbSeed.Collections);
            modelBuilder.Entity<Publisher>().HasData(DbSeed.Publishers);
            modelBuilder.Entity<BookAuthorConnector>().HasData(DbSeed.BookAuthorConnectors);
            modelBuilder.Entity<BookCollectionConnector>().HasData(DbSeed.BookCollectionConnectors);

            #endregion
        }
    }
}
