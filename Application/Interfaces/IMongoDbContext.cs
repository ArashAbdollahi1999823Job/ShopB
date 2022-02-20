using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Application.Interfaces
{
    public  interface  IMongoDbContext<T>
    {
        public IMongoCollection<T> GetCollection();
    }
}
