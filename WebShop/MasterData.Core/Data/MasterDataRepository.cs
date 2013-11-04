using System;
using System.Linq;
using DotNetPrograms.Common.Exceptions;
using MasterData.Core.Domain;
using MissingLinq.Sql;
using MissingLinq.Sql.ExtensionMethods;
using NHibernate;
using NHibernate.Linq;

namespace MasterData.Core.Data
{
    public class MasterDataRepository : IMasterDataRepository
    {
        private readonly ISession _session;
        private readonly IQueryProvider _queryProvider;

        public MasterDataRepository(ISession session)
        {
            _session = session;
            _queryProvider = new MissingLinqQueryProvider(new QueryableToSqlTranslator(), _session.Connection);
        }

        public T Get<T>(Guid id) where T : MasterDataObject
        {
            return _session.Get<T>(id);
        }

        public T GetOrThrow<T>(Guid id) where T : MasterDataObject
        {
            var item = Get<T>(id);
            if (item == null)
            {
                throw UserException.Unknown<T>(() => id);
            }
            return item;
        }

        public IQueryable<T> GetAll<T>() where T : MasterDataObject
        {
            return _session.Query<T>();
        }

        public T Save<T>(T item) where T : MasterDataObject
        {
            _session.SaveOrUpdate(item);
            return item;
        }

        public void Commit()
        {
            _session.Flush();
        }

        public void DeleteAll<T>() where T : MasterDataObject
        {
            Linq<T>().Delete();
        }

        private void ExecuteNonQuery(string sql, params object[] arguments)
        {
            using (var command = _session.Connection.CreateCommand())
            {
                command.CommandText = sql;
                for (var ii = 0; ii < arguments.Length; ii++)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = string.Format("@p{0}", ii);
                    parameter.Value = arguments[ii];
                    command.Parameters.Add(parameter);
                }
                command.ExecuteNonQuery();
            }
        }

        public void Delete<T>(T item) where T : MasterDataObject
        {
            _session.Delete(item);
        }

        public IQueryable<T> Linq<T>() where T : MasterDataObject
        {
            return new MissingLinqQueryable<T>(_queryProvider);
        }
    }
}