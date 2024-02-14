using Microsoft.EntityFrameworkCore;
using System;

namespace IntegrationSample
{
    public class MyAppDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options)
        : base(options)
        {

            // ensures that in memory database structure gets created.
            Database.EnsureCreated();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // This is to generate sample data in the memory database.
            modelBuilder.Entity<Player>().HasData(
                new Player
                {
                    Id = 1,
                    Brand = "myexamplebrand",
                    Nickname = "Player1",
                    Country = "SE",
                    Language = "en_GB",
                    Currency = "EUR",
                    Registered = DateTime.Parse("2019-01-01"),
                    Birthdate = DateTime.Parse("1980-01-01"),
                    Locked = false,
                    Balance = 1000M,
                    BonusBalance = 0,
                    Token = "player1token"
                }
        );



            modelBuilder.Entity<Player>().HasData(
        new Player
        {
            Id = 2,
            Brand = "myexamplebrand",
            Nickname = "Player2",
            Country = "FR",
            Language = "en_GB",
            Currency = "USD",
            Registered = DateTime.Parse("2019-01-01"),
            Birthdate = DateTime.Parse("1980-01-01"),
            Locked = true,
            Balance = 9.212M,
            BonusBalance = 0,
            Token = "player2token"
        }
);



            modelBuilder.Entity<Player>().HasData(
        new Player
        {
            Id = 3,
            Brand = "myexamplebrand",
            Nickname = "Player3",
            Country = "FR",
            Language = "en_GB",
            Currency = "USD",
            Registered = DateTime.Parse("2019-01-01"),
            Birthdate = DateTime.Parse("1980-01-01"),
            Locked = true,
            Balance = 1000M,
            BonusBalance = 0,
            Token = "player3token"
        }
);


            modelBuilder.Entity<Player>().HasData(
        new Player
        {
            Id = 4,
            Brand = "myexamplebrand",
            Nickname = "Player4",
            Country = "FR",
            Language = "en_GB",
            Currency = "USD",
            Registered = DateTime.Parse("2019-01-01"),
            Birthdate = DateTime.Parse("1980-01-01"),
            Locked = true,
            Balance = 1000M,
            BonusBalance = 0,
            Token = "player4token"
        }
);


            modelBuilder.Entity<Player>().HasData(
        new Player
        {
            Id = 5,
            Brand = "myexamplebrand",
            Nickname = "Player5",
            Country = "FR",
            Language = "en_GB",
            Currency = "USD",
            Registered = DateTime.Parse("2019-01-01"),
            Birthdate = DateTime.Parse("1980-01-01"),
            Locked = true,
            Balance = 1000M,
            BonusBalance = 0,
            Token = "player5token"
        }
);


            modelBuilder.Entity<Player>().HasData(
         new Player
         {
             Id = 6,
             Brand = "myexamplebrand",
             Nickname = "Player6",
             Country = "FR",
             Language = "en_GB",
             Currency = "USD",
             Registered = DateTime.Parse("2019-01-01"),
             Birthdate = DateTime.Parse("1980-01-01"),
             Locked = true,
             Balance = 1000M,
             BonusBalance = 0,
             Token = "player6token"
         }
 );

            //bot players
            for (int i = 0; i < 10; i++)
            {


                modelBuilder.Entity<Player>().HasData(
                new Player
                {
                    Id = 7 + i,
                    Brand = "botPlayerNr" + i,
                    Nickname = "botPlayerNr" + i,
                    Country = "JP",
                    Language = "en_GB",
                    Currency = "USD",
                    Registered = DateTime.Parse("2019-01-01"),
                    Birthdate = DateTime.Parse("1980-01-01"),
                    Locked = false,
                    Balance = 10000M,
                    BonusBalance = 0,
                    Token = "botPlayerNr" + i
                });
            }

        }
    }

}