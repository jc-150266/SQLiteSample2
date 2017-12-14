using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.IO;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using SQLiteSample.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_iOS))] // <-1
namespace SQLiteSample.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // <-2
            var libraryPath = Path.Combine(documentsPath, "..", "Library"); // <-3
            var path = Path.Combine(libraryPath, "TodoSQLite.db3");         // <-4
            return new SQLiteConnection(new SQLitePlatformIOS(), path);     // <-5
        }
    }
}