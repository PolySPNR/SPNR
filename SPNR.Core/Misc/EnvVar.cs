using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SPNR.Core.Misc
{
    /// <summary>
    ///     Get or set environment variable
    /// </summary>
    /// <typeparam name="T">Type of environment variable. Should be a basic type</typeparam>
    public class EnvVar<T>
    {
        internal static readonly Dictionary<string, EnvVar<T>> Vars = new();
        private readonly TypeConverter _converter;

        private readonly T _defValue;

        internal EnvVar(string name, T defaultValue)
        {
            Name = name;
            _defValue = defaultValue;
            _converter = TypeDescriptor.GetConverter(typeof(T));
        }

        /// <summary>
        ///     Name of environment variable
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Value of environment variable
        /// </summary>
        public T Value
        {
            get
            {
                var sVal = Environment.GetEnvironmentVariable(Name);
                if (sVal == null)
                    return _defValue;

                T val;

                try
                {
                    val = (T) _converter.ConvertFromString(sVal);
                }
                catch
                {
                    val = _defValue;
                }

                return val;
            }

            set => Environment.SetEnvironmentVariable(Name, value.ToString());
        }
    }

    /// <summary>
    ///     Pre-defined environment variables
    /// </summary>
    public static class EnvVar
    {
        /// <summary>
        ///     Get variable
        /// </summary>
        /// <param name="name">Name of variable</param>
        /// <param name="defaultValue">Default value of environment</param>
        /// <returns></returns>
        public static EnvVar<T> Get<T>(string name, T defaultValue)
        {
            if (EnvVar<T>.Vars.ContainsKey(name))
                return EnvVar<T>.Vars[name];

            var envVar = new EnvVar<T>(name, defaultValue);
            EnvVar<T>.Vars.Add(name, envVar);

            return envVar;
        }
    }
}