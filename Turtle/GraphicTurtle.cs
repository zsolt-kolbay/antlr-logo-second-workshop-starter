using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Turtle.Grammar;

namespace Turtle.Main
{
    class GraphicTurtle : ITurtle
    {
        private readonly Canvas _turtleCanvas;
        private readonly FrameworkElement _turtleShape;
        private readonly RotateTransform _turtleRotation;

        private Brush _strokeBrush = Brushes.Black;

        public bool IsPenDown { get; set; } = true;

        public GraphicTurtle(Canvas turtleCanvas, FrameworkElement turtleShape, RotateTransform turtleRotation)
        {
            if (turtleCanvas == null) throw new ArgumentNullException(nameof(turtleCanvas));
            if (turtleShape == null) throw new ArgumentNullException(nameof(turtleShape));
            if (turtleRotation == null) throw new ArgumentNullException(nameof(turtleRotation));

            _turtleCanvas = turtleCanvas;
            _turtleShape = turtleShape;
            _turtleRotation = turtleRotation;
        }

        public void MoveForward(double distance)
        {
            // get current position
            double left = Canvas.GetLeft(_turtleShape);
            double top = Canvas.GetTop(_turtleShape);
            double width = _turtleShape.ActualWidth;
            double height = _turtleShape.ActualHeight;
            double angle = _turtleRotation.Angle;

            // calculate new position
            double newLeft = left + distance * Math.Sin(angle * Math.PI / 180.0);
            double newTop = top - distance * Math.Cos(angle * Math.PI / 180.0);

            // set new position
            Canvas.SetLeft(_turtleShape, newLeft);
            Canvas.SetTop(_turtleShape, newTop);

            if (IsPenDown)
            {
                // draw a line between the old and new position
                var line = new Line
                {
                    X1 = left + width / 2,
                    Y1 = top + height / 2,
                    X2 = newLeft + width / 2,
                    Y2 = newTop + height / 2,
                    StrokeThickness = 2,
                    Stroke = _strokeBrush
                };
                _turtleCanvas.Children.Add(line);
            }
        }

        public void TurnByAngle(double angle)
        {
            _turtleRotation.Angle += angle;
        }

        public void SetStrokeColor(TurtleStrokeColor strokeColor)
        {
            _strokeBrush = strokeColor switch
            {
                TurtleStrokeColor.Black => Brushes.Black,
                TurtleStrokeColor.Red => Brushes.Red,
                TurtleStrokeColor.Green => Brushes.Green,
                TurtleStrokeColor.Blue => Brushes.Blue,
                _ => throw new ArgumentException($"Invalid color value: {strokeColor}")
            };
        }

        public void SaveDrawnImageToFile(string filePath)
        {
            _turtleCanvas.Dispatcher.Invoke(() => 
            {
                var renderBitmap = new RenderTargetBitmap((int)_turtleCanvas.RenderSize.Width, (int)_turtleCanvas.RenderSize.Height, 96d, 96d, PixelFormats.Default);
                renderBitmap.Render(_turtleCanvas);

                var pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                var fileInfo = new FileInfo(filePath);
                fileInfo.Directory?.Create();

                using (var fileStream = File.OpenWrite(filePath))
                {
                    pngEncoder.Save(fileStream);
                }
            }, System.Windows.Threading.DispatcherPriority.Background);
        }
    }
}
