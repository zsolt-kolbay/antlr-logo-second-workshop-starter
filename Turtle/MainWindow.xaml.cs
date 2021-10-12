using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Turtle.Grammar;
using Turtle.Main;

namespace Turtle
{
    public partial class MainWindow
    {
        private const string InitialCode =
@"pen up
turn 180
forward 100
pen down

repeat 5 {
 color green
 repeat 8 {
   forward 150
   turn 144
 }

 pen up
 forward 350
 pen down

 color red
 repeat 120 {
   forward 10
   turn 3
 }
}
";        

        public MainWindow()
        {
            InitializeComponent();
            CodeTextBox.Text = InitialCode;
        }

        private void RunButtonClicked(object sender, RoutedEventArgs e)
        {
            ResetCanvas();

            var turtle = new GraphicTurtle(DrawingCanvas, TurtleShape, TurtleRotatation);
            LogoLanguageHelper.ParseAndExecuteLogoScript(CodeTextBox.Text, turtle);
        }

        private void ResetCanvas()
        {
            DrawingCanvas.Children.Clear();
            DrawingCanvas.Children.Add(TurtleShape);
            Canvas.SetLeft(TurtleShape, (DrawingCanvas.ActualWidth - TurtleShape.ActualWidth) / 2);
            Canvas.SetTop(TurtleShape, (DrawingCanvas.ActualHeight - TurtleShape.ActualHeight) / 2);
            TurtleRotatation.Angle = 0;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResetCanvas();
        }
    }
}
