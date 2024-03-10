using Microsoft.EntityFrameworkCore;
using QGXUN0_HFT_2023241.Models.Models;

namespace QGXUN0_HFT_2023241.Repository.Database
{
    /// <summary>
    /// Specifies the tables and their connections of a <see cref="DbContext"/>.
    /// </summary>
    public class BookDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the book table of the <see cref="BookDbContext"/>
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the author table of the <see cref="BookDbContext"/>
        /// </summary>
        public DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the book and author connector table of the <see cref="BookDbContext"/>
        /// </summary>
        public DbSet<BookAuthorConnector> BookAuthorConnectors { get; set; }

        /// <summary>
        /// Gets or sets the collection table of the <see cref="BookDbContext"/>
        /// </summary>
        public DbSet<Collection> Collections { get; set; }

        /// <summary>
        /// Gets or sets the book and collection connector table of the <see cref="BookDbContext"/>
        /// </summary>
        public DbSet<BookCollectionConnector> BookCollectionConnectors { get; set; }

        /// <summary>
        /// Gets or sets the publisher table of the <see cref="BookDbContext"/>
        /// </summary>
        public DbSet<Publisher> Publishers { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Collection"/> <see langword="class"/> by ensuring the creation of the database.
        /// </summary>
        public BookDbContext()
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Specifies the configuration of the database.
        /// </summary>
        /// <remarks>Sets the database to be <see langword="InMemory Database"/> and use <see langword="Lazy Loading"/></remarks>
        /// <param name="optionsBuilder">Options builder of the <see cref="BookDbContext"/></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseInMemoryDatabase("book").UseLazyLoadingProxies();
        }

        /// <summary>
        /// Specifies the actions of the database during creation.
        /// </summary>
        /// <remarks>Sets the database table connections and loads the default values</remarks>
        /// <param name="modelBuilder">Database model builder of the <see cref="BookDbContext"/></param>
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
                        .OnDelete(DeleteBehavior.Cascade),
                    book => book
                        .HasOne(connector => connector.Book)
                        .WithMany()
                        .HasForeignKey(connector => connector.BookID)
                        .OnDelete(DeleteBehavior.Cascade)
                );

            modelBuilder.Entity<BookAuthorConnector>()
                .HasOne(connector => connector.Author)
                .WithMany(author => author.BookConnector)
                .HasForeignKey(connector => connector.AuthorID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookAuthorConnector>()
                .HasOne(connector => connector.Book)
                .WithMany(author => author.AuthorConnector)
                .HasForeignKey(connector => connector.BookID)
                .OnDelete(DeleteBehavior.Cascade);

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
                        .OnDelete(DeleteBehavior.Cascade),
                    book => book
                        .HasOne(connector => connector.Book)
                        .WithMany()
                        .HasForeignKey(connector => connector.BookID)
                        .OnDelete(DeleteBehavior.Cascade)
                );

            modelBuilder.Entity<BookCollectionConnector>()
                .HasOne(connector => connector.Collection)
                .WithMany(collection => collection.BookConnector)
                .HasForeignKey(connector => connector.CollectionID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookCollectionConnector>()
                .HasOne(connector => connector.Book)
                .WithMany(book => book.CollectionConnector)
                .HasForeignKey(connector => connector.BookID)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Book-Publisher connector

            modelBuilder.Entity<Book>(book => book
                .HasOne(book => book.Publisher)
                .WithMany(publisher => publisher.Books)
                .HasForeignKey(book => book.PublisherID)
                .OnDelete(DeleteBehavior.Cascade)
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
