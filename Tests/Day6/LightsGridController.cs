using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tests.Day6
{
    public sealed class LightsGridController<TLightType> : ILightsGridController
        where TLightType : ILight, new()
    {
        private readonly ILightsGrid<TLightType> _lightsGrid;

        public LightsGridController(ILightsGrid<TLightType> lightsGrid)
        {
            if (lightsGrid == null)
                throw new ArgumentNullException(nameof(lightsGrid));

            _lightsGrid = lightsGrid;
        }

        public void ProcessCommand(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            ICommand parsedCommand = ParseCommand(input);
            parsedCommand.Execute(_lightsGrid);
        }

        private ICommand ParseCommand(string input)
        {
            Debug.Assert(input != null);

            string[] splitted = input.Split(new char[] { ' ', }, StringSplitOptions.RemoveEmptyEntries);
            Debug.Assert(splitted != null);
            Debug.Assert(splitted.All(str => !string.IsNullOrWhiteSpace(str)));

            if (splitted.Length < 4)
                throw new ArgumentException("Invalid command format.");

            string finishPointStr = splitted[splitted.Length - 1];
            Point finishPoint = Point.Parse(finishPointStr);

            if (!"through".Equals(splitted[splitted.Length - 2], StringComparison.Ordinal))
                throw new ArgumentException("Invalid command format.");

            string startingPointStr = splitted[splitted.Length - 3];
            Point startingPoint = Point.Parse(startingPointStr);

            if ("toggle".Equals(splitted[0], StringComparison.Ordinal))
            {
                return new ToggleCommand(startingPoint, finishPoint);
            }
            else if ("turn".Equals(splitted[0], StringComparison.Ordinal))
            {
                if ("on".Equals(splitted[1], StringComparison.Ordinal))
                {
                    return new TurnOnCommand(startingPoint, finishPoint);
                }
                else if ("off".Equals(splitted[1], StringComparison.Ordinal))
                {
                    return new TurnOffCommand(startingPoint, finishPoint);
                }
                else
                    throw new ArgumentException("Invalid command format.");
            }
            else
                throw new ArgumentException("Invalid command format.");
        }

        public void ProcessCommands(IEnumerable<string> inputs)
        {
            if (inputs == null)
                throw new ArgumentNullException(nameof(inputs));
            if (inputs.Any(str => str == null))
                throw new ArgumentException("One of commands is null.", nameof(inputs));

            inputs.ForEach(ProcessCommand);
        }

        private interface ICommand
        {
            void Execute(ILightsGrid<TLightType> lightsGrid);
        }

        private class ToggleCommand : ICommand
        {
            private readonly Point _startingPoint;
            private readonly Point _finishPoint;

            public ToggleCommand(Point startingPoint, Point finishPoint)
            {
                if (startingPoint == null)
                    throw new ArgumentNullException(nameof(startingPoint));
                if (finishPoint == null)
                    throw new ArgumentNullException(nameof(finishPoint));

                _startingPoint = startingPoint;
                _finishPoint = finishPoint;
            }

            public void Execute(ILightsGrid<TLightType> lightsGrid)
            {
                if (lightsGrid == null)
                    throw new ArgumentNullException(nameof(lightsGrid));

                lightsGrid.Toggle(_startingPoint, _finishPoint);
            }
        }

        private class TurnOnCommand : ICommand
        {
            private readonly Point _startingPoint;
            private readonly Point _finishPoint;

            public TurnOnCommand(Point startingPoint, Point finishPoint)
            {
                if (startingPoint == null)
                    throw new ArgumentNullException(nameof(startingPoint));
                if (finishPoint == null)
                    throw new ArgumentNullException(nameof(finishPoint));

                _startingPoint = startingPoint;
                _finishPoint = finishPoint;
            }

            public void Execute(ILightsGrid<TLightType> lightsGrid)
            {
                if (lightsGrid == null)
                    throw new ArgumentNullException(nameof(lightsGrid));

                lightsGrid.TurnOn(_startingPoint, _finishPoint);
            }
        }

        private class TurnOffCommand : ICommand
        {
            private readonly Point _startingPoint;
            private readonly Point _finishPoint;

            public TurnOffCommand(Point startingPoint, Point finishPoint)
            {
                if (startingPoint == null)
                    throw new ArgumentNullException(nameof(startingPoint));
                if (finishPoint == null)
                    throw new ArgumentNullException(nameof(finishPoint));

                _startingPoint = startingPoint;
                _finishPoint = finishPoint;
            }

            public void Execute(ILightsGrid<TLightType> lightsGrid)
            {
                if (lightsGrid == null)
                    throw new ArgumentNullException(nameof(lightsGrid));

                lightsGrid.TurnOff(_startingPoint, _finishPoint);
            }
        }
    }
}