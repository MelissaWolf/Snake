using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snake.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace Snake.Data
{
    public class SnakeDatabase
    {
        //DataBase Connection
        readonly SQLiteAsyncConnection _database;

        //Create Tables 
        public SnakeDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<UserModel>().Wait();
            _database.CreateTableAsync<UserScoresModel>().Wait();
            _database.CreateTableAsync<MapModel>().Wait();
            _database.CreateTableAsync<MapRowModel>().Wait();
        }

        //Getting
        //Getting All Users
        public Task<List<UserModel>> GetUsersAsync()
        {
            return _database.Table<UserModel>().ToListAsync();
        }

        //Getting User by ID
        public Task<UserModel> GetUserAsync(int id)
        {
            return _database.Table<UserModel>()
                            .Where(i => i.UserID == id)
                            .FirstOrDefaultAsync();
        }
        //Checking Users Exist
        public async Task<bool> CheckUsers()
        => await _database.Table<UserModel>().CountAsync() != 0 ? true : false;
        //Getting ENDS

        //Adding
        //Save User in DB
        public Task<int> SaveUserAsync(UserModel user)
        {

            if (user.UserID != 0)
            {
                return _database.UpdateAsync(user);
            }
            else
            {
                return _database.InsertAsync(user);
            }

        }


    }
}
