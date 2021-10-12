using Antlr4.Runtime.Misc;
using System;

namespace Turtle.Grammar
{
    public class TurtleVisitor : LogoBaseVisitor<object>
    {
        private readonly ITurtle _turtle;

        public TurtleVisitor(ITurtle turtle)
        {
            if (turtle == null) throw new ArgumentNullException(nameof(turtle));

            _turtle = turtle;
        }

        public override object VisitForward(LogoParser.ForwardContext context)
        {
            int distance = int.Parse(context.NUMBER().GetText());
            _turtle.MoveForward(distance);

            return null;
        }

        public override object VisitTurn(LogoParser.TurnContext context)
        {
            int angle = int.Parse(context.NUMBER().GetText());
            _turtle.TurnByAngle(angle);

            return null;
        }

        public override object VisitRepeat(LogoParser.RepeatContext context)
        {
            int times = int.Parse(context.NUMBER().GetText());

            for (int i = 0; i < times; i++)
            {
                VisitCommandList(context.commandList());
            }

            return null;
        }

        public override object VisitSetColor([NotNull] LogoParser.SetColorContext context)
        {
            var strokeColor = context.strokeColor();

            if (strokeColor.COLOR_BLACK() is not null)
            {
                _turtle.SetStrokeColor(TurtleStrokeColor.Black);
            }
            else if (strokeColor.COLOR_RED() is not null)
            {
                _turtle.SetStrokeColor(TurtleStrokeColor.Red);
            }
            else if (strokeColor.COLOR_GREEN() is not null)
            {
                _turtle.SetStrokeColor(TurtleStrokeColor.Green);
            }
            else if (strokeColor.COLOR_BLUE() is not null)
            {
                _turtle.SetStrokeColor(TurtleStrokeColor.Blue);
            }

            return null;
        }

        public override object VisitPenUpOrDown([NotNull] LogoParser.PenUpOrDownContext context)
        {
            _turtle.IsPenDown = context.DOWN() is not null;

            return null;
        }
    }
}
