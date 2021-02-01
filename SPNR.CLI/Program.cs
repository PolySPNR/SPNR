﻿using System;
using System.IO;
using ER.Shared.Container;

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