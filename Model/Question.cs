using MongoDB.Bson.Serialization.Attributes;

namespace TinasLabb03.Model
{
    // Representerar en fråga med svarsalternativ.

    public class Question
    {
        [BsonElement("question")]
        public string Query { get; set; }

        [BsonElement("correct_answer")]
        public string CorrectAnswer { get; set; }

        [BsonElement("incorrect_answers")]
        public string[] IncorrectAnswers { get; set; }

        [BsonElement("difficulty")]
        public Difficulty Difficulty { get; set; }

        // Konstruktor med parametrar
        public Question(string query, string correctAnswer, string[] incorrectAnswers, Difficulty difficulty)
        {
            Query = query;
            CorrectAnswer = correctAnswer;
            IncorrectAnswers = incorrectAnswers;
            Difficulty = difficulty;
        }

        // Parameterlös konstruktor med defaultvärden (används vid deserialisering)
        public Question()
        {
            Query = string.Empty;
            CorrectAnswer = string.Empty;
            IncorrectAnswers = new[] { string.Empty, string.Empty, string.Empty };
            Difficulty = Difficulty.Medium;
        }
    }
}