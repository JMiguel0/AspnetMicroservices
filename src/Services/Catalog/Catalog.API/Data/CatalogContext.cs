using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {

        public CatalogContext(IConfiguration configuration)
        {
            //Mongo client connection
            MongoClient client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            //Mongo databse connection
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            //Mongo collection connection
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

            //Function to fill collection in Mongo if there´s no data
            CatalogContextSeed.SeedData(Products);
        }

        //Propertie where all products from collection are stored
        public IMongoCollection<Product> Products { get; }
    }
}
