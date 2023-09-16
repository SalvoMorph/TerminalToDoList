using TerminalToDoList.Models;

namespace TerminalToDoList.Interfaces.DataProviders
{
	/// <summary>
	/// The TerminalToDoList Data provider
	/// </summary>
	public interface ITerminalToDoListDataProvider
	{
		/// <summary>
		/// Add a note in the database.
		/// </summary>
		/// <param name="message">The message to save.</param>
		void AddNote(string message);

		/// <summary>
		/// Show the saved notes.
		/// </summary>
		/// <param name="idNote">The note ID to show. If 0, will display all the notes.</param>
		/// <returns>A list of <see cref="Note"/>.</returns>
        List<Note> ShowNote(int idNote);

        /// <summary>
		/// Show the saved notes.
		/// </summary>
		/// <returns>A list of <see cref="Note"/>.</returns>
        List<Note> ShowAllNotes();

        /// <summary>
        /// Show the completed notes.
        /// </summary>
        /// <param name="idNote">The note ID to show.</param>
        /// <returns>A list of <see cref="Note"/>.</returns>
        List<Note> ShowCompletedNote(int idNote);

        /// <summary>
        /// Show all the completed notes.
        /// </summary>
        /// <returns>A list of <see cref="Note"/>.</returns>
        List<Note> ShowAllCompletedNote();

        /// <summary>
        /// Delete a note.
        /// </summary>
        /// <param name="idNote">The note ID to delete.</param>
        /// <returns>Return true if the note has been deleted.</returns>
        bool DeleteNote(int idNote);

        /// <summary>
        /// Complete a note.
        /// </summary>
        /// <param name="idNote">The note ID to marked as complete.</param>
        void CompleteNote(int idNote);
    }
}