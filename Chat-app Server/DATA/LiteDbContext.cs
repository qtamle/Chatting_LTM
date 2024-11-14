using Communicator;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_app_Server.DATA
{
    public static class LiteDbContext
    {
        private static LiteDatabase _db;
        
        public static LiteDatabase Db
        {
            get { 
                
                if (_db == null)
                {
                    _db = new LiteDatabase("@ChatServerDb.dat");
                } 

                return _db;
            }
        }
    }
}
