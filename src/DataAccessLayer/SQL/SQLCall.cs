using DataAccessLayer.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.SQL
{
    static public class SQLCall
    {
        public static IEnumerable<T> GetAllRequest<T>(string connectionString, string command) where T : new()
        {
            List<T> resultList = new List<T>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(command, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var result = new T();
                    foreach (PropertyInfo property in typeof(T).GetProperties())
                    {
                        if (!IsNavigationProperty(property))
                            property.SetValue(result, Convert.ChangeType(rdr[property.Name], property.PropertyType), null);
                    }
                    resultList.Add(result);
                }
            }
            return resultList;
        }

        private static bool IsNavigationProperty(PropertyInfo property)
        {
            foreach (Attribute atr in property.GetCustomAttributes())
            {
                if (atr is NavigationPropertyAttribute)
                {
                    return true;
                }
            }
            return false;
        }

        public static void CreateRequest<T>(string connectionString, string command, T obj)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(command, con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (PropertyInfo property in typeof(T).GetProperties())
                {
                    if (property.Name != "Id" && !IsNavigationProperty(property))
                        cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(obj) ?? DBNull.Value);

                }
                cmd.ExecuteNonQuery();
            }
        }

        public static T GetByIdRequest<T>(string connectionString, string command, int id) where T : new()
        {
            T result = new T();
            bool isFind = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(command, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    isFind = true;
                    foreach (PropertyInfo property in typeof(T).GetProperties())
                    {
                        if (!IsNavigationProperty(property))
                            property.SetValue(result, Convert.ChangeType(rdr[property.Name], property.PropertyType), null);
                    }
                }
                if (isFind)
                {
                    return result;
                }
                return default(T);

            }
        }

        public static void UpdateRequest<T>(string connectionString, string command, T obj)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(command, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                foreach (PropertyInfo property in typeof(T).GetProperties())
                {
                    if (!IsNavigationProperty(property))
                        cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(obj) ?? DBNull.Value);
                }
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteRequest(string connectionString, string command, int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(command, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}