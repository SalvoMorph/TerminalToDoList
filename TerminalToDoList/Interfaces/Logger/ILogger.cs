using static TerminalToDoList.Models.TerminalToDoListConstants;

namespace TerminalToDoList.Interfaces.Logger
{
	/// <summary>
	/// The Logger.
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// Print the message in the console.
		/// </summary>
		/// <param name="level">The <see cref="LogLevel"/>.</param>
		/// <param name="message">The message.</param>
		void Log(LogLevel level, string message);

        /// <summary>
        /// Show the menu.
        /// </summary>
        void ShowMenu();

		/// <summary>
		/// Show the Help menu.
		/// </summary>
		void ShowHelpMenu();
    }
}