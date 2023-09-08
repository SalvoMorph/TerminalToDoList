using System;
namespace TerminalToDoList.Interfaces.DataProviders
{
	/// <summary>
	/// The TerminalToDoList Data provider
	/// </summary>
	public interface ITerminalToDoListDataProvider
	{
		void AddNote(string message);

        List<string> ShowNote(int idNote = 0);

    }
}

