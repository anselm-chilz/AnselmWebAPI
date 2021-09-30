using AnselmWebAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnselmWebAPI.Services
{
    public class CountryService
    {
        private readonly IMongoCollection<Country> _countries;

        public CountryService(ICountriesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _countries = database.GetCollection<Country>(settings.CountriesCollectionName);
        }

        public List<Country> Get() =>
            _countries.Find(country => true).ToList();

        public Country Get(string id) =>
            _countries.Find<Country>(country => country.Id == id).FirstOrDefault();

        public Country Create(Country country)
        {
            _countries.InsertOne(country);
            return country;
        }

        public void Update(string id, Country countryIn) =>
            _countries.ReplaceOne(country => country.Id == id, countryIn);

        public void Remove(Country countryIn) =>
            _countries.DeleteOne(country => country.Id == countryIn.Id);

        public void Remove(string id) =>
            _countries.DeleteOne(country => country.Id == id);
    }
}
