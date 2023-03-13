using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Text;
using CapstoneProjectLibrary.Interfaces;

namespace CapstoneProjectLibrary
{
    public class DbConfiguration : IDbConfig
    {
        public string Value { get; set; }

        public string GetConnectionString()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
            return config.GetValue<string>("ConnectionStrings:Value");
        }
    }
}
