using CryptoBot.Commands.Attribute;
using CryptoBot.Commands.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CryptoBot.Commands.Service
{
    public class CommandHandlerService
    {

        private readonly List<IModule> _modules;

        public CommandHandlerService()
        {
            _modules = new List<IModule>();
        }

        public void RegisterModule(IModule module)
        {
            if (_modules.Contains(module))
                throw new InvalidOperationException("You have already registered this command module.");

            _modules.Add(module);
        }

        public Task ExecuteCommandAsync(string commandName)
        {
            foreach(var module in _modules)
            {
                var methods = module.GetType().GetMethods().Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0).ToArray();
                foreach(var method in methods)
                {
                    var cmdAttr = (CommandAttribute)method.GetCustomAttribute(typeof(CommandAttribute));
                    if(cmdAttr != null && cmdAttr.CommandName.Equals(commandName))
                    {
                        method.Invoke(module, null);
                    }
                }
            }
            
            return Task.CompletedTask;
        }

    }
}
