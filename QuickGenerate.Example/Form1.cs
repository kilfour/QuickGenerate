using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FluentWinForms.Grid;
using QuickGenerate.Complex;
using QuickGenerate.HardCodeThoseDates;
using QuickGenerate.Primitives;

namespace QuickGenerate.Example
{
    public partial class Form1 : Form
    {
        private readonly FluentGrid<Category> categoriesFluentGrid;
        private readonly FluentGrid<Product> productsFluentGrid;
        
        public Form1()
        {
            InitializeComponent();

            categoriesFluentGrid =
                new FluentGrid<Category>(masterGrid)
                    .AsNumber(c => c.Id, col => col.Width = 30)
                    .AsText(c => c.Title, col => col.Width = 150)
                    .AsText(c => c.Description, col => col.Width = 150)
                    .AsText(c => c.Products, col => col.Visible = false)
                    .SelectionChanged(c => productsFluentGrid.Populate(c.Products))
                    .Initialize();

            categoriesFluentGrid.Populate(GetRandomData());

            productsFluentGrid =
                new FluentGrid<Product>(detailsGrid)
                    .AsNumber(p => p.Id, col => col.Width = 30)
                    .AsText(p => p.Title, col => col.Width = 150)
                    .AsText(p => p.Description, col => col.Width = 150)
                    .AsNumber(p => p.Price, col => col.Width = 100)
                    .AsText(p => p.Created, col => col.Width = 100)
                    .FormatCell(p => p.Created, cell => cell.Value = ((DateTime)cell.Value).ToShortDateString())
                    .Initialize();
        }

        private static IEnumerable<Category> GetRandomData()
        {
            return
                new DomainGenerator()
                    .With<Category>(g => g.For(category => category.Id, 0, val => val + 1))
                    .With<Category>(g => g.For(category => category.Title, new StringGenerator(5, 10, 'H', 'a', 'l')))
                    .With<Category>(
                        g => g.For(category => category.Description,
                                   new StringBuilderGenerator()
                                       .Append("Not many", "Lots of", "Some")
                                       .Space()
                                       .Append("big", "small", "dangerous", "volatile", "futile")
                                       .Space()
                                       .Append("paintbrushes", "camels", "radio antaennas")
                                       .Period(),
                                   new NumericStringGenerator(4, 12)))
                    .OneToMany<Category, Product>(3, 7, (c, p) => c.Products.Add(p))
                    .With<Product>(g => g.For(product => product.Id, 0, val => val + 1))
                    .With<Product>(g => g.For(product => product.Title, "title1", "title2", "Another title"))
                    .With<Product>(
                        g => g.For(product => product.Description,
                                   new StringBuilderGenerator()
                                       .Append(WordGenerator.FromFile("Adjectives.txt")).Space()
                                       .Space().Dot().Dot().Dot().Space().Question(),
                                   new StringBuilderGenerator()
                                       .Append(WordGenerator.FromFile("Colours.txt")).Space()
                                       .Append(WordGenerator.FromFile("Adjectives.txt")).Space()
                                       .Append(WordGenerator.FromFile("Nouns.txt"))
                                       .Period()))
                    .With<Product>(g => g.For(product => product.Price, val => Math.Round(val, 2)))
                    .With<Product>(g => g.For(product => product.Created, 31.December(2009), val => val.AddDays(1)))
                    .Many<Category>(4, 8);
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<Product> Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
    }
}