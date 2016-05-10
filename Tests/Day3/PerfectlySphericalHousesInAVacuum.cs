using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tests.Tools;

namespace Tests.Day3
{
    public sealed class PerfectlySphericalHousesInAVacuum : IPerfectlySphericalHousesInAVacuum
    {
        private readonly IParser _parser;

        public PerfectlySphericalHousesInAVacuum(IParser parser)
        {
            if (parser == null)
                throw new ArgumentNullException(nameof(parser));

            _parser = parser;
            _parser.DefineToken('^', MoveNorth);
            _parser.DefineToken('v', MoveSouth);
            _parser.DefineToken('>', MoveEast);
            _parser.DefineToken('<', MoveWest);
        }

        public int HousesWithAtLeastOnePresent => _visitedHouses.Count;

        public int NumberOfAgents { get; set; } = 1;

        private HashSet<Position> _visitedHouses = new HashSet<Position>();
        private Agent[] _agents;
        private int _currentAgent = 0;

        public void Process(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            _agents = Enumerable.Range(0, NumberOfAgents).Select(ignored => new Agent()).ToArray();

            _visitedHouses.Add(new Position(0, 0));

            _parser.Parse(input);
        }

        private void MoveWest() => Move(agent => agent.MoveWest());
        private void MoveEast() => Move(agent => agent.MoveEast());
        private void MoveSouth() => Move(agent => agent.MoveSouth());
        private void MoveNorth() => Move(agent => agent.MoveNorth());

        private void Move(Action<Agent> agentMoveAction)
        {
            Debug.Assert(agentMoveAction != null);
            Agent currentAgent = GetCurrentAgent();
            agentMoveAction(currentAgent);
            MarkHouseVisited(currentAgent);
            ActivateTheNextAgent();
        }

        private Agent GetCurrentAgent() => _agents[_currentAgent];

        private void MarkHouseVisited(Agent agent)
        {
            _visitedHouses.Add(agent.CurrentPosition);
        }

        private void ActivateTheNextAgent()
        {
            _currentAgent = (_currentAgent + 1) % NumberOfAgents;
        }

        private sealed class Agent
        {
            private Position _currentPosition = new Position(0, 0);
            public Position CurrentPosition => _currentPosition;

            public void MoveWest()
            {
                _currentPosition = new Position(_currentPosition.X - 1, _currentPosition.Y);
            }

            public void MoveEast()
            {
                _currentPosition = new Position(_currentPosition.X + 1, _currentPosition.Y);
            }

            public void MoveSouth()
            {
                _currentPosition = new Position(_currentPosition.X, _currentPosition.Y + 1);
            }

            public void MoveNorth()
            {
                _currentPosition = new Position(_currentPosition.X, _currentPosition.Y - 1);
            }
        }

        private sealed class Position : IEquatable<Position>
        {
            public int X { get; }
            public int Y { get; }

            public Position(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override int GetHashCode() => X * 31 + Y;
            public override bool Equals(object obj) => Equals(obj as Position);
            public bool Equals(Position position)
            {
                if (position == null)
                    return false;

                return X == position.X && Y == position.Y;
            }
        }
    }
}