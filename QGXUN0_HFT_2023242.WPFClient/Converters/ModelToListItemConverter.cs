using QGXUN0_HFT_2023241.Models.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace QGXUN0_HFT_2023242.WPFClient.Converters
{
    public class conv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Join("\n", (value as IEnumerable<Author>).Select(t => t.AuthorName));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    [ValueConversion(typeof(object), typeof(FrameworkElement))]
    public class ModelToListItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<Author> auhtors) return auhtors.Select(a => ConvertAuthor(a));
            else if (value is IEnumerable<Book> books) return books.Select(b => ConvertBook(b));
            else if (value is IEnumerable<Collection> collections) return collections.Select(c => ConvertCollection(c));
            else if (value is IEnumerable<Publisher> publishers) return publishers.Select(p => ConvertPublisher(p));
            else throw new NotSupportedException($"Parameter '{nameof(value)}' is supported type");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }


        private static UIElement ConvertAuthor(Author author)
        {
            var authorName = new TextBlock
            {
                Text = author.AuthorName,
                TextWrapping = TextWrapping.NoWrap,
                TextTrimming = TextTrimming.CharacterEllipsis,
                FontFamily = new FontFamily("Cascadia Mono"),
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(5),
            };
            Grid.SetColumn(authorName, 0);

            var books = new TextBlock
            {
                Text = string.Join("\n", author.Books.Select(t => t.Title)),
                TextWrapping = TextWrapping.Wrap,
                FontFamily = new FontFamily("Cascadia Mono"),
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(5),
            };
            Grid.SetColumn(books, 1);


            var grid = new Grid();
            grid.ShowGridLines = true;
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60, GridUnitType.Star) });

            grid.Children.Add(authorName);
            grid.Children.Add(books);

            return grid;
        }

        private static UIElement ConvertBook(Book book)
        {
            var authors = new Border
            {
                Child = new TextBlock
                {
                    Text = string.Join("\n", book.Authors.Select(t => t.AuthorName)),
                    TextWrapping = TextWrapping.Wrap,
                    FontFamily = new FontFamily("Cascadia Mono"),
                    FontSize = 12,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                },
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1, 0, 1, 0),
                Padding = new Thickness(5),
            };
            Grid.SetColumn(authors, 0);

            var title = new Border
            {
                Child = new TextBlock
                {
                    Text = book.Title,
                    TextWrapping = TextWrapping.Wrap,
                    FontFamily = new FontFamily("Cascadia Mono"),
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                },
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1, 0, 1, 0),
                Padding = new Thickness(5),
            };
            Grid.SetColumn(title, 1);

            var year = new Border
            {
                Child = new TextBlock
                {
                    Text = book.Year.ToString(),
                    TextWrapping = TextWrapping.NoWrap,
                    FontFamily = new FontFamily("Cascadia Mono"),
                    FontSize = 12,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                },
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1, 0, 1, 0),
                Padding = new Thickness(5),
            };
            Grid.SetColumn(year, 2);

            var publisher = new Border
            {
                Child = new TextBlock
                {
                    Text = book.Publisher?.PublisherName,
                    TextWrapping = TextWrapping.NoWrap,
                    FontFamily = new FontFamily("Cascadia Mono"),
                    FontSize = 12,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                },
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1, 0, 1, 0),
                Padding = new Thickness(5),
            };
            Grid.SetColumn(publisher, 3);

            var price = new Border
            {
                Child = new TextBlock
                {
                    Text = book.Price.ToString(),
                    TextWrapping = TextWrapping.NoWrap,
                    FontFamily = new FontFamily("Cascadia Mono"),
                    FontSize = 12,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                },
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1, 0, 1, 0),
                Padding = new Thickness(5),
            };
            Grid.SetColumn(price, 4);

            var rating = new Border
            {
                Child = new TextBlock
                {
                    Text = book.Rating.ToString(),
                    TextWrapping = TextWrapping.NoWrap,
                    FontFamily = new FontFamily("Cascadia Mono"),
                    FontSize = 12,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                },
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1, 0, 1, 0),
                Padding = new Thickness(5),
            };
            Grid.SetColumn(rating, 5);


            var grid = new Grid();

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2.5, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(11, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2.5, GridUnitType.Star) });

            grid.Children.Add(authors);
            grid.Children.Add(title);
            grid.Children.Add(year);
            grid.Children.Add(publisher);
            grid.Children.Add(price);
            grid.Children.Add(rating);

            return new Border
            {
                Child = grid,
                BorderBrush = Brushes.DimGray,
                BorderThickness = new Thickness(2)
            };
        }

        private static UIElement ConvertCollection(Collection collection)
        {
            var collectionName = new TextBlock
            {
                Text = collection.CollectionName,
                TextWrapping = TextWrapping.NoWrap,
                TextTrimming = TextTrimming.CharacterEllipsis,
                FontFamily = new FontFamily("Cascadia Mono"),
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(5),
            };
            Grid.SetColumn(collectionName, 0);

            var books = new TextBlock
            {
                Text = string.Join("\n", collection.Books.Select(t => t.Title)),
                TextWrapping = TextWrapping.Wrap,
                FontFamily = new FontFamily("Cascadia Mono"),
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(5),
            };
            Grid.SetColumn(books, 1);

            var isSeries = new TextBlock
            {
                Text = collection.IsSeries.ToString(),
                TextWrapping = TextWrapping.NoWrap,
                FontFamily = new FontFamily("Cascadia Mono"),
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(5),
            };
            Grid.SetColumn(isSeries, 2);


            var grid = new Grid();
            grid.ShowGridLines = true;
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });

            grid.Children.Add(collectionName);
            grid.Children.Add(books);
            grid.Children.Add(isSeries);

            return grid;
        }

        private static UIElement ConvertPublisher(Publisher publisher)
        {
            var publisherName = new TextBlock
            {
                Text = publisher.PublisherName,
                TextWrapping = TextWrapping.NoWrap,
                TextTrimming = TextTrimming.CharacterEllipsis,
                FontFamily = new FontFamily("Cascadia Mono"),
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(5),
            };
            Grid.SetColumn(publisherName, 0);

            var books = new TextBlock
            {
                Text = string.Join("\n", publisher.Books.Select(t => t.Title)),
                TextWrapping = TextWrapping.Wrap,
                FontFamily = new FontFamily("Cascadia Mono"),
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(5),
            };
            Grid.SetColumn(books, 1);

            var website = new TextBlock
            {
                Text = publisher.Website,
                TextWrapping = TextWrapping.NoWrap,
                FontFamily = new FontFamily("Cascadia Mono"),
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(5),
            };
            Grid.SetColumn(website, 2);


            var grid = new Grid();
            grid.ShowGridLines = true;
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });

            grid.Children.Add(publisherName);
            grid.Children.Add(books);
            grid.Children.Add(website);

            return grid;
        }
    }
}
