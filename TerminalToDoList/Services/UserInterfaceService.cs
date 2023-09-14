using TerminalToDoList.Interfaces.Logger;
using TerminalToDoList.Interfaces.Services;
using TerminalToDoList.Logger;
using static TerminalToDoList.Models.TerminalToDoListConstants;

namespace TerminalToDoList.Services
{
    /// <inheritdoc cref="IUserInterfaceService"/>
    public class UserInterfaceService : IUserInterfaceService
    {
        private readonly ILogger _logger;
        private readonly ITerminalToDoListService _terminalToDoListService;

        #region Ctor

        /// <summary>
        /// Ctor of <see cref="UserInterfaceService"/>.
        /// </summary>
        public UserInterfaceService()
        {
            _logger = new ConsoleLogger();
            _terminalToDoListService = new TerminalToDoListService();
        }

        /// <summary>
        /// Ctor of <see cref="UserInterfaceService"/>.
        /// </summary>
        /// <param name="logger">The Logger Interface.</param>
        /// <param name="terminalToDoListService">The TerminalToDoListService Interface.</param>
        public UserInterfaceService(ILogger logger, ITerminalToDoListService terminalToDoListService)
        {
            _logger = logger;
            _terminalToDoListService = terminalToDoListService;
        }

        #endregion

        /// <inheritdoc cref="IUserInterfaceService.ShowVisualInterface"/>
        public void ShowVisualInterface()
        {
            while (true)
            {
                _logger.ShowMenu();

                if (Enum.TryParse(Console.ReadLine(), out UserChoice choice))
                {
                    int idNote;

                    switch (choice)
                    {
                        case UserChoice.Add:
                            _logger.Log(LogLevel.Info, "Add new activity: ");
                            string message = Console.ReadLine();
                            _terminalToDoListService.AddNote(message);
                            break;

                        case UserChoice.View:
                            idNote = ReadNoteIdFromConsole("Id note to show: ");
                            _terminalToDoListService.ViewNote(idNote);
                            break;

                        case UserChoice.ViewAll:
                            _terminalToDoListService.ViewAllNote();
                            break;

                        case UserChoice.ViewCompleted:
                            idNote = ReadNoteIdFromConsole("Id note to show: ");
                            _terminalToDoListService.ViewCompletedNote(idNote);
                            break;

                        case UserChoice.ViewAllCompleted:
                            _terminalToDoListService.ViewAllCompletedNote();
                            break;

                        case UserChoice.Complete: // Complete a note
                            idNote = ReadNoteIdFromConsole("Id note to complete: ");
                            _terminalToDoListService.CompleteNote(idNote);
                            break;

                        case UserChoice.Delete:
                            idNote = ReadNoteIdFromConsole("Id note to delete: ");
                            _terminalToDoListService.DeleteNote(idNote);
                            break;

                        case UserChoice.Exit: // Exit
                        default:
                            _logger.Log(LogLevel.Info, "Goodbye!");
                            return;
                    }
                }
                else
                {
                    _logger.Log(LogLevel.Warning, "Invalid choice.");
                }
            }
        }

        private int ReadNoteIdFromConsole(string message)
        {
            _logger.Log(LogLevel.Info, message);
            _ = int.TryParse(Console.ReadLine(), out var idNote);

            return idNote;
        }

    }
}