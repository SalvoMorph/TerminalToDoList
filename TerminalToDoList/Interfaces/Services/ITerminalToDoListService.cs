namespace TerminalToDoList.Interfaces.Services
{
	/// <summary>
	/// The TerminalToDoList Service.
	/// </summary>
	public interface ITerminalToDoListService
	{
		void AddNote(string message);

		void DeleteNote(int idNote = 0);

		void ViewActivities(int idNote = 0);

		void CompleteActivity(int idNote = 0);
	}
}

