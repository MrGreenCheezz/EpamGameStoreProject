using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Text;
using CapstoneProjectLibrary.Interfaces;

namespace CapstoneProjectLibrary
{
    public class DbConfiguration : IDbConfig
    {
        private readonly ConfigurationBuilder _configBuilder = new ConfigurationBuilder();

        private string _connectionString { get; set; }
        public string _developmentConnectionString { get; set; }

        public DbConfiguration()
        {
            _connectionString = _configBuilder.AddJsonFile("appsettings.json").Build().GetValue<string>("ConnectionStrings:Value");
            _developmentConnectionString = _configBuilder.AddJsonFile("appsettings.Development.json").Build().GetValue<string>("ConnectionStrings:Value");
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
