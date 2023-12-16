using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace КПО_1.Script
{
    public interface IContractRepository
    {
    }

    public class NoSqlContractRepository: IContractRepository
    {
        private readonly IMongoCollection<Contract> _collection;

        public NoSqlContractRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Contract>("contract");
        }


    }
}
