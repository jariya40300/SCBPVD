using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SCBPVD.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sql, U parameter);
        Task<Tuple<T, List<T1>, List<T2>>> LoadDataMultiOneSingleTwoList<T, T1, T2, U>(string sql, U parameter);
        Task<Tuple<T, List<T1>>> LoadDataMultiOneSingliOneList<T, T1, U>(string sql, U parameter);
        Task<Tuple<T, T1, List<T2>>> LoadDataMultiTwoSingleOneList<T, T1, T2, U>(string sql, U parameter);
        Task<Tuple<T, T1, List<T2>, List<T3>>> LoadDataMultiTwoSingleTwoList<T, T1, T2, T3, U>(string sql, U parameter);
        Task<T> LoadDataSingle<T, U>(string sql, U parameter);
        IEnumerable<object> MapExcuteReader<T>(IDataReader dr, T o);
        Task SaveData<T>(string sql, T parameter);
        Task<T> SaveDataReader<T, T1, U>(string sql, U parameter, T1 type);
        Task<T> SaveDataScalar<T, U>(string sql, U parameter);
        Task SaveData(string sql, Dictionary<string, string[]> data);
    }
}