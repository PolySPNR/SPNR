using System;
using System.Collections.Generic;
using System.CommandLine.Invocation;
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
        public static InvocationContext InvocationContext { get; private set; }

        private static void Main(InvocationContext invocationContext, string[] args)
        {
            Arguments = args;
            InvocationContext = invocationContext;

            LoadEnvironment();

            // var elib = new ELibApi();
            //
            // elib.Authorize(ELibCredentialsType.LogPassPair, "WGOS", "0puWGQunjNDNP3Xt").Wait();
            //
            //
            // Console.WriteLine(JsonConvert.SerializeObject(elib.GetWorkInfo(41448623).Result, Formatting.Indented));
            
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