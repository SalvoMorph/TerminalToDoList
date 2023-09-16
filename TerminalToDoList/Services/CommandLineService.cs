using TerminalToDoList.Interfaces.Logger;
using TerminalToDoList.Interfaces.Services;
using static TerminalToDoList.Models.TerminalToDoListConstants;
using TerminalToDoList.Models;
using TerminalToDoList.Logger;

namespace TerminalToDoList.Services
{
    /// <inheritdoc cref="ICommandLineService"/>
    public class CommandLineService : ICommandLineService
    {
        private readonly ILogger _logger;
        private readonly IUserChoiceService _userChoiceService;

        #region Ctor

        /// <summary>
        /// Ctor of <see cref="CommandLineService"/>.
        /// </summary>
        public CommandLineService()
		{
            _logger = new ConsoleLogger();
            _userChoiceService = new UserChoiceService();
		}

        /// <summary>
        /// Ctor of <see cref="CommandLineService"/>.
        /// </summary>
        /// <param name="logger">The Logger Interface.</param>
        /// <param name="userChoiceService">The UserChoiceService Interface.</param>
        public CommandLineService(ILogger logger, IUserChoiceService userChoiceService)
        {
            _logger = logger;
            _userChoiceService = userChoiceService;
        }

        #endregion

        /// <inheritdoc cref="ICommandLineService.CallProperService(TerminalCmdLineArgument)"/>
        public void CallProperService(TerminalCmdLineArgument argument)
		{
            var action = _userChoiceService.GetProperTerminalServiceMethod(argument);

            if (action != null)
#pragma warning disable CS8604 // Possibile argomento di riferimento Null.
                action.Invoke(obj: argument.CmdLineValue);
#pragma warning restore CS8604 // Possibile argomento di riferimento Null.
            else
                _logger.Log(LogLevel.Info, "Goodbye");
        }
	}
}