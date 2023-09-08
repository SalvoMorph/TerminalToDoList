using TerminalToDoList.Interfaces.Logger;
using TerminalToDoList.Interfaces.Managers;
using TerminalToDoList.Interfaces.Services;
using TerminalToDoList.Logger;
using TerminalToDoList.Services;
using static TerminalToDoList.Models.TerminalToDoListConstants;

namespace TerminalToDoList.Managers
{
    /// <inheritdoc cref="ITerminalToDoListManager"/>
    internal class TerminalToDoListManager : ITerminalToDoListManager
	{
        private readonly ILogger _logger;
        private readonly ITerminalToDoListService _terminalToDoListService;

        /// <summary>
        /// Ctor of <see cref="TerminalToDoListManager"/>.
        /// </summary>
        /// <param name="logger">The Logger interface.</param>
        public TerminalToDoListManager (ILogger logger)
		{
			_logger = logger ?? new ConsoleLogger();
            _terminalToDoListService = new TerminalToDoListService(_logger);
        }

        public void Start(string[] args)
        {
            if (args.Count() == 0)
            {
                while (true)
                {
                    _logger.ShowMenu();

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            _terminalToDoListService.AddNote(string.Empty);
                            break;

                        case "2":
                            _terminalToDoListService.ViewActivities();
                            break;

                        case "3":
                            _terminalToDoListService.CompleteActivity();
                            break;

                        case "4":
                            _terminalToDoListService.DeleteNote();
                            break;

                        case "5":
                            _logger.Log(LogLevel.Info, "Goodbye!");
                            return;

                        default:
                            _logger.Log(LogLevel.Info, "Goodbye!");
                            break;
                    }
                }
            }

            // Leggi args e chiama il servizio giusto
        }

	}
}

