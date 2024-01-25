using System.Data;

namespace Infla.Repositories
{
    public class BaseRepository
    {
        protected readonly IDbConnection _connection;

        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }
    }
}