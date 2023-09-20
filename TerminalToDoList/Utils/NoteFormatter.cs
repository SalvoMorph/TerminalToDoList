using System.Text;
using TerminalToDoList.Models;

namespace TerminalToDoList.Utils
{
    /// <summary>
    /// The formatter used to show the note.
    /// </summary>
	public class NoteFormatter
    {
        /// <summary>
        /// Format the note.
        /// </summary>
        /// <param name="note">The <see cref="Note"/>.</param>
        /// <returns>The formatted string.</returns>
        public static string Format(Note note)
        {
            var sb = new StringBuilder();
            sb.AppendLine("------------------------------------");
            sb.AppendLine("Text: ");
            sb.AppendLine($"  {note.Message}");
            sb.AppendLine();
            sb.AppendLine($"Note id: {note.IdNote}");
            sb.AppendLine($"Created at: {note.CreationDate}");

            if (note.IsCompleted)
                sb.AppendLine($"Completed at: {note.CompletedDate}");

            sb.AppendLine("------------------------------------");
            return sb.ToString();
        }
    }
}