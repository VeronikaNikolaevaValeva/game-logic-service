﻿namespace GameLogicService.Models.Messaging
{
    public class UpdateUserScore
    {
        public string? EmailAddress { get; set; }
        public int? AmountAnsweredQuestions { get; set; }
        public int? AmountOfPlayedGames { get; set; }
        public int? CorrectAnswerCount { get; set; }
        public int? IncorrectAnswerCount { get; set; }
        public int? NonAnsweredCount { get; set; }
    }
}
