using System;
using System.Collections.Generic;
using System.Linq;

namespace KooliProjekt.Application.Data
{
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext context)
        {
            SeedUsers(context);
            SeedCategories(context);
            SeedProducts(context);
            SeedOrders(context);
            SeedOrderLines(context);
        }

        private static void SeedUsers(ApplicationDbContext context)
        {
            if (context.Set<User>().Any())
            {
                return;
            }

            List<User> kasutajad = new List<User>();

            for (int i = 1; i <= 35; i++)
            {
                User kasutaja = new User();
                kasutaja.firsName = "Eesnimi" + i;
                kasutaja.lastName = "Perenimi" + i;
                kasutaja.email = "kasutaja" + i + "@example.com";
                kasutaja.phoneNumber = "555000" + i;
                kasutaja.UserName = "kasutaja" + i;
                kasutaja.password = "Parool123";

                kasutajad.Add(kasutaja);
            }

            context.Set<User>().AddRange(kasutajad);
            context.SaveChanges();
        }
        private static void SeedCategories(ApplicationDbContext context)
        {
            if (context.Set<ProductCategory>().Any())
            {
                return;
            }

            List<ProductCategory> kategooriad = new List<ProductCategory>();

            for (int i = 1; i <= 35; i++)
            {
                ProductCategory kat = new ProductCategory();
                kat.Name = "Kategooria " + i;
                kategooriad.Add(kat);
            }

            context.Set<ProductCategory>().AddRange(kategooriad);
            context.SaveChanges();
        }

        private static void SeedProducts(ApplicationDbContext context)
        {
            if (context.Products.Any())
            {
                return;
            }

            List<ProductCategory> olemasolevadKategooriad = context.Set<ProductCategory>().ToList();

            if (olemasolevadKategooriad.Count > 0)
            {
                List<Product> tooted = new List<Product>();

                for (int i = 1; i <= 35; i++)
                {
                    Product toode = new Product();
                    toode.Name = "Toode " + i;
                    toode.description = "Kirjeldus tootele " + i;
                    toode.price = i * 10;
                    toode.productCategoryId = olemasolevadKategooriad[0].Id;

                    tooted.Add(toode);
                }

                context.Products.AddRange(tooted);
                context.SaveChanges();
            }
        }

        private static void SeedOrders(ApplicationDbContext context)
        {
            if (context.Set<Order>().Any())
            {
                return;
            }

            List<User> olemasolevadKasutajad = context.Set<User>().ToList();

            if (olemasolevadKasutajad.Count > 0)
            {
                List<Order> tellimused = new List<Order>();

                for (int i = 1; i <= 35; i++)
                {
                    Order tellimus = new Order();
                    tellimus.OrderDate = DateTime.Now;
                    tellimus.UserId = olemasolevadKasutajad[i % olemasolevadKasutajad.Count].id;

                    tellimused.Add(tellimus);
                }

                context.Set<Order>().AddRange(tellimused);
                context.SaveChanges();
            }
        }

        private static void SeedOrderLines(ApplicationDbContext context)
        {
            if (context.Set<OrderLine>().Any())
            {
                return;
            }

            List<Product> olemasolevadTooted = context.Products.ToList();
            List<Order> olemasolevadTellimused = context.Set<Order>().ToList();

            if (olemasolevadTooted.Count > 0 && olemasolevadTellimused.Count > 0)
            {
                List<OrderLine> rread = new List<OrderLine>();

                for (int i = 1; i <= 35; i++)
                {
                    OrderLine rida = new OrderLine();
                    rida.OrderId = olemasolevadTellimused[i % olemasolevadTellimused.Count].Id;
                    rida.ProductId = olemasolevadTooted[i % olemasolevadTooted.Count].id;
                    rida.Price = olemasolevadTooted[i % olemasolevadTooted.Count].price;
                    rida.Quantity = 1;

                    rread.Add(rida);
                }

                context.Set<OrderLine>().AddRange(rread);
                context.SaveChanges();
            }
        }
    }
}