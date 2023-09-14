namespace TerminalToDoList.Interfaces.Services
{
	/// <summary>
	/// The TerminalToDoList Service.
	/// </summary>
	public interface ITerminalToDoListService
	{
		/// <summary>
		/// Add a note.
		/// </summary>
		/// <param name="message">The message.</param>
		void AddNote(string message);

		/// <summary>
		/// Delete a note.
		/// </summary>
		/// <param name="idNote">The Note ID.</param>
		void DeleteNote(int idNote);

        /// <summary>
        /// Show the Notes.
        /// </summary>
        /// <param name="idNote">The Note Id.</param>
        void ViewNote(int idNote);

		/// <summary>
		/// Show all the notes.
		/// </summary>
		void ViewAllNote();

        /// <summary>
        /// Show the completed note.
        /// </summary>
        /// /// <param name="idNote">The Note Id.</param>
        void ViewCompletedNote(int idNote);

        /// <summary>
        /// Show alle the completed note.
        /// </summary>
        void ViewAllCompletedNote();

        /// <summary>
        /// Complete a note.
        /// </summary>
        /// <param name="idNote">The Note ID. If 0, the application ask to digit it.</param>
        void CompleteNote(int idNote);
	}
}