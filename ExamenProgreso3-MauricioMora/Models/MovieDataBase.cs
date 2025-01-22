using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;

namespace ExamenProgreso3_MauricioMora.Models
{
    public class MovieDatabase
    {
        private static SQLiteAsyncConnection Database;

        private static async Task InitializeAsync()
        {
            if (Database == null)
            {
                string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mmora_movies.db");
                Database = new SQLiteAsyncConnection(dbPath);
                await Database.CreateTableAsync<MovieEntity>();
            }
        }

        // Cambiar la propiedad Instance por un método asincrónico
        public static async Task<MovieDatabase> GetInstanceAsync()
        {
            await InitializeAsync();
            return new MovieDatabase();
        }

        public Task<int> SaveMovieAsync(MovieEntity movie)
        {
            return Database.InsertAsync(movie);
        }

        public Task<List<MovieEntity>> GetMoviesAsync()
        {
            return Database.Table<MovieEntity>().ToListAsync();
        }
    }
}
