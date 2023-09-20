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

        #region Ctor

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
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
            }
        }

        #endregion

        /// <inheritdoc cref="ITerminalToDoListDataProvider.AddNote(string)"/>
        public int AddNote(string message)
        {
            try
            {

                using SQLiteConnection connection = new(ConnectionString);
                connection.Open();

                string insertNoteQuery = "INSERT INTO Notes (Content, CompletedAt) VALUES (@content, @CompletedAt); SELECT MAX(Id) AS LASTID FROM Notes;";

                using SQLiteCommand insertNoteCommand = new(insertNoteQuery, connection);
                insertNoteCommand.Parameters.AddWithValue("@content", message);
                insertNoteCommand.Parameters.AddWithValue("@CompletedAt", DateTime.MinValue);
                var reader = insertNoteCommand.ExecuteReader();

                reader.Read();
                int lastId = reader.GetInt32(0);
                reader.Close();

                return lastId;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }
            return -1;
        }

        #region Delete

        /// <inheritdoc cref="ITerminalToDoListDataProvider.DeleteNote(int)"/>
        public bool DeleteNote(int idNote)
        {
            string deleteNoteQuery = $"DELETE FROM Notes WHERE Id = {idNote};";
            return ExecuteNonQuery(deleteNoteQuery);
        }

        /// <inheritdoc cref="ITerminalToDoListDataProvider.DeleteAllNotes()"/>
        public bool DeleteAllNotes()
        {
            string deleteNoteQuery = $"DELETE FROM Notes;";
            var result = ExecuteNonQuery(deleteNoteQuery);
            _ = ExecuteNonQuery("UPDATE sqlite_sequence SET seq = 0 WHERE name=\"Notes\"");

            return result;
        }

        #endregion

        #region Complete Note

        /// <inheritdoc cref="ITerminalToDoListDataProvider.CompleteNote(int)"/>
        public bool CompleteNote(int idNote)
        {
            var query = "UPDATE Notes SET CompletedAt = @CompletedAt WHERE Id =@IdNote and CompletedAt = @MinDate ;";
            Dictionary<string, object> commandParams = new()
            {
                { "@CompletedAt", DateTime.UtcNow },
                { "@IdNote", idNote },
                { "@MinDate", DateTime.MinValue },
            };

            return ExecuteNonQuery(query, commandParams);
        }

        #endregion

        #region Show

        /// <inheritdoc cref="ITerminalToDoListDataProvider.ShowNote(int)"/>
        public List<Note> ShowNote(int idNote)
        {
            string selectNotesQuery = "SELECT * FROM Notes WHERE CompletedAt = @CompletedAt AND Id =@IdNote;";
            Dictionary<string, object> commandParams = new()
            {
                { "@CompletedAt", DateTime.MinValue },
                { "@IdNote", idNote }
            };
            return GetNotes(selectNotesQuery, commandParams);
        }

        /// <inheritdoc cref="ITerminalToDoListDataProvider.ShowAllNotes()"/>
        public List<Note> ShowAllNotes()
        {
            string selectNotesQuery = "SELECT * FROM Notes WHERE CompletedAt = @CompletedAt;";
            Dictionary<string, object> commandParams = new()
            {
                { "@CompletedAt", DateTime.MinValue },
            };
            return GetNotes(selectNotesQuery, commandParams);
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



        private List<Note> GetNotes(string query, Dictionary<string, object>? commandParams = null)
        {
            var result = new List<Note>();

            try
            {
                using SQLiteConnection connection = new(ConnectionString);
                connection.Open();

                using SQLiteCommand selectNotesCommand = new(query, connection);
                if (commandParams != null)
                {
                    foreach (var param in commandParams)
                    {
                        selectNotesCommand.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                using SQLiteDataReader reader = selectNotesCommand.ExecuteReader();

                if (!reader.HasRows)
                    return result;

                while (reader.Read())
                {
                    Note note = new(reader.GetInt32(0),
                                    reader.GetString(1),
                                    reader.GetDateTime(2),
                                    reader.GetDateTime(3));
                    result.Add(note);
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }

            return result;
        }

        private bool ExecuteNonQuery(string query, Dictionary<string, object>? commandParams = null)
        {
            try
            {
                using SQLiteConnection connection = new(ConnectionString);
                connection.Open();

                using SQLiteCommand queryNotesCommand = new(query, connection);

                if (commandParams != null)
                {
                    foreach (var param in commandParams)
                    {
                        queryNotesCommand.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                var result = queryNotesCommand.ExecuteNonQuery();

                return result != 0;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }

            return false;
        }
    }
}