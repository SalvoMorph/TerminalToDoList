using System;
namespace TerminalToDoList.Models
{
    /// <summary>
    /// A note.
    /// </summary>
    public class Note
	{
        /// <summary>
        /// The note ID.
        /// </summary>
        public int IdNote { get; set; }

        /// <summary>
        /// The message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The Creation date.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Completion date.
        /// </summary>
        public DateTime CompletedDate { get; set; }

        /// <summary>
        /// Ctor of <see cref="Note"/>.
        /// </summary>
        /// <param name="idNote">The note id.</param>
        /// <param name="message">The message.</param>
        /// <param name="creationDate">The creation date.</param>
        public Note(int idNote, string message, DateTime creationDate)
        {
            IdNote = idNote;
            Message = message;
            CreationDate = creationDate;
        }
	}
}