using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;
using HelixToolkit.Wpf;
using System.IO;
using Newtonsoft.Json; // 1 Add  Newtonsoft
using System.Linq;
using System.Collections.ObjectModel; // To ObservalbeCollection

namespace continent_explorer
{
    public partial class GameWindow : Window
    {
        ObjReader objReader = new ObjReader();
        Model3DGroup model;
        Transform3DGroup transformGroup = new Transform3DGroup();
        QuaternionRotation3D currentRotation = new QuaternionRotation3D(new Quaternion(0, 0, 0, 1));
        RotateTransform3D rotateTransform;

        private ObservableCollection<Continent> continents = new ObservableCollection<Continent>(); // To ObservableCollection
        private Continent selectedContinent;

        public GameWindow()
        {
            InitializeComponent();
            LoadContinentsData();
            Load3DModel();
            LoadLabels();
        }

        private void LoadContinentsData()
        {
            try
            {
                string jsonFilePath = "C:\\Users\\Adi\\source\\repos\\continent-explorer\\Data\\continents.json";
                if (File.Exists(jsonFilePath))
                {
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    continents = JsonConvert.DeserializeObject<ObservableCollection<Continent>>(jsonContent); // Newtonsoft.Json
                }
                else
                {
                    MessageBox.Show("Continents data file not found. Using default data.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    LoadDefaultContinentsData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading continents data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                LoadDefaultContinentsData();
            }
        }

        private void LoadDefaultContinentsData()
        {
            // Create default continents with rotation angles
            continents = new ObservableCollection<Continent>
            {
                new Continent("Africa", "The second-largest continent, known for its diverse wildlife and cultures.",
                    "/Resources/Images/africa.jpg", "/Resources/Audio/africa.mp3", (-30, 0, 0)),

                new Continent("Antarctica", "The southernmost continent, covered in ice and home to unique wildlife.",
                    "/Resources/Images/antarctica.jpg", "/Resources/Audio/antarctica.mp3", (-90, 0, 0)),

                new Continent("Asia", "The largest continent with diverse cultures, landscapes, and ancient civilizations.",
                    "/Resources/Images/asia.jpg", "/Resources/Audio/asia.mp3", (0, -60, 0)),

                new Continent("Australia", "The smallest continent, known for unique wildlife and natural wonders.",
                    "/Resources/Images/australia.jpg", "/Resources/Audio/australia.mp3", (30, -120, 0)),

                new Continent("Europe", "A continent rich in history, art, and diverse cultures.",
                    "/Resources/Images/europe.jpg", "/Resources/Audio/europe.mp3", (0, 0, 0)),

                new Continent("North America", "A continent with diverse ecosystems from arctic tundra to tropical forests.",
                    "/Resources/Images/northamerica.jpg", "/Resources/Audio/northamerica.mp3", (0, 60, 0)),

                new Continent("South America", "Known for the Amazon rainforest, Andes mountains, and diverse cultures.",
                    "/Resources/Images/southamerica.jpg", "/Resources/Audio/southamerica.mp3", (-30, 60, 0))
            };
        }

        private void Load3DModel()
        {
            model = objReader.Read("C:\\Users\\Adi\\source\\repos\\continent-explorer\\Resources\\globe.obj");
            viewport.CameraMode = CameraMode.FixedPosition;
            viewport.Camera.LookDirection = new Vector3D(0, 0, -1);
            viewport.Camera.UpDirection = new Vector3D(0, 1, 0);
            viewport.Camera.Position = new Point3D(0, 0, 30);

            // Set up the rotation transform
            rotateTransform = new RotateTransform3D(currentRotation);
            transformGroup.Children.Add(rotateTransform);

            // Add scale transform
            ScaleTransform3D scaleTransform = new ScaleTransform3D(10, 10, 10);
            transformGroup.Children.Add(scaleTransform);

            model.Transform = transformGroup;
            globeModel.Content = model;
        }

        private void LoadLabels()
        {
            labelStack.Children.Clear();

            foreach (var continent in continents)
            {
                Button continentButton = new Button
                {
                    Content = continent.Name,
                    Margin = new Thickness(5),
                    Padding = new Thickness(10, 5, 10, 5),
                    Style = (Style)FindResource("ContinentButton")
                };

                continentButton.Click += (s, e) => SelectContinent(continent);
                labelStack.Children.Add(continentButton);
            }
        }

        private void SelectContinent(Continent continent)
        {
            selectedContinent = continent;
            RotateGlobe(continent);
            UpdateContinentInfo(continent);
        }

        private void UpdateContinentInfo(Continent continent)
        {
            continentNameText.Text = continent.Name;
            continentDescriptionText.Text = continent.Description;

            // Here you would also update images, play audio, etc.
            // For example:
            // continentImage.Source = new BitmapImage(new Uri(continent.ImagePath, UriKind.Relative));

            // Enable the explore button now that a continent is selected
            exploreButton.IsEnabled = true;
        }

        private void RotateGlobe(Continent continent)
        {
            try
            {
                if (model == null)
                {
                    MessageBox.Show("Model is not initialized!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Hardcoded rotation angles for each continent
                Dictionary<string, (double X, double Y, double Z)> ContinentRotations = new()
        {
            {"Africa", (-30, 0, 0) },
            {"Antarctica", (-90, 0, 0) },
            {"Asia", (0, -60, 0) },
            {"Australia", (-45, -120, 0) },
            {"Europe", (10, 10, 0) },
            {"North America", (15, 120, 0) },
            {"South America", (-45, 90, 0) }
        };

                // Retrieve rotation angles based on continent name
                if (!ContinentRotations.TryGetValue(continent.Name, out var rotationAngles))
                {
                    MessageBox.Show($"Rotation angles not found for {continent.Name}!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Convert angles to quaternions
                Quaternion qX = new Quaternion(new Vector3D(1, 0, 0), rotationAngles.X);
                Quaternion qY = new Quaternion(new Vector3D(0, 1, 0), rotationAngles.Y);
                Quaternion qZ = new Quaternion(new Vector3D(0, 0, 1), rotationAngles.Z);

                // Combine rotations to get the target quaternion
                Quaternion targetRotation = qX * qY * qZ;

                // Create quaternion animation
                QuaternionAnimation quaternionAnimation = new QuaternionAnimation
                {
                    From = currentRotation.Quaternion,
                    To = targetRotation,
                    Duration = TimeSpan.FromSeconds(1.5), // Animation duration
                    FillBehavior = FillBehavior.HoldEnd,
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut } // Smooth easing
                };

                // Update the current rotation to the target for the next animation
                currentRotation.Quaternion = targetRotation;

                // Begin the animation
                rotateTransform.Rotation.BeginAnimation(QuaternionRotation3D.QuaternionProperty, quaternionAnimation);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExploreButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedContinent != null)
            {
                ContinentDetailWindow detailWindow = new ContinentDetailWindow(selectedContinent);
                detailWindow.Show();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}