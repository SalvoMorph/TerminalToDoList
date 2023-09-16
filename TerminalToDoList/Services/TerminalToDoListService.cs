using TerminalToDoList.DataProviders;
using TerminalToDoList.Interfaces.DataProviders;
using TerminalToDoList.Interfaces.Logger;
using TerminalToDoList.Interfaces.Services;
using TerminalToDoList.Logger;
using TerminalToDoList.Models;
using TerminalToDoList.Utils;
using static TerminalToDoList.Models.TerminalToDoListConstants;

namespace TerminalToDoList.Services
{
    /// <inheritdoc cref="ITerminalToDoListService"/>
	internal class TerminalToDoListService : ITerminalToDoListService
    {
        private readonly ILogger _logger;
        private readonly ITerminalToDoListDataProvider _terminalToDoListDataProvider;

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
            _terminalToDoListDataProvider.AddNote(message);

            _logger.Log(LogLevel.Info, "done!");
            _logger.Log(LogLevel.Info, "...");
            Console.ReadLine();
        }

        /// <inheritdoc cref="ITerminalToDoListService.CompleteNote(int)"/>
        public void CompleteNote(int idNote)
        {
            _terminalToDoListDataProvider.CompleteNote(idNote);

            _logger.Log(LogLevel.Info, "Note marked as completed!");
            _logger.Log(LogLevel.Info, "...");
            Console.ReadLine();
        }

        /// <inheritdoc cref="ITerminalToDoListService.DeleteNote(int)"/>
        public void DeleteNote(int idNote)
        {
            if (_terminalToDoListDataProvider.DeleteNote(idNote))
            {
                _logger.Log(LogLevel.Info, "Note deleted!");
                _logger.Log(LogLevel.Info, "...");
                Console.ReadLine();
                return;
            }
            _logger.Log(LogLevel.Info, $"Note {idNote} not present!");
        }

        /// <inheritdoc cref="ITerminalToDoListService.ViewNote(int)"/>
        public void ViewNote(int idNote)
        {
            var notes = _terminalToDoListDataProvider.ShowNote(idNote);
            PrintNote(notes);
        }

        /// <inheritdoc cref="ITerminalToDoListService.ViewNote(int)"/>
        public void ViewAllNote()
        {
            var notes = _terminalToDoListDataProvider.ShowAllNotes();
            PrintNote(notes);
        }

        /// <inheritdoc cref="ITerminalToDoListService.ViewCompletedNote(int)"/>
        public void ViewCompletedNote(int idNote)
        {
            var notes = _terminalToDoListDataProvider.ShowCompletedNote(idNote);
            PrintNote(notes);
        }

        /// <inheritdoc cref="ITerminalToDoListService.ViewAllCompletedNote()"/>
        public void ViewAllCompletedNote()
        {
            var notes = _terminalToDoListDataProvider.ShowAllCompletedNote();
            PrintNote(notes);
        }

        private void PrintNote(List<Note> notes)
        {
            if(notes == null || ! notes.Any())
            {
                _logger.Log(LogLevel.Info, "No note to show.");
                return;
            }

            foreach (var note in notes)
            {
                string formattedNote = NoteFormatter.Format(note);
                _logger.Log(LogLevel.Info, formattedNote);
            }

            _logger.Log(LogLevel.Info, "...");
            Console.ReadLine();
        }
    }
}