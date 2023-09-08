using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using TerminalToDoList.Interfaces.DataProviders;
using TerminalToDoList.Interfaces.Logger;
using TerminalToDoList.Logger;
using static TerminalToDoList.Models.TerminalToDoListConstants;

namespace TerminalToDoList.DataProviders
{
    /// <inheritdoc cref="ITerminalToDoListDataProvider"/>
    public class TerminalToDoListDataProvider : ITerminalToDoListDataProvider
    {
        private readonly ILogger _logger;

        private const string ConnectionString = "Data Source=notes.db;Version=3;";

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
                    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
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

        public void AddNote(string message)
        {
            using SQLiteConnection connection = new(ConnectionString);
            connection.Open();

            string insertNoteQuery = "INSERT INTO Notes (Content) VALUES (@content);";

            using SQLiteCommand insertNoteCommand = new (insertNoteQuery, connection);
            insertNoteCommand.Parameters.AddWithValue("@content", message);
            insertNoteCommand.ExecuteNonQuery();
        }

        public List<string> ShowNote(int idNote = 0)
        {
            var result = new List<string>();

            using SQLiteConnection connection = new(ConnectionString);
            connection.Open();

            string selectNotesQuery = idNote == 0 ? "SELECT * FROM Notes;" : $"SELECT * FROM Notes WHERE Id={idNote};";

            using SQLiteCommand selectNotesCommand = new(selectNotesQuery, connection);
            using SQLiteDataReader reader = selectNotesCommand.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string content = reader.GetString(1);
                DateTime createdAt = reader.GetDateTime(2);

                result.Add($"ID: {id}, Contenuto: {content}, Creato il: {createdAt}");
            }

            return result;

        }
    }
}