namespace Turtle.Grammar
{
    public interface ITurtle
    {
        bool IsPenDown { get; set; }

        void MoveForward(double distance);
        void TurnByAngle(double angle);
        void SetStrokeColor(TurtleStrokeColor strokeColor);
        void SaveDrawnImageToFile(string filePath);
    }
}