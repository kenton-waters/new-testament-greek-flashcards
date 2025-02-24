﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FlashcardGen.Core;
using FlashcardGen.Common;
using FlashcardGen.DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Constants.LocalFiles.ConfigFileName, optional: false);

var configuration = configBuilder.Build();

var services = new ServiceCollection();

services.AddSingleton<IConfiguration>(configuration);
services.AddSingleton<ICardGenerator, CardGenerator>();
services.AddSingleton<IDatabaseAccessor, DatabaseAccessor>();
services.AddSingleton<ILocalFileAccessor, LocalFileAccessor>();

using (var inMemoryConnection = new SqliteConnection(Constants.ConnectionStrings.InMemory))
{
    inMemoryConnection.Open();

    services.AddSingleton<SqliteConnection>(inMemoryConnection);

    services.AddDbContext<OpenGreekNewTestamentContext>(
        options => options.UseSqlite(inMemoryConnection)
    );

    services.BuildServiceProvider()
    .GetService<ICardGenerator>()!
    .GenerateCards();
}