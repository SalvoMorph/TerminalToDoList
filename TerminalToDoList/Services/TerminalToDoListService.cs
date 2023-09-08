using TerminalToDoList.DataProviders;
using TerminalToDoList.Interfaces.DataProviders;
using TerminalToDoList.Interfaces.Logger;
using TerminalToDoList.Interfaces.Services;
using TerminalToDoList.Logger;
using static TerminalToDoList.Models.TerminalToDoListConstants;

namespace TerminalToDoList.Services
{
    /// <inheritdoc cref="ITerminalToDoListService"/>
	internal class TerminalToDoListService : ITerminalToDoListService
    {
        private readonly ILogger _logger;
        private readonly ITerminalToDoListDataProvider _terminalToDoListDataProvider;
        static List<string> tasks = new();

        #region Ctor

        /// <summary>
        /// Ctor of <see cref="TerminalToDoListService"/>
        /// </summary>
        public TerminalToDoListService()
        {
            _logger = new ConsoleLogger();
            _terminalToDoListDataProvider = new TerminalToDoListDataProvider();
        }

        /// <summary>
        /// Ctor of <see cref="TerminalToDoListService"/>
        /// </summary>
        /// <param name="logger">The Logger Interface.</param>
        /// <param name="terminalToDoListDataProvider">The TerminalToDoListDataProvider Interface.</param>
        public TerminalToDoListService(ILogger logger, ITerminalToDoListDataProvider terminalToDoListDataProvider)
		{
            _logger = logger ?? new ConsoleLogger();
            _terminalToDoListDataProvider = terminalToDoListDataProvider;
		}

        #endregion

        /// <inheritdoc cref="ITerminalToDoListService.AddNote(string)"/>
        public void AddNote(string message)
        {
            if (string.IsNullOrEmpty(message))
            {

                _logger.Log(LogLevel.Info, "Add new activity: ");

                message = Console.ReadLine();
            }

            _terminalToDoListDataProvider.AddNote(message);

            _logger.Log(LogLevel.Info, "done!");
            _logger.Log(LogLevel.Info, "...");
            Console.ReadLine();
        }

        /// <inheritdoc cref="ITerminalToDoListService.CompleteActivity(int)"/>
        public void CompleteActivity(int idNote = 0)
        {
            if (idNote == 0)
            {
                _logger.Log(LogLevel.Info, "Add activity number: ");
                _ = int.TryParse(Console.ReadLine(), out idNote);
            }

            if (idNote >= 1 && idNote <= tasks.Count)
            {
                tasks.RemoveAt(idNote - 1);
                _logger.Log(LogLevel.Info, "Activity marked as completed!");
                _logger.Log(LogLevel.Info, "...");
                Console.ReadLine();
                return;
            }

            _logger.Log(LogLevel.Warning, "Activity not present!");
            _logger.Log(LogLevel.Info, "...");
            Console.ReadLine();
        }

        /// <inheritdoc cref="ITerminalToDoListService.DeleteNote(int)"/>
        public void DeleteNote(int idNote = 0)
        {
            if(idNote == 0)
            {
                _logger.Log(LogLevel.Info, "Add activity number to delete: ");
                _ = int.TryParse(Console.ReadLine(), out idNote);
            }

            if (idNote >= 1 && idNote <= tasks.Count)
            {
                tasks.RemoveAt(idNote - 1);
                _logger.Log(LogLevel.Info, "Activity deleted!");
                _logger.Log(LogLevel.Info, "...");
                Console.ReadLine();
                return;
            }

            _logger.Log(LogLevel.Warning, "Activity not present!");
            _logger.Log(LogLevel.Info, "...");
            Console.ReadLine();
        }

        /// <inheritdoc cref="ITerminalToDoListService.ViewActivities(int)"/>
        public void ViewActivities(int idNote = 0)
        {
            var activities = _terminalToDoListDataProvider.ShowNote(idNote);

            foreach (var activity in activities)
            {
                _logger.Log(LogLevel.Info, activity);
            }

            _logger.Log(LogLevel.Info, "...");
            Console.ReadLine();
        }

    }
}

