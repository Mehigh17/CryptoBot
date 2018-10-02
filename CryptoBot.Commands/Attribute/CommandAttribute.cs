using System;

namespace CryptoBot.Commands.Attribute
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class CommandAttribute : System.Attribute
    {

        public string CommandName { get; }

        public CommandAttribute(string commandName)
        {
            CommandName = commandName;
        }

    }
}
