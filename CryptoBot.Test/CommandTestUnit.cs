using CryptoBot.Commands.Attribute;
using CryptoBot.Commands.Interface;
using CryptoBot.Commands.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace CryptoBot.Test
{
    [TestClass]
    public class CommandTestUnit
    {

        private class GeneralCommandModule : IModule
        {

            public string DummyString { get; set; }
            public string FakeDummyString { get; set; }

            [Command("dummy")]
            public void DummyCommand(string[] args)
            {
                DummyString = "Executed.";
            }

            [Command("fake-dummy")]
            public void FakeDummy(string[] args)
            {
                FakeDummyString = "Executed.";
            }

        }

        private class ArbitraryCommandModule : IModule
        {
            public int DummyInteger { get; set; }

            [Command("pimp-my-int")]
            public void ChangeInteger(string[] args)
            {
                DummyInteger = 420;
            }

        }

        /// <summary>
        /// What I expect from this test is to have the first dummy string set correctly to what the command sets it.
        /// I expect the second(fake) dummy string to be null or something else rather what its own command sets it to.
        /// If the test is true it means the command handler service executes only the command given and not every method having attribute "CommandAttribute".
        /// </summary>
        [TestMethod]
        public void UseExecuteSingleModule()
        {
            var service = new CommandHandlerService();
            var cmdModule = new GeneralCommandModule();
            service.RegisterModule(cmdModule);
            service.ExecuteCommandAsync("dummy", new string[0]).Wait();
            
            Assert.AreEqual("Executed.", cmdModule.DummyString);
            Assert.AreNotEqual("Executed.", cmdModule.FakeDummyString);
        }
        
        [TestMethod]
        public void UseExecuteMultipleModules()
        {
            var service = new CommandHandlerService();
            var generalCmdModule = new GeneralCommandModule();
            var arbitraryCmdModule = new ArbitraryCommandModule();
            service.RegisterModule(generalCmdModule);
            service.RegisterModule(arbitraryCmdModule);
            service.ExecuteCommandAsync("dummy").Wait();
            service.ExecuteCommandAsync("pimp-my-int").Wait();

            Assert.AreEqual("Executed.", generalCmdModule.DummyString);
            Assert.AreEqual(420, arbitraryCmdModule.DummyInteger);
        }

    }
}
