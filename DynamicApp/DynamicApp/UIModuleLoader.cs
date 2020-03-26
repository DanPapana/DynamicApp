using DynamicApp.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ComponentModel.Composition;

namespace DynamicApp
{
    internal class UIModuleLoader
    {
        [ImportMany(typeof(IDynamicModule))]
        private readonly List<IDynamicModule> modules = new List<IDynamicModule>();

        public IEnumerable<IDynamicModule> Modules
        {
            get
            {
                return modules.AsReadOnly();
            }
        }

        public bool LoadModulesFromDirectory(string directory)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            var files = directoryInfo.EnumerateFiles("*.dll");
            bool retVal = false;

            foreach (var fileInfo in files)
            {
                var currentAssembly = LoadAssembly(fileInfo.FullName);
                if (currentAssembly != null)
                {
                    retVal |= LoadDynamicModules(currentAssembly);
                }
            }

            return retVal;
        }

        private Assembly LoadAssembly(string path)
        {
            Assembly retAssembly = null;
            try
            {
                retAssembly = Assembly.LoadFrom(path);
            }
            catch (Exception e)
            {
                //log exception
            }

            return retAssembly;
        }

        private bool LoadDynamicModules(Assembly assemblyToUse)
        {
            var types = assemblyToUse.GetExportedTypes();
            var dynamicModuleTypes = types.Where(type => typeof(IDynamicModule).IsAssignableFrom(type));
            bool retVal = true;

            foreach (var dynamicModuleType in dynamicModuleTypes)
            {
                try
                {
                    IDynamicModule module = (IDynamicModule)Activator.CreateInstance(dynamicModuleType);
                    modules.Add(module);
                }
                catch (Exception e)
                {
                    //log exception  details
                    retVal = false;
                }
            }

            return retVal;
        }
    }
}