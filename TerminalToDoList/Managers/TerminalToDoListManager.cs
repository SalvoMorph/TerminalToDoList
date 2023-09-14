using TerminalToDoList.Interfaces.Logger;
using TerminalToDoList.Interfaces.Managers;
using TerminalToDoList.Interfaces.Services;
using TerminalToDoList.Logger;
using TerminalToDoList.Services;

namespace TerminalToDoList.Managers
{
    /// <inheritdoc cref="ITerminalToDoListManager"/>
    internal class TerminalToDoListManager : ITerminalToDoListManager
	{
        private readonly ILogger _logger;
        private readonly IUserInterfaceService _userInterfaceService;

        #region Ctor

        /// <summary>
        /// Ctor of <see cref="TerminalToDoListManager"/>.
        /// </summary>
        public TerminalToDoListManager ()
		{
			_logger = new ConsoleLogger();
            _userInterfaceService = new UserInterfaceService();
        }

        /// <summary>
        /// Ctor of <see cref="TerminalToDoListManager"/>.
        /// </summary>
        /// <param name="logger">The Logger interface.</param>
        /// <param name="terminalToDoListService">The UserInterfaceService Interface.</param>
        public TerminalToDoListManager(ILogger logger, IUserInterfaceService userInterfaceService)
        {
            _logger = logger;
            _userInterfaceService = userInterfaceService;
        }

        #endregion

        public void Start(string[] args)
        {
            if (args.Count() == 0)
            {
                _userInterfaceService.ShowVisualInterface();
            }

            // Leggi args e chiama il servizio giusto
        }
	}
}

