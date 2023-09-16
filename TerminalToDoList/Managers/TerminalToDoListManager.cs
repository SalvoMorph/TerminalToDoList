using TerminalToDoList.Interfaces.Factory;
using TerminalToDoList.Interfaces.Managers;
using TerminalToDoList.Interfaces.Services;
using TerminalToDoList.Services;

namespace TerminalToDoList.Managers
{
    /// <inheritdoc cref="ITerminalToDoListManager"/>
    internal class TerminalToDoListManager : ITerminalToDoListManager
    {
        private readonly IUserInterfaceService _userInterfaceService;
        private readonly ITerminalCmdLineArgumentFactory _terminalCmdLineArgumentFactory;
        private readonly ICommandLineService _commandLineService;

        #region Ctor

        /// <summary>
        /// Ctor of <see cref="TerminalToDoListManager"/>.
        /// </summary>
        public TerminalToDoListManager()
        {
            _userInterfaceService = new UserInterfaceService();
            _terminalCmdLineArgumentFactory = new TerminalCmdLineArgumentFactory();
            _commandLineService = new CommandLineService();
        }

        /// <summary>
        /// Ctor of <see cref="TerminalToDoListManager"/>.
        /// </summary>
        /// <param name="userInterfaceService">The UserInterfaceService interface.</param>
        /// <param name="terminalCmdLineArgumentFactory">The TerminalCmdLineArgumentFactory Interface.</param>
        /// <param name="commandLineService">The CommandLineService Interface.</param>
        public TerminalToDoListManager(
            IUserInterfaceService userInterfaceService,
            ITerminalCmdLineArgumentFactory terminalCmdLineArgumentFactory,
            ICommandLineService commandLineService)
        {
            _userInterfaceService = userInterfaceService;
            _terminalCmdLineArgumentFactory = terminalCmdLineArgumentFactory;
            _commandLineService = commandLineService;
        }

        #endregion

        /// <inheritdoc cref="ITerminalToDoListManager.Start(string[])"/>
        public void Start(string[] args)
        {
            if (args.Length == 0)
            {
                _userInterfaceService.ShowVisualInterface();
                return;
            }

            var argument = _terminalCmdLineArgumentFactory.Create(args);

            if (argument != null)
            {
                _commandLineService.CallProperService(argument);
            }
        }
    }
}