using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snake.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace Snake.Data
{
    public class SnakeDB
    {
        //DataBase Connection
        readonly SQLiteAsyncConnection _database;

        //Create Tables 
        public SnakeDB(string dbPath)
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
        //Getting User by Name
        public Task<UserModel> GetUserByNameAsync(string n)
        {
            return _database.Table<UserModel>()
                            .Where(i => i.UserName == n)
                            .FirstOrDefaultAsync();
        }
        //Getting User by Active
        public Task<UserModel> GetActiveUserAsync()
        {
            return _database.Table<UserModel>()
                            .Where(i => i.Active == 1)
                            .FirstOrDefaultAsync();
        }
        //Checking Users Exist
        public async Task<bool> CheckUsers()
        => await _database.Table<UserModel>().CountAsync() != 0 ? true : false;
        //Checking User Name is in Use
        public async Task<bool> CheckUsersByName(string n)
        => await _database.Table<UserModel>()
            .Where(i => i.UserName == n)
            .CountAsync() != 0 ? true : false;

        //Getting HighScores
        public Task<List<UserScoresModel>> GetHighScoresAsync()
        {
            return _database.Table<UserScoresModel>()
                .OrderByDescending(x => x.Score)
                .ToListAsync();
        }

        //Getting HighScores by UserID
        public Task<UserScoresModel> GetBestHighScoreByUserAsync(int id)
        {
            return _database.Table<UserScoresModel>()
                            .Where(i => i.UserID == id)
                            .OrderByDescending(x => x.Score)
                            .FirstOrDefaultAsync();
        }
        public Task<List<UserScoresModel>> GetHighScoresByUserAsync(int id)
        {
            return _database.Table<UserScoresModel>()
                            .Where(i => i.UserID == id)
                            .OrderByDescending(x => x.Score)
                            .ToListAsync();
        }
        //Checking Users Exist
        public async Task<bool> CheckUserHS(int id)
        => await _database.Table<UserScoresModel>()
            .Where(i => i.UserID == id)
            .CountAsync() != 0 ? true : false;

        //Getting HighScores by MapID
        public Task<List<UserScoresModel>> GetHighScoresByMapAsync(int id)
        {
            return _database.Table<UserScoresModel>()
                            .Where(i => i.MapID == id)
                            .ToListAsync();
        }
        //Checking HS by MapID & UserID
        public async Task<bool> CheckUserMapHS(int Uid, int Mid)
        => await _database.Table<UserScoresModel>()
            .Where(i => (i.UserID == Uid) && (i.MapID == Mid))
            .CountAsync() != 0 ? true : false;

        //Getting HighScores by UserID & MapID
        public Task<UserScoresModel> GetBestHighScoreByUserMapAsync(int Uid, int Mid)
        {
            return _database.Table<UserScoresModel>()
                            .Where(i => (i.UserID == Uid) && (i.MapID == Mid))
                            .FirstOrDefaultAsync();
        }

        //Getting Maps
        public Task<List<MapModel>> GetMapsAsync()
        {
            return _database.Table<MapModel>().ToListAsync();
        }
        //Getting Map by Name
        public Task<MapModel> GetMapByNameAsync(string n)
        {
            return _database.Table<MapModel>()
                            .Where(i => i.MapName == n)
                            .FirstOrDefaultAsync();
        }

        //Getting Map Rows
        public Task<List<MapRowModel>> GetMapRowsAsync()
        {
            return _database.Table<MapRowModel>().ToListAsync();
        }
        //Getting Map Rows
        public Task<MapRowModel> GetMapRowsByIdAsync(int id, int r)
        {
            return _database.Table<MapRowModel>()
                            .Where(i => (i.MapID == id) && (i.RowNum == r))
                            .FirstOrDefaultAsync();
        }
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

        //Save Map Info in DB
        public Task<int> SaveMapInfoAsync(MapModel mapID)
        {
            if (mapID.MapID != 0)
            {
                return _database.UpdateAsync(mapID);
            }
            else
            {
                return _database.InsertAsync(mapID);
            }
        }
        //Save Map Row in DB
        public Task<int> SaveMapRowAsync(MapRowModel mapRowID)
        {
            if (mapRowID.MapRowID != 0)
            {
                return _database.UpdateAsync(mapRowID);
            }
            else
            {
                return _database.InsertAsync(mapRowID);
            }
        }

        //Save HighScore in to DB
        public Task<int> SaveHighScoreAsync(UserScoresModel hs)
        {

            if (hs.ScoreID != 0)
            {
                return _database.UpdateAsync(hs);
            }
            else
            {
                return _database.InsertAsync(hs);
            }

        }
        //Adding ENDS
    }
}