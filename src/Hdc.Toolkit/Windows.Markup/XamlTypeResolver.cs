﻿using System;
using System.Collections.Generic;
using System.Windows.Markup;

//removed by rein
//using Catel.Windows.Properties;
//using log4net;

namespace Hdc.Windows.Markup
{
    /// <summary>
    /// Custom <see cref="IXamlTypeResolver"/> because Microsoft "wisely" decided to make it internal.
    /// </summary>
    /// <remarks>
    /// This class was intended to be used by the <see cref="StyleHelper"/> class, but the issue was solved by
    /// using the <see cref="XamlTypeMapper"/> class. Therefore, this class is just party finished (but not yet thrown away).
    /// </remarks>
    internal class XamlTypeResolver : IXamlTypeResolver
    {
        #region Variables
        
        //removed by rein
        //private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType);

        private readonly Dictionary<string, Type> _cache = new Dictionary<string, Type>();
        #endregion

        #region Methods
        /// <summary>
        /// When implemented in a derived class, resolves a XAML element to the corresponding type.
        /// </summary>
        /// <param name="qualifiedTypeName">The fully qualified type name to resolve.</param>
        /// <returns>
        /// The type that <paramref name="qualifiedTypeName"/> represents.
        /// </returns>
        public Type Resolve(string qualifiedTypeName)
        {
            string typeName = CleanUpTypeName(qualifiedTypeName);

            ResolveType(typeName);

            if (!_cache.ContainsKey(typeName))
            {
                //removed by rein
                //Log.Warn(TraceMessages.CouldNotResolveType, qualifiedTypeName);
                return null;
            }

            //removed by rein
            //Log.Debug(TraceMessages.SuccessfullyResolvedType, qualifiedTypeName, _cache[typeName]);

            return _cache[typeName];
        }

        /// <summary>
        /// Resolves the type.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        private void ResolveType(string typeName)
        {
            if (_cache.ContainsKey(typeName))
            {
                return;
            }

            Type dotNetType = ResolveDotNetFrameworkType(typeName);
            if (dotNetType != null)
            {
                _cache.Add(typeName, dotNetType);
                return;
            }

            // Resolve manually
            // TODO:
        }

        /// <summary>
        /// Resolves the type of the .NET Framework (thus a control located in the .NET Framework that can be used without
        /// namespace prefix in XAML).
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns>
        /// 	<see cref="Type"/> or <c>null</c> if the name is not a .NET Framework type.
        /// </returns>
        private static Type ResolveDotNetFrameworkType(string typeName)
        {
            // Create list of possible prefixes
            List<string> prefixes = new List<string>();
            prefixes.Add("System.Windows.Controls");
            prefixes.Add("System.Windows");

            foreach (string prefix in prefixes)
            {
                string fullTypeName = string.Format("{0}{1}", prefix, typeName);
                Type type = Type.GetType(fullTypeName);
                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }

        /// <summary>
        /// Cleans up the name of the type by stripping "{x:Type" and "}" from the type name.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns></returns>
        private static string CleanUpTypeName(string typeName)
        {
            // TODO: Implement
            return typeName;
        }
        #endregion
    }
}
