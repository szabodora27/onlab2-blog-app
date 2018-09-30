﻿using Microsoft.Extensions.Logging;
using System;

namespace Blog.Dal.Logging
{
    public class DBLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private string _connectionString;

        public DBLoggerProvider(Func<string, LogLevel, bool> filter, string connectionStr)
        {
            _filter = filter;
            _connectionString = connectionStr;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DBLogger(categoryName, _filter, _connectionString);
        }

        public void Dispose()
        {
        }
    }
}
