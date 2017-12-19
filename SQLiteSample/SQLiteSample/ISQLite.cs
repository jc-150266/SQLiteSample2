using SQLite.Net;
using System;
using System.IO;

namespace SQLiteSample
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection(); // <-1
    }
}