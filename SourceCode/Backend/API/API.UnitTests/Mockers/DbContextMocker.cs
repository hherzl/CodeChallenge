﻿using API.Core.DataLayer;

namespace API.UnitTests.Mockers
{
    public static class DbContextMocker
    {
        public static StoreDbContext GetStoreDbContext(string dbName)
        {
            var dbContext = new StoreDbContext(DbContextOptionsMocker.GetDbOptions(dbName));

            dbContext.SeedInMemory();

            return dbContext;
        }
    }
}
