using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageDataAccess.Models;
using MongoDB.Driver;

namespace GarageDataAccess.DataAccess
{
    public class MongoDataAccess   //Hänvisar till MongoDb Servern, databasen och collection
    {         
        private const string ConnectionString = "mongodb+srv://Sourati:high1234@cluster0.yzf7cuu.mongodb.net/test";
        private const string DatabaseName = "garagedb";
        private const string CarCollection = "garage_chart";

        private IMongoCollection<T> ConnectToMongo<T>(in string collection) // Kopplar till mongodb och returnernar från Mongodb
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            return db.GetCollection<T>(collection);
        }

        public async Task<List<CarModel>> GetAllCars()  //Kopplar mot databasen och returnerar all data
        {
            var carCollection = ConnectToMongo<CarModel>(CarCollection);
            var results = await carCollection.FindAsync(_ => true);
            return results.ToList();

        }

        public Task CreateCar(CarModel car)   //Kopplar mot databasen och låter användaren att skapa ny data
        {
            var carCollection = ConnectToMongo<CarModel>(CarCollection);
            return carCollection.InsertOneAsync(car);
        }
        public Task UpdateCar(CarModel car) //Kopplar mot databasen och låter användaren att ändra befintlig data
        {
            var carCollection = ConnectToMongo<CarModel>(CarCollection);
            var filter = Builders<CarModel>.Filter.Eq("Id", car.Id);
            return carCollection.ReplaceOneAsync(filter, car, new ReplaceOptions { IsUpsert = true });
        }
        public Task DeleteCar(CarModel car)   //Kopplar mot databasen och låter användaren att radera befintlig data
        {
            var carCollection = ConnectToMongo<CarModel>(CarCollection);
            return carCollection.DeleteOneAsync(c => c.Id == car.Id);

        }

        public int InputControl()   //Metod för att bara siffrorna 1 till 5 kan skrivas 
        {
            int val = 0;

            while (!Int32.TryParse(Console.ReadLine(), out val) || val < 1 || val > 5)
            {
                Console.WriteLine("Du behöver skriva en siffra, välj något av alternativen");
            }
            return val;
        }

    }


}
