using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Reflection;

namespace continent_explorer
{
    public partial class ContinentDetailWindow : Window
    {
        private Continent continent;
        private MediaPlayer audioPlayer;
        private int currentQuizIndex = 0;
        private int quizScore = 0;
        private bool quizCompleted = false;

        public ContinentDetailWindow(Continent selectedContinent)
        {
            InitializeComponent();
            continent = selectedContinent;
            audioPlayer = new MediaPlayer();

            LoadContinentData();
        }

        private void LoadContinentData()
        {
            // Set window title
            this.Title = $"Exploring {continent.Name}";

            // Set continent name and description
            continentNameText.Text = continent.Name.ToUpper();
            descriptionText.Text = continent.Description;

            // Load animals and landmarks
            LoadAnimals();
            LoadLandmarks();

            // Play audio if available
            if (!string.IsNullOrEmpty(continent.AudioPath) && File.Exists(continent.AudioPath))
            {
                try
                {
                    audioPlayer.Open(new Uri(continent.AudioPath, UriKind.Absolute));
                    audioPlayer.Volume = 0.3; // Set volume to 30%
                    audioPlayer.MediaEnded += (s, e) => audioPlayer.Position = TimeSpan.Zero; // Loop audio
                    audioPlayer.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error playing audio: {ex.Message}", "Audio Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            // Initialize quiz
            if (continent.QuizQuestions.Count > 0)
            {
                InitializeQuiz();
            }
            else
            {
                quizPanel.Visibility = Visibility.Collapsed;
                noQuizText.Visibility = Visibility.Visible;
            }
        }

        private void LoadAnimals()
        {
            animalsPanel.Children.Clear();

            if (continent.Animals.Count == 0)
            {
                TextBlock noAnimalsText = new TextBlock
                {
                    Text = "No animals information available for this continent.",
                    Foreground = Brushes.White,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(10)
                };
                animalsPanel.Children.Add(noAnimalsText);
                return;
            }

            foreach (var animal in continent.Animals)
            {
                Border animalCard = CreateInfoCard(animal.Name, animal.Description);
                animalsPanel.Children.Add(animalCard);
            }
        }

        private void LoadLandmarks()
        {
            landmarksPanel.Children.Clear();

            if (continent.Landmarks.Count == 0)
            {
                TextBlock noLandmarksText = new TextBlock
                {
                    Text = "No landmarks information available for this continent.",
                    Foreground = Brushes.White,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(10)
                };
                landmarksPanel.Children.Add(noLandmarksText);
                return;
            }

            foreach (var landmark in continent.Landmarks)
            {
                Border landmarkCard = CreateInfoCard(landmark.Name, landmark.Description);
                landmarksPanel.Children.Add(landmarkCard);
            }
        }

        private Border CreateInfoCard(string title, string description)
        {
            // Create card container
            Border card = new Border
            {
                Background = new SolidColorBrush(Color.FromArgb(100, 28, 63, 96)),
                BorderBrush = new SolidColorBrush(Color.FromArgb(255, 78, 143, 194)),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(8),
                Margin = new Thickness(0, 0, 0, 15),
                Padding = new Thickness(10)
            };

            // Create layout for card content
            Grid cardGrid = new Grid();
            cardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            cardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Add title
            TextBlock titleText = new TextBlock
            {
                Text = title,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                Margin = new Thickness(0, 0, 0, 5)
            };
            Grid.SetRow(titleText, 0);
            cardGrid.Children.Add(titleText);

            // Add description
            TextBlock descText = new TextBlock
            {
                Text = description,
                TextWrapping = TextWrapping.Wrap,
                Foreground = Brushes.White,
                Margin = new Thickness(0, 0, 0, 10)
            };
            Grid.SetRow(descText, 1);
            cardGrid.Children.Add(descText);

            // Set the grid as the card's child
            card.Child = cardGrid;

            return card;
        }

        private void InitializeQuiz()
        {
            currentQuizIndex = 0;
            quizScore = 0;
            quizCompleted = false;

            // Show quiz panel and hide results
            quizPanel.Visibility = Visibility.Visible;
            quizResultsPanel.Visibility = Visibility.Collapsed;

            // Display first question
            DisplayCurrentQuestion();
        }

        private void DisplayCurrentQuestion()
        {
            if (currentQuizIndex >= continent.QuizQuestions.Count)
            {
                ShowQuizResults();
                return;
            }

            var question = continent.QuizQuestions[currentQuizIndex];

            // Update question text and progress
            questionText.Text = question.Question;
            questionProgressText.Text = $"Question {currentQuizIndex + 1} of {continent.QuizQuestions.Count}";

            // Clear previous options
            optionsPanel.Children.Clear();

            // Add radio buttons for each option
            for (int i = 0; i < question.Options.Count; i++)
            {
                RadioButton option = new RadioButton
                {
                    Content = question.Options[i],
                    Margin = new Thickness(0, 5, 0, 5),
                    Tag = i,
                    GroupName = "QuizOptions",
                    Foreground = Brushes.White,
                    FontSize = 14
                };
                optionsPanel.Children.Add(option);
            }

            // Reset explanation
            explanationText.Text = "";
            explanationPanel.Visibility = Visibility.Collapsed;

            // Update buttons
            submitAnswerButton.IsEnabled = true;
            nextQuestionButton.IsEnabled = false;
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            // Find selected option
            int selectedIndex = -1;

            foreach (var child in optionsPanel.Children)
            {
                if (child is RadioButton rb && rb.IsChecked == true)
                {
                    selectedIndex = (int)rb.Tag;
                    break;
                }
            }

            if (selectedIndex == -1)
            {
                MessageBox.Show("Please select an answer.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var question = continent.QuizQuestions[currentQuizIndex];
            bool isCorrect = selectedIndex == question.CorrectAnswerIndex;

            // Update score if correct
            if (isCorrect)
            {
                quizScore++;
            }

            // Show explanation
            explanationText.Text = question.Explanation;
            explanationPanel.Visibility = Visibility.Visible;

            // Highlight correct and incorrect answers
            foreach (var child in optionsPanel.Children)
            {
                if (child is RadioButton rb)
                {
                    int optionIndex = (int)rb.Tag;

                    if (optionIndex == question.CorrectAnswerIndex)
                    {
                        rb.Foreground = Brushes.LightGreen;
                    }
                    else if (optionIndex == selectedIndex && !isCorrect)
                    {
                        rb.Foreground = Brushes.LightCoral;
                    }

                    rb.IsEnabled = false;
                }
            }

            // Update buttons
            submitAnswerButton.IsEnabled = false;
            nextQuestionButton.IsEnabled = true;
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            currentQuizIndex++;
            DisplayCurrentQuestion();
        }

        private void ShowQuizResults()
        {
            quizCompleted = true;

            // Hide quiz panel and show results
            quizPanel.Visibility = Visibility.Collapsed;
            quizResultsPanel.Visibility = Visibility.Visible;

            // Update results text
            resultScoreText.Text = $"Your Score: {quizScore} out of {continent.QuizQuestions.Count}";

            double percentage = (double)quizScore / continent.QuizQuestions.Count * 100;
            resultPercentageText.Text = $"{percentage:F1}%";

            // Set feedback based on score
            if (percentage >= 80)
            {
                resultFeedbackText.Text = "Excellent! You're an expert on this continent!";
            }
            else if (percentage >= 60)
            {
                resultFeedbackText.Text = "Good job! You know quite a bit about this continent.";
            }
            else if (percentage >= 40)
            {
                resultFeedbackText.Text = "Not bad, but there's room for improvement.";
            }
            else
            {
                resultFeedbackText.Text = "You might want to review the information about this continent.";
            }

            // Save quiz result
            SaveQuizResult();
        }

        private void SaveQuizResult()
        {
            try
            {
                QuizResult result = new QuizResult(continent.Name, quizScore, continent.QuizQuestions.Count);

                string resultsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QuizResults");
                Directory.CreateDirectory(resultsDir);

                string resultsFile = Path.Combine(resultsDir, "quiz_results.json");

                List<QuizResult> allResults = new List<QuizResult>();

                if (File.Exists(resultsFile))
                {
                    string json = File.ReadAllText(resultsFile);
                    allResults = JsonConvert.DeserializeObject<List<QuizResult>>(json) ?? new List<QuizResult>();
                }

                allResults.Add(result);

                string updatedJson = JsonConvert.SerializeObject(allResults, Formatting.Indented);
                File.WriteAllText(resultsFile, updatedJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving quiz results: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RestartQuiz_Click(object sender, RoutedEventArgs e)
        {
            InitializeQuiz();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Stop audio when navigating away
            if (audioPlayer != null)
            {
                audioPlayer.Stop();
                audioPlayer.Close();
            }

            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            // Ensure audio is stopped when window is closed
            if (audioPlayer != null)
            {
                audioPlayer.Stop();
                audioPlayer.Close();
            }

            base.OnClosed(e);
        }
    }
}