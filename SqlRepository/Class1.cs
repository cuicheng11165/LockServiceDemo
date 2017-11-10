using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRepository
{
    using System.Data.SqlClient;
    using LockServiceDemo;

    public class SqlRepository : ILockRepository
    {
        private String connectionString;

        public SqlRepository(string connection)
        {
            this.connectionString = connection;
        }

        /// <summary>
        ///     return true if success, false if fail or key already exists.
        /// </summary>
        public void InsertUniqueKey(String rowKey, Int32 timeout)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        ///     return true if success, false if fail or key already exists.
        /// </summary>
        public Task InsertUniqueKeyAsync(String rowKey, Int32 timeout)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "";
                    return cmd.ExecuteNonQueryAsync();
                }
            }            
        }

        /// <summary>
        ///     return true if success
        /// </summary>
        public void RemoveKey(String rowKey)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "";
                    cmd.ExecuteNonQuery();
                }
            } 
        }


        /// <summary>
        ///     return true if success
        /// </summary>
        public Task RemoveKeyAsync(String rowKey)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "";
                    return cmd.ExecuteNonQueryAsync();
                }
            }    
        }
    }


    public class DBInitialize
    {

        public static void EnsureDataSource(string connection, bool forceCreateDatabase)
        {
            var builder = new SqlConnectionStringBuilder(connection);
            var databaseName = builder.InitialCatalog;

            var masterSqlConnection = new SqlConnectionStringBuilder();
            masterSqlConnection.DataSource = builder.DataSource;
            masterSqlConnection.InitialCatalog = "master";            
            masterSqlConnection.IntegratedSecurity = true;
            masterSqlConnection.UserID = builder.UserID;
            masterSqlConnection.Password = builder.Password;

            bool databaseExist = false;
            using (SqlConnection masterConnection = new SqlConnection(masterSqlConnection.ToString()))
            {
                masterConnection.Open();
                using (var cmd = masterConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM SYS.DATABASES WHERE NAME =@Name";
                    cmd.Parameters.AddWithValue("@Name", databaseName);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            databaseExist = true;
                        }                        
                    }
                }
                using (var cmd = masterConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM SYS.DATABASES WHERE NAME =@Name";
                    cmd.Parameters.AddWithValue("@Name", databaseName);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            databaseExist = true;
                        }                        
                    }
                }

                if (!databaseExist)
                {
                    if (forceCreateDatabase)
                    {
                        throw new Exception("database does not exsit");
                    }

                    using (var cmd = masterConnection.CreateCommand())
                    {
                        cmd.CommandText = "CREATE DATABASE " + databaseName;                        
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = masterConnection.CreateCommand())
                    {
                        cmd.CommandText = @"CREATE TABLE [Lock] (
                        [ResourceName] nvarchar(255)  NOT NULL,
                        [Expiration] datatime NOT NULL)";                        
                        cmd.ExecuteNonQuery();
                    }
                }

                






            }


        }
    }
}
