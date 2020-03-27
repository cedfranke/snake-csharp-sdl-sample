using Snake.Enums;
using Snake.Helper;
using Snake.Interfaces;

namespace Snake
{
    class Snake : IGameObject
    {
        MoveType _moveDirection;
        IMap _map;
        Point[] _snakePositions;
        int length;
        int _snakeId;

        public long moveTickCount = 1000000;
        public long currentMoveTickCount = 0;

        bool dead = false;

        public Snake(int snakeId, IMap map, int startX, int startY)
        {
            _snakeId = snakeId;
            _map = map;

            _snakePositions = new Point[100];
            _snakePositions[0] = new Point(startX, startY);
            _map.ChangeField(_snakePositions[0], (FieldType)(((int)FieldType.Snake1) + _snakeId));
            length = 1;
        }

        public void Input(InputType input)
        {
            if (input == InputType.Down && _moveDirection != MoveType.Up)
                _moveDirection = MoveType.Down;
            else if (input == InputType.Up && _moveDirection != MoveType.Down)
                _moveDirection = MoveType.Up;
            else if (input == InputType.Left && _moveDirection != MoveType.Right)
                _moveDirection = MoveType.Left;
            else if (input == InputType.Right && _moveDirection != MoveType.Left)
                _moveDirection = MoveType.Right;
        }

        public void Update(long ticks)
        {
            if (!dead)
            {
                currentMoveTickCount += ticks;
                if (currentMoveTickCount > moveTickCount)
                {
                    currentMoveTickCount -= moveTickCount;
                    Point nextPos;
                    if (_moveDirection== MoveType.Up)
                    {
                        nextPos = new Point(_snakePositions[0].X, _snakePositions[0].Y - 1);
                    }
                    else if (_moveDirection == MoveType.Down)
                    {
                        nextPos = new Point(_snakePositions[0].X, _snakePositions[0].Y + 1);
                    }
                    else if (_moveDirection == MoveType.Left)
                    {
                        nextPos = new Point(_snakePositions[0].X - 1, _snakePositions[0].Y);
                    }
                    else
                    {
                        nextPos = new Point(_snakePositions[0].X + 1, _snakePositions[0].Y);
                    }
                    var nextfield = _map.GetField(nextPos);

                    if (nextfield == FieldType.Fruit1)
                    {
                        length++;
                        for (int n = length - 1; n > 0; n--)
                        {
                            _snakePositions[n] = _snakePositions[n - 1];
                                }
                        _snakePositions[0] = nextPos;
                        _map.ChangeField(nextPos, (FieldType)(((int)FieldType.Snake1) + _snakeId));
                    }
                    if (nextfield == FieldType.None)
                    {
                        var last = _snakePositions[length - 1];
                        for (int n = length - 1; n > 0; n--)
                        {
                            _snakePositions[n] = _snakePositions[n - 1];
                        }
                        _snakePositions[0] = nextPos;
                        _map.ChangeField(nextPos, (FieldType)(((int)FieldType.Snake1) + _snakeId));
                        _map.ChangeField(last, FieldType.None);
                    }
                }
            }
        }
    }
}
