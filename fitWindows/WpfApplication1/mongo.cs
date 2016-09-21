using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace db
{
    public class mongo
    {
        static mongo instance;
        internal IMongoDatabase database;
        internal IMongoCollection<BsonDocument> collection;
        
        public static mongo mongoInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new mongo();
                }
                return instance;
            }
        } 

        public void connect()
        {
            Process mongoD = new Process();
            try
            {
                mongoD.StartInfo.UseShellExecute = false;
                mongoD.StartInfo.CreateNoWindow = true;
                mongoD.StartInfo.FileName = @"C:\Program Files\MongoDB\Server\3.2\bin\mongod.exe";
                mongoD.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            IMongoClient client = new MongoClient();
            database = client.GetDatabase("fit");
            try
            {
                database.CreateCollection("eList");
            }
            catch(Exception e) { }
            collection = database.GetCollection<BsonDocument>("eList");

        }

        public void addExercize(string name, string[] muscles, string location)
        {
            var exercize = new BsonDocument
            {
                { "name", name },
                { "muscles", new BsonArray(muscles) },
                { "location", location }
            };
            collection.InsertOne(exercize);
        }
    }

}
