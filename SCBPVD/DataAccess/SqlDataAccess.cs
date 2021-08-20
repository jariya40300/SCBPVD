using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Omu.ValueInjecter;
using SCBPVD.DataAccess.Models;

namespace SCBPVD.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SCBPVD"].ConnectionString;
        //public string ConnectionStringName { get; set; } = "Default";



        public async Task<List<T>> LoadData<T, U>(string sql, U parameter)
        {


            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var data = await connection.QueryAsync<T>(sql, parameter, commandType: CommandType.StoredProcedure);

                    return data.ToList();
                }
                catch (Exception ex)
                {
                    List<StatusState> statusStates = new List<StatusState>{ new StatusState
                    {
                        Status = "reject",
                        Reason = ex.Message,
                        AlertText = "Can't connect database"
                    } };

                    IEnumerable<T> enumerable = (IEnumerable<T>)(IEnumerator<T>)statusStates;
                    return enumerable.ToList();
                }
                finally
                {
                    connection.Close();
                }

            }
        }
        public async Task<T> LoadDataSingle<T, U>(string sql, U parameter)
        {


            using (IDbConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    var data = await connection.QuerySingleOrDefaultAsync<T>(sql, parameter, commandType: CommandType.StoredProcedure);

                    return data;
                }
                catch (Exception ex)
                {
                    StatusState statusStates = new StatusState
                    {

                        Status = "reject",
                        Reason = ex.Message,
                        AlertText = "Can't connect database"
                    };
                    return (T)Convert.ChangeType(statusStates, typeof(T));
                }
                finally
                {
                    connection.Close();
                }

            }
        }
        public async Task<Tuple<T, T1, List<T2>, List<T3>>> LoadDataMultiTwoSingleTwoList<T, T1, T2, T3, U>(string sql, U parameter)
        {


            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var data = await connection.QueryMultipleAsync(sql, parameter, commandType: CommandType.StoredProcedure);

                    var t1 = data.Read<T>().Single();
                    var t2 = data.Read<T1>().Single();
                    var t3 = data.Read<T2>().ToList();
                    var t4 = data.Read<T3>().ToList();

                    return Tuple.Create(t1, t2, t3, t4);


                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public async Task<Tuple<T, List<T1>, List<T2>>> LoadDataMultiOneSingleTwoList<T, T1, T2, U>(string sql, U parameter)
        {

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var data = await connection.QueryMultipleAsync(sql, parameter, commandType: CommandType.StoredProcedure);

                    var t1 = data.Read<T>().Single();
                    var t2 = data.Read<T1>().ToList();
                    var t3 = data.Read<T2>().ToList();

                    return Tuple.Create(t1, t2, t3);


                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public async Task<Tuple<T, T1, List<T2>>> LoadDataMultiTwoSingleOneList<T, T1, T2, U>(string sql, U parameter)
        {


            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var data = await connection.QueryMultipleAsync(sql, parameter, commandType: CommandType.StoredProcedure);

                    var t1 = data.Read<T>().Single();
                    var t2 = data.Read<T1>().Single();
                    var t3 = data.Read<T2>().ToList();

                    return Tuple.Create(t1, t2, t3);


                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    connection.Close();
                }




            }
        }

        public async Task<Tuple<T, List<T1>>> LoadDataMultiOneSingliOneList<T, T1, U>(string sql, U parameter)
        {


            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var data = await connection.QueryMultipleAsync(sql, parameter, commandType: CommandType.StoredProcedure);
                    var t1 = data.Read<T>().Single();
                    var t2 = data.Read<T1>().ToList();

                    return Tuple.Create(t1, t2);


                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    connection.Close();
                }




            }
        }

        public async Task<T> SaveDataScalar<T, U>(string sql, U parameter)
        {

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var data = await connection.ExecuteScalarAsync<T>(sql, parameter, commandType: CommandType.StoredProcedure);


                    return data;
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public async Task<T> SaveDataReader<T, T1, U>(string sql, U parameter, T1 type)
        {

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var data = await connection.ExecuteReaderAsync(sql, parameter, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                    IEnumerable<object> obj = MapExcuteReader<T1>(data, type);
                    var list = obj.Cast<T>().ToList().SingleOrDefault();

                    return list;
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public async Task SaveData<T>(string sql, T parameter)
        {

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    await connection.ExecuteAsync(sql, parameter, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("time"))
                    {
                      connection.Close();
                      await  SaveData<T>(sql, parameter).ConfigureAwait(false);
                    }
                    //StatusState s = new StatusState
                    //{
                    //    Status = "reject",
                    //    Reason = ex.Message,
                    //    AlertText = "Can't connect database"
                    //};
                }
                finally
                {
                    connection.Close();
                }


            }

        }
        public async Task SaveData(string sql, Dictionary<string, string[]> data)
        {

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    foreach (var item in data)
                    {

                        var parameter = new
                        {
                            message_id = item.Key,
                            status = item.Value[0],
                            reason_reject = item.Value[1]

                        };

                        await connection.ExecuteAsync(sql, parameter, commandType: CommandType.StoredProcedure);
                    }

                   
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("time"))
                    {
                        connection.Close();
                        //await SaveData<T>(sql, parameter).ConfigureAwait(false);
                    }
                    //StatusState s = new StatusState
                    //{
                    //    Status = "reject",
                    //    Reason = ex.Message,
                    //    AlertText = "Can't connect database"
                    //};
                }
                finally
                {
                    connection.Close();
                }


            }

        }
        public IEnumerable<object> MapExcuteReader<T>(IDataReader dr, T o)
        {
            while (dr.Read())
            {
                //T t = new T();
                //T o = t;
                o.InjectFrom<DataReaderInjection>(dr);

                yield return o;
            }

        }
    }
}
