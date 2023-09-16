using System.Data.SQLite;
using TerminalToDoList.Interfaces.DataProviders;
using TerminalToDoList.Interfaces.Logger;
using TerminalToDoList.Logger;
using TerminalToDoList.Models;
using static TerminalToDoList.Models.TerminalToDoListConstants;

namespace TerminalToDoList.DataProviders
{
    /// <inheritdoc cref="ITerminalToDoListDataProvider"/>
    public class TerminalToDoListDataProvider : ITerminalToDoListDataProvider
    {
        private readonly ILogger _logger;

        private const string ConnectionString = "Data Source=notes.db;Version=3;";

        /// <summary>
        /// Ctor of <see cref="TerminalToDoListDataProvider"/>
        /// </summary>
        public TerminalToDoListDataProvider() 
		{
            _logger = new ConsoleLogger();

            try
            {
                using SQLiteConnection connection = new(ConnectionString);
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Notes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Content TEXT NOT NULL,
                    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                    CompletedAt DATETIME NULL
                );";

                using SQLiteCommand createTableCommand = new(createTableQuery, connection);
                createTableCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
            }
        }

        /// <inheritdoc cref="ITerminalToDoListDataProvider.AddNote(string)"/>
        public void AddNote(string message)
        {
            using SQLiteConnection connection = new(ConnectionString);
            connection.Open();

            string insertNoteQuery = "INSERT INTO Notes (Content) VALUES (@content);";

            using SQLiteCommand insertNoteCommand = new (insertNoteQuery, connection);
            insertNoteCommand.Parameters.AddWithValue("@content", message);
            insertNoteCommand.ExecuteNonQuery();
        }

        /// <inheritdoc cref="ITerminalToDoListDataProvider.DeleteNote(int)"/>
        public bool DeleteNote(int idNote)
        {
            using SQLiteConnection connection = new(ConnectionString);
            connection.Open();

            string deleteNoteQuery = $"DELETE * FROM Notes WHERE Id = {idNote};";
            using SQLiteCommand selectNotesCommand = new(deleteNoteQuery, connection);
            var result = selectNotesCommand.ExecuteNonQuery();

            return result != 0;
        }

        /// <inheritdoc cref="ITerminalToDoListDataProvider.CompleteNote(int)"/>
        public void CompleteNote(int idNote)
        {
            throw new NotImplementedException();
        }

        #region Show

        /// <inheritdoc cref="ITerminalToDoListDataProvider.ShowNote(int)"/>
        public List<Note> ShowNote(int idNote)
        {
            string selectNotesQuery = $"SELECT * FROM Notes WHERE CompletedAt IS NULL AND Id = {idNote};";
            return GetNotes(selectNotesQuery);
        }

        /// <inheritdoc cref="ITerminalToDoListDataProvider.ShowAllNotes()"/>
        public List<Note> ShowAllNotes()
        {
            string selectNotesQuery = "SELECT * FROM Notes WHERE CompletedAt IS NULL;";
            return GetNotes(selectNotesQuery);
        }

        /// <inheritdoc cref="ITerminalToDoListDataProvider.ShowCompletedNote(int)"/>
        public List<Note> ShowCompletedNote(int idNote)
        {
            string selectNotesQuery = $"SELECT * FROM Notes WHERE CompletedAt IS NOT NULL AND Id = {idNote}";
            return GetNotes(selectNotesQuery);
        }

        /// <inheritdoc cref="ITerminalToDoListDataProvider.ShowAllCompletedNote()"/>
        public List<Note> ShowAllCompletedNote()
        {
            string selectNotesQuery = "SELECT * FROM Notes WHERE CompletedAt IS NOT NULL;";
            return GetNotes(selectNotesQuery);
        }

        #endregion



        private static List<Note> GetNotes(string query)
        {
            var result = new List<Note>();

            using SQLiteConnection connection = new(ConnectionString);
            connection.Open();

            using SQLiteCommand selectNotesCommand = new(query, connection);
            using SQLiteDataReader reader = selectNotesCommand.ExecuteReader();

            while (reader.Read())
            {
                Note note = new(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2));
                result.Add(note);
            }

            return result;
        }
    }
}