using System;

namespace QGXUN0_HFT_2023241.Client
{
    class Program
    {
        static void Main()
        {
            Action authorMenu = () => CustomConsole.Menu("AUTHOR MANAGER",
                new Tuple<string, Action>("Create new author", ModelAction.Author.Create),
                new Tuple<string, Action>("List all authors", ModelAction.Author.List),
                new Tuple<string, Action>("Read author", ModelAction.Author.Read),
                new Tuple<string, Action>("Update author", ModelAction.Author.Update),
                new Tuple<string, Action>("Delete author", ModelAction.Author.Delete),

                new Tuple<string, Action>("<<<    Highest rated author    >>>", ModelAction.Author.HighestRated),
                new Tuple<string, Action>("<<<    Lowest rated author    >>>", ModelAction.Author.LowestRated),
                new Tuple<string, Action>("<<<    Series from an author    >>>", ModelAction.Author.Series),
                new Tuple<string, Action>("<<<    Select filtered book from an author    >>>", ModelAction.Author.SelectBook));


            Action bookMenu = () => CustomConsole.Menu("BOOK MANAGER",
                new Tuple<string, Action>("Create new book", ModelAction.Book.Create),
                new Tuple<string, Action>("List all books", ModelAction.Book.List),
                new Tuple<string, Action>("Read book", ModelAction.Book.Read),
                new Tuple<string, Action>("Update book", ModelAction.Book.Update),
                new Tuple<string, Action>("Delete book", ModelAction.Book.Delete),

                new Tuple<string, Action>("<<<    Add authors to a book    >>>", ModelAction.Book.AddAuthors),
                new Tuple<string, Action>("<<<    Remove authors from a book    >>>", ModelAction.Book.RemoveAuthors),
                new Tuple<string, Action>("<<<    List books in year    >>>", ModelAction.Book.InYear),
                new Tuple<string, Action>("<<<    List books between years    >>>", ModelAction.Book.BetweenYears),
                new Tuple<string, Action>("<<<    List books where the title has texts    >>>", ModelAction.Book.TitleContains), //FORMAT
                new Tuple<string, Action>("<<<    Select filtered book    >>>", ModelAction.Book.Select));


            Action collectionMenu = () => CustomConsole.Menu("COLLECTION MANAGER",
                new Tuple<string, Action>("Create new collection", ModelAction.Collection.Create),
                new Tuple<string, Action>("List all collections", ModelAction.Collection.List),
                new Tuple<string, Action>("Read collection", ModelAction.Collection.Read),
                new Tuple<string, Action>("Update collection", ModelAction.Collection.Update),
                new Tuple<string, Action>("Delete collection", ModelAction.Collection.Delete),

                new Tuple<string, Action>("<<<    Add books to a collection    >>>", ModelAction.Collection.AddBooks),
                new Tuple<string, Action>("<<<    Remove books from a collection    >>>", ModelAction.Collection.RemoveAuthors),
                new Tuple<string, Action>("<<<    List series collections    >>>", ModelAction.Collection.Series),
                new Tuple<string, Action>("<<<    List non-series collections    >>>", ModelAction.Collection.NonSeries),
                new Tuple<string, Action>("<<<    List collections in year    >>>", ModelAction.Collection.InYear),
                new Tuple<string, Action>("<<<    List collections between years    >>>", ModelAction.Collection.BetweenYears),
                new Tuple<string, Action>("<<<    Summarized price of a collection    >>>", ModelAction.Collection.Price),
                new Tuple<string, Action>("<<<    Average rating of a collection    >>>", ModelAction.Collection.Rating),
                new Tuple<string, Action>("<<<    Select filtered collection    >>>", ModelAction.Collection.Select),
                new Tuple<string, Action>("<<<    Select filtered book from a collection    >>>", ModelAction.Collection.SelectBook));


            Action publisherMenu = () => CustomConsole.Menu("PUBLISHER MANAGER",
                new Tuple<string, Action>("Create new publisher", ModelAction.Publisher.Create),
                new Tuple<string, Action>("List all publishers", ModelAction.Publisher.List),
                new Tuple<string, Action>("Read publisher", ModelAction.Publisher.Read),
                new Tuple<string, Action>("Update publisher", ModelAction.Publisher.Update),
                new Tuple<string, Action>("Delete publisher", ModelAction.Publisher.Delete),

                new Tuple<string, Action>("<<<    List series publishers    >>>", ModelAction.Publisher.Series),
                new Tuple<string, Action>("<<<    List only series publishers    >>>", ModelAction.Publisher.OnlySeries),
                new Tuple<string, Action>("<<<    Highest rated publisher    >>>", ModelAction.Publisher.HighestRated),
                new Tuple<string, Action>("<<<    Lowest rated publisher    >>>", ModelAction.Publisher.LowestRated),
                new Tuple<string, Action>("<<<    Average rating of a publisher    >>>", ModelAction.Publisher.Rating),
                new Tuple<string, Action>("<<<    Authors    >>>", ModelAction.Publisher.Authors),
                new Tuple<string, Action>("<<<    Permanent authors    >>>", ModelAction.Publisher.PermanentAuthors),
                new Tuple<string, Action>("<<<    Permanent authors of a publisher    >>>", ModelAction.Publisher.PermanentAuthorsOfPublisher));



            CustomConsole.Menu("B O O K     D A T A B A S E     M A N A G E R",
                new Tuple<string, Action>("AUTHOR MANAGER", authorMenu),
                new Tuple<string, Action>("BOOK MANAGER", bookMenu),
                new Tuple<string, Action>("COLLECTION MANAGER", collectionMenu),
                new Tuple<string, Action>("PUBLISHER MANAGER", publisherMenu));
        }
    }
}
