using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tests.Day6
{
    public sealed class LightsGrid<TLightType> : ILightsGrid<TLightType>
        where TLightType : ILight, new()
    {
        private readonly ILight[][] _grid;

        public LightsGrid()
        {
            _grid = new ILight[GRID_SIZE][];
            for (int i = 0; i < _grid.Length; ++i)
            {
                _grid[i] = new ILight[GRID_SIZE];
                for (int j = 0; j < _grid[i].Length; ++j)
                {
                    _grid[i][j] = new TLightType();
                }
            }
        }

        public void TurnOffAllOfTheLights()
        {
            LightsGroup lightsGroup = GetAllLights();
            lightsGroup.TurnOff();
        }

        public void TurnOnAllOfTheLights()
        {
            LightsGroup lightsGroup = GetAllLights();
            lightsGroup.TurnOn();
        }

        public void Toggle(Point startingPoint, Point finishPoint)
        {
            CheckPoints(startingPoint, finishPoint);
            LightsGroup lightsGroup = GetLightsGroup(startingPoint, finishPoint);
            lightsGroup.Toggle();
        }

        public void TurnOn(Point startingPoint, Point finishPoint)
        {
            CheckPoints(startingPoint, finishPoint);
            LightsGroup lightsGroup = GetLightsGroup(startingPoint, finishPoint);
            lightsGroup.TurnOn();
        }

        public void TurnOff(Point startingPoint, Point finishPoint)
        {
            CheckPoints(startingPoint, finishPoint);
            LightsGroup lightsGroup = GetLightsGroup(startingPoint, finishPoint);
            lightsGroup.TurnOff();
        }

        private void CheckPoints(Point startingPoint, Point finishPoint)
        {
            if (startingPoint == null)
                throw new ArgumentNullException(nameof(startingPoint));
            if (finishPoint == null)
                throw new ArgumentNullException(nameof(finishPoint));
            if (startingPoint.X > finishPoint.X)
                throw new ArgumentException("X coordinate of starting point cannot be greater than X coordinate of finish point.", nameof(startingPoint));
            if (startingPoint.Y > finishPoint.Y)
                throw new ArgumentException("Y coordinate of starting point cannot be greater than Y coordinate of finish point.", nameof(startingPoint));
        }

        private LightsGroup GetLightsGroup(Point startingPoint, Point finishPoint)
        {
            Debug.Assert(startingPoint != null);
            Debug.Assert(finishPoint != null);
            Debug.Assert(startingPoint.X <= finishPoint.X);
            Debug.Assert(startingPoint.Y <= finishPoint.Y);

            IEnumerable<ILight> lights = GetLights(startingPoint, finishPoint);
            LightsGroup lightsGroup = new LightsGroup(lights);
            return lightsGroup;
        }

        private LightsGroup GetAllLights()
        {
            Point startingPoint = new Point(0, 0);
            Point finishPoint = new Point(GRID_SIZE - 1, GRID_SIZE - 1);
            LightsGroup allLightsGroup = GetLightsGroup(startingPoint, finishPoint);
            return allLightsGroup;
        }

        private IEnumerable<ILight> GetLights(Point startingPoint, Point finishPoint)
        {
            Debug.Assert(startingPoint != null);
            Debug.Assert(finishPoint != null);
            Debug.Assert(startingPoint.X <= finishPoint.X);
            Debug.Assert(startingPoint.Y <= finishPoint.Y);

            for (int x = startingPoint.X; x <= finishPoint.X; ++x)
            {
                for (int y = startingPoint.Y; y <= finishPoint.Y; ++y)
                {
                    yield return _grid[x][y];
                }
            }
        }

        public int TotalBrightness
        {
            get
            {
                int totalBrightness = 0;
                Point startingPoint = new Point(0, 0);
                Point finishPoint = new Point(GRID_SIZE - 1, GRID_SIZE - 1);
                for (int x = startingPoint.X; x <= finishPoint.X; ++x)
                {
                    for (int y = startingPoint.Y; y <= finishPoint.Y; ++y)
                    {
                        totalBrightness += _grid[x][y].TotalBrightness;
                    }
                }

                return totalBrightness;
            }
        }

        private const int GRID_SIZE = 1000;
    }
}