using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace continent_explorer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Configure camera first
        myView.CameraMode = CameraMode.FixedPosition;
        myView.Camera.LookDirection = new Vector3D(0, 0, -1);
        myView.Camera.UpDirection = new Vector3D(0, 1, 0);
        myView.Camera.Position = new Point3D(0, 0, 150);

        // Load the model
        ObjReader CurrentHelixObjectReader = new ObjReader();
        Model3DGroup MyModel = CurrentHelixObjectReader.Read("C:\\Users\\Adi\\source\\repos\\continent-explorer\\Resources\\globe.obj");

        // Create transform group
        Transform3DGroup transformGroup = new Transform3DGroup();

        // Single scale transform that handles both scaling and flipping
        ScaleTransform3D scaleTransform = new ScaleTransform3D(20.0, 20.0, 20.0);
        transformGroup.Children.Add(scaleTransform);

        TranslateTransform3D translate = new TranslateTransform3D();

        // Initial orientation
        QuaternionRotation3D quaternionRotation = new QuaternionRotation3D(
            new Quaternion(new Vector3D(0, 0, 1), -45) * // Tilt forward by 45° (Z-axis)
            new Quaternion(new Vector3D(1, 0, 0), -15)
        );
        RotateTransform3D rotateTransformBase = new RotateTransform3D(quaternionRotation);
        transformGroup.Children.Add(rotateTransformBase);

        // Rotation animation setup
        Vector3D tiltedYAxis = new Vector3D(Math.Sqrt(2) / 2, Math.Sqrt(2) / 2, -0.3); // 45° tilted Y-axis
        AxisAngleRotation3D axisAngleRotation = new AxisAngleRotation3D(tiltedYAxis, 0);
        RotateTransform3D rotateTransform = new RotateTransform3D(axisAngleRotation);
        transformGroup.Children.Add(rotateTransform);

        // Apply transform to model
        MyModel.Transform = transformGroup;

        // Apply model to viewport
        foo.Content = MyModel;

        // Configure rotation animation
        DoubleAnimation rotationAnimation = new DoubleAnimation
        {
            From = 0,
            To = -360,
            Duration = new Duration(TimeSpan.FromSeconds(5)),
            RepeatBehavior = RepeatBehavior.Forever
        };
        axisAngleRotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, rotationAnimation);

        // Optional: Set some lighting to better see the model
        //AmbientLight ambientLight = new AmbientLight(System.Windows.Media.Colors.White);
        //myView.Children.Add(new ModelVisual3D { Content = ambientLight });
    }
}