using System;
using System.Collections.Generic;
using System.IO;
using ER.Shared.Container;
using Newtonsoft.Json;
using SPNR.Core.Api.ELibrary;
using SPNR.Core.Api.ELibrary.Credentials;
using SPNR.Core.Models;

namespace SPNR.CLI
{
    internal class Program
    {
        public static string[] Arguments { get; private set; }

        private static void Main(string[] args)
        {
            Arguments = args;

            LoadEnvironment();
            
            var appContainer = new AppContainer<Startup, EntryPoint>();
            appContainer.Initialize();
            appContainer.Run();
        }

        private static void LoadEnvironment()
        {
            if (!File.Exists(".env"))
                return;

            foreach (var envVar in File.ReadAllLines(".env"))
            {
                var varPair = envVar.Split("=");

                if (varPair.Length < 2)
                    continue;

                Environment.SetEnvironmentVariable(varPair[0], varPair[1]);
            }
        }
    }
}