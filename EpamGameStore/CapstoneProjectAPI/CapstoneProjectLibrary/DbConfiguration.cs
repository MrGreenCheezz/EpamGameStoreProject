using System;
using System.Collections.Generic;
using System.Text;
using CapstoneProjectLibrary.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CapstoneProjectLibrary
{
    public class DbConfiguration : IDbConfig
    {
        private readonly ConfigurationBuilder _configBuilder = new ConfigurationBuilder();

        private string _connectionString { get; set; }
        public string _developmentConnectionString { get; set; }

        public DbConfiguration()
        {
            _connectionString = _configBuilder.AddJsonFile("appsettings.json").Build().GetConnectionString("ConnectionStrings:Value");
            _developmentConnectionString = _configBuilder.AddJsonFile("appsettings.Development.json").Build().GetConnectionString("Value");
        }

        public string GetConnectionString(ConfigurationTypes configType)
        {
            switch (configType){
                case ConfigurationTypes.Development:
                    return _developmentConnectionString;
                case ConfigurationTypes.Release:
                    return _connectionString;
            }
            return "";
        }
    }
}
