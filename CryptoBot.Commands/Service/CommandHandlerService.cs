using CryptoBot.Commands.Attribute;
using CryptoBot.Commands.Interface;
using Discord.WebSocket;
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
        private readonly string _commandPrefix;

        public CommandHandlerService()
        {
            _modules = new List<IModule>();
            _commandPrefix = "==c";
        }

        public void RegisterModule(IModule module)
        {
            if (_modules.Contains(module))
                throw new InvalidOperationException("You have already registered this command module.");

            _modules.Add(module);
        }

        public Task ExecuteCommandAsync(string commandName, string[] args, SocketMessage socketMessage = null)
        {
            foreach(var module in _modules)
            {
                var methods = module.GetType().GetMethods().Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0).ToArray();
                foreach(var method in methods)
                {
                    var cmdAttr = (CommandAttribute)method.GetCustomAttribute(typeof(CommandAttribute));
                    if(cmdAttr != null && cmdAttr.CommandName.Equals(commandName))
                    {
                        var parameters = new object[]
                        {
                            args,
                            socketMessage
                        };
                        method.Invoke(module, parameters);
                    }
                }
            }
            
            return Task.CompletedTask;
        }

        public async Task ExecuteCommandAsync(string commandName)
        {
            await ExecuteCommandAsync(commandName, new string[0]);
        }

        public async Task ExecuteCommandAsync(SocketMessage message)
        {
            string[] cmdParams = message.Content.Split(' ');
            if (cmdParams[0].Equals(_commandPrefix))
            {
                await ExecuteCommandAsync(cmdParams[1], cmdParams.Skip(2).Take(cmdParams.Length - 2).ToArray(), message);
            }
            else
            {
                await Task.CompletedTask;
            }
        }

    }
}
