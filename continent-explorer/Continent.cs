using System;
using System.Collections.Generic;

namespace continent_explorer
{
    public class Continent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string AudioPath { get; set; }
        public (double X, double Y, double Z) RotationAngles { get; set; }
        public List<Animal> Animals { get; set; } = new List<Animal>();
        public List<Landmark> Landmarks { get; set; } = new List<Landmark>();
        public List<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();

        public Continent(string name, string description, string imagePath, string audioPath,
                         (double X, double Y, double Z) rotationAngles)
        {
            Name = name;
            Description = description;
            ImagePath = imagePath;
            AudioPath = audioPath;
            RotationAngles = rotationAngles;
        }
    }

    public class Animal
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePrompt { get; set; } 

        public Animal(string name, string description, string imagePrompt)
        {
            Name = name;
            Description = description;
            ImagePrompt = imagePrompt;
        }
    }

    public class Landmark
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePrompt { get; set; } 

        public Landmark(string name, string description, string imagePrompt)
        {
            Name = name;
            Description = description;
            ImagePrompt = imagePrompt;
        }
    }

    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectAnswerIndex { get; set; }
        public string Explanation { get; set; }

        public QuizQuestion(string question, List<string> options, int correctAnswerIndex, string explanation)
        {
            Question = question;
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
            Explanation = explanation;
        }
    }

    public class QuizResult
    {
        public string ContinentName { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public DateTime CompletionTime { get; set; }

        public QuizResult(string continentName, int score, int totalQuestions)
        {
            ContinentName = continentName;
            Score = score;
            TotalQuestions = totalQuestions;
            CompletionTime = DateTime.Now;
        }
    }
}