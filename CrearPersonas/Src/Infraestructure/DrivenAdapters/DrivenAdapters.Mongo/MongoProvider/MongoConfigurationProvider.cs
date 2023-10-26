using DrivenAdapters.Mongo.Entities;
using Helpers.ObjectsUtils.Setting;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections;
using System.Text.Json;

namespace DrivenAdapters.Mongo.MongoCP
{
    public class MongoConfigurationProvider : ConfigurationProvider
    {
        private readonly IMongoCollection<BusinessParameter> _collection;

        public MongoConfigurationProvider(string connectionString, string databaseName, string collectionName, bool reloadOnChange)
        {
            IMongoClient client = new MongoClient(connectionString);

            IMongoDatabase database = client.GetDatabase(databaseName);

            _collection = database.GetCollection<BusinessParameter>(collectionName);

            if (reloadOnChange)
                _ = WatchCollectionStatic();
        }

        private async Task WatchCollectionStatic()
        {
            using IChangeStreamCursor<ChangeStreamDocument<BusinessParameter>> cursor = _collection.Watch();

            await cursor.ForEachAsync((Action<ChangeStreamDocument<BusinessParameter>>)delegate
            {
                Load();
                OnReload();
            }, default);
        }

        #region Métodos Privados

        public override void Load()
        {
            List<BusinessParameter> configs = _collection
                .Find(FilterDefinition<BusinessParameter>.Empty)
                .ToList();

            int num = 0;

            Type type;

            Type typeArray;

            string clave;

            object valor;

            string obtenerValor;

            IDictionary<string, object> dictionary;

            foreach (BusinessParameter config in configs)
            {
                clave = config.Nombre;

                valor = config.Valor;

                type = valor.GetType();

                if (type.IsPrimitive || type == typeof(decimal) || type == typeof(string))
                {
                    Set($"{nameof(AppSettings)}:" + clave, valor.ToString());
                }
                else if (valor is IEnumerable)
                {
                    foreach (dynamic array in valor as IEnumerable)
                    {
                        typeArray = array.GetType();

                        if (typeArray.IsPrimitive || typeArray == typeof(decimal) || typeArray == typeof(string))
                        {
                            Set($"{nameof(AppSettings)}:{clave}:{num}", ((object)array).ToString());
                        }
                        else
                        {
                            dictionary = array;

                            foreach (string key in dictionary.Keys)
                            {
                                Set($"{nameof(AppSettings)}:{clave}:{num}:{key}", $"{dictionary[key]}");
                            }

                            Set($"{nameof(AppSettings)}:{clave}:{num}", JsonSerializer.Serialize((object)array));
                        }

                        num++;
                    }
                }
                else
                {
                    obtenerValor = JsonSerializer.Serialize(valor);
                    Set($"{nameof(AppSettings)}:" + clave, obtenerValor);
                }
            }
        }

        #endregion Métodos Privados
    }
}

