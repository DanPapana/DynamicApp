using DynamicApp.Abstractions;
using DynamicApp.Shared;
using System;
using System.ComponentModel.Composition;

namespace BCRPaymentModule
{
    [Export(typeof(IDynamicModule))]
    public class BCPPayment : IDynamicModule
    {
        public string Name 
        {
            get
            {
                return "BCR Payment Module";
            }
        }

        public IMenu GetModuleMenu()
        {
            IMenu retMenu = new MenuItem((parameters)=> 
            { 
                Console.WriteLine("BCR PaymentModule Execute"); 
                Console.ReadLine(); 
            }) { DisplayText = Name };

            return retMenu;
        }

        public void InitializeModule(string initializeData)
        {
            //throw new NotImplementedException();
        }
    }
}
