using AlcoolGasolina.Database.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlcoolGasolina.Database.Data
{
    public class PostoDataBase
    {
        readonly SQLiteAsyncConnection database;

        public PostoDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<PostoModel>().Wait();
        }

        public Task<List<PostoModel>> GetPostosAsync()
        {
            return database.Table<PostoModel>().ToListAsync();
        }

        public Task<PostoModel> GetPostoAsync(int id)
        {
            return database.Table<PostoModel>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SavePostoAsync(PostoModel item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeletePostoAsync(PostoModel item)
        {
            return database.DeleteAsync(item);
        }

        public Task<int> UpdatePostoAsync(PostoModel item)
        {
            return database.UpdateAsync(item);
        }
    }
}
