using DynamicApp.Abstractions;
using DynamicApp.Shared;
using System;
using System.ComponentModel.Composition;

namespace AnotherPaymentModule
{
    [Export(typeof(IDynamicModule))]
    public class AntotherPaymentModule : IDynamicModule
    {
        public string Name 
        {
            get 
            {
                return "Some other payment module";
            }
        }

        public IMenu GetModuleMenu()
        {
            var item = new MenuItem((parameters) =>
            {
                Console.WriteLine("Execute another module ");
                Console.ReadLine();
            })
            {
                DisplayText = Name
            };
            return item;
        }

        public void InitializeModule(string initializeData)
        {
            //throw new NotImplementedException();
        }
    }
}
