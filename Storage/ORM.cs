using Storage.Attributes;
using Storage.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Storage
{

    public abstract class ORM<TModel> : IEnumerable<TModel> where TModel : class, new()
    {
        private string tableName;
        private SqlConnection Connection { get; set; }
        public ORM(string connectionString)
        {
            this.Connection = new SqlConnection(connectionString);
            SetTableName();
        }
        private void SetTableName()
        {
            var attribute = (TableAttribute)Attribute.GetCustomAttribute(typeof(TModel), typeof(TableAttribute)); //Henter attribut for at hente navnet
            if (attribute != null)
                this.tableName = attribute.TableName;
            else
                this.tableName = typeof(TModel).Name;
        }
        private TModel Map(SqlDataReader reader)
        {
            TModel model = new TModel(); 
            var propertyNames = typeof(TModel).GetProperties(); // Henter properties fra modellen

            foreach (var property in propertyNames) //Looper igennem og sætter værdierne på vores model, som den får fra readeren
                typeof(TModel).GetProperty(property.Name).SetValue(model, reader[GetColumnName(property)]);

            return model;
        }
        private string GetColumnName(PropertyInfo property)
        {
            string name = "";

            var attr = (ColumnAttribute)property.GetCustomAttributes(true).FirstOrDefault(o => o.GetType() == typeof(ColumnAttribute)); //Henter attributten hvor columnname står

            if (attr != null) //Hvis der ikk er coulumnname, så burger den propertynavnet istedet
                name = attr.ColumnName;
            else
                name = property.Name;

            return name;

        }
        public int Insert(TModel model)
        {
            var cmd = InsertToCommand(model); //Laver vores insert command

            Connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            Connection.Close();

            return rowsAffected;
        }
        private SqlCommand InsertToCommand(TModel model)
        {
            List<PropertyInfo> properties = typeof(TModel).GetProperties().ToList();

            for (int i = 0; i < properties.Count; i++)
            {
                var property = properties[i];

                if (IsIdentity(property)) //Hvis den er identitet, så fjerner den propertien
                {
                    properties.RemoveAt(i);
                    break;
                }
            }

            string query = $"INSERT INTO {tableName} ({string.Join(", ", properties.Select(o => $"[{GetColumnName(o)}]"))}) VALUES ({string.Join(", ", properties.Select(o => $"@{o.Name}"))})";
            SqlCommand cmd = new SqlCommand(query, Connection);

            foreach (var property in properties)
            {
                cmd.Parameters.AddWithValue($"@{property.Name}", property.GetValue(model));
            }
            return cmd;
        }
        private bool IsIdentity(string column)
        {
            string q = $"SELECT columnproperty(object_id('{tableName}'),'{column}','IsIdentity')"; //Command til at se om vores column er identitet. Retunere 1 hvis den er, ellers 0
            var c = new SqlCommand(q, Connection);

            Connection.Open();
            var r = c.ExecuteReader();
            r.Read();
            var t = (int)r[""];
            Connection.Close();
            return t == 1;
        }
        private bool IsIdentity(PropertyInfo column)
        {
            return IsIdentity(GetColumnName(column));
        }
        public int Remove(TModel model)
        {
            var identity = GetIdentity(model);

            if (identity != null)
            {
                object id = identity.GetValue(model);
                string name = GetColumnName(identity);

                string query = $"DELETE FROM {tableName} WHERE [{name}] = {id};";

                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                var r = cmd.ExecuteNonQuery();
                Connection.Close();
                return r;
            }
            else
            {
                throw new ArgumentException("the entity doesn't have any Identity!");
            }
        }
        private PropertyInfo GetIdentity(TModel model)
        {
            List<PropertyInfo> properties = typeof(TModel).GetProperties().ToList();

            for (int i = 0; i < properties.Count; i++)
            {
                var name = GetColumnName(properties[i]);

                if (IsIdentity(name))
                    return properties[i];
            }
            return null;
        }
        public IEnumerator<TModel> GetEnumerator()
        {
            TModel model = new TModel();

            var properties = typeof(TModel).GetProperties();
            string query = $"SELECT {string.Join(", ", properties.Select(o => $"[{GetColumnName(o)}]"))} FROM {tableName}";

            SqlCommand cmd = new SqlCommand(query, Connection);

            Connection.Open();

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return Map(reader);
            }
            Connection.Close();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
