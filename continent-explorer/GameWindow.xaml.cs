using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace continent_explorer
{
    public partial class GameWindow : Window
    {
        private Dictionary<string, string> continentMappings = new Dictionary<string, string>();

        public GameWindow()
        {
            InitializeComponent();
            Load3DModel();
            viewport.MouseMove += OnMouseMove;
        }

        private void Load3DModel()
        {
            // Read the .obj file
            ObjReader objReader = new ObjReader();
            Model3DGroup model = objReader.Read("C:\\Users\\Adi\\source\\repos\\continent-explorer\\Resources\\globe.obj");

            viewport.Camera.LookDirection = new Vector3D(0, 0, -1);
            viewport.Camera.UpDirection = new Vector3D(0, 1, 0);

            // Assign material names to continents (this assumes you have separate materials for each continent)
            continentMappings["africa"] = "Africa";
            continentMappings["asia"] = "Asia";
            continentMappings["europe"] = "Europe";
            continentMappings["north_america"] = "North America";
            continentMappings["south_america"] = "South America";
            continentMappings["australia"] = "Australia";
            continentMappings["antarctica"] = "Antarctica";

            globeModel.Content = model;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            // Perform a hit test to check what the mouse is hovering over
            Point mousePosition = e.GetPosition(viewport);
            HitTestResult result = VisualTreeHelper.HitTest(viewport, mousePosition);

            if (result is RayMeshGeometry3DHitTestResult hit)
            {
                string materialName = hit.ModelHit.GetValue(MaterialGroup.ChildrenProperty)?.ToString()?.ToLower();

                if (!string.IsNullOrEmpty(materialName) && continentMappings.ContainsKey(materialName))
                {
                    // Show label with continent name
                    continentLabel.Text = continentMappings[materialName];
                    continentLabel.Visibility = Visibility.Visible;
                    continentLabel.Margin = new Thickness(mousePosition.X + 10, mousePosition.Y + 10, 0, 0);
                }
                else
                {
                    continentLabel.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                continentLabel.Visibility = Visibility.Hidden;
            }
        }
    }
}
