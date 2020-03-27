using Snake.Enums;
using Snake.Helper;
using Snake.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class Map : IMap
    {
        FieldType[,] _playfield;
        FieldType[,] _changedData;

        int maxFruits = 5;
        int _fruits = 0;

        Random rnd;

        public Map(int width, int height)
        {
            rnd = new Random();

            Width = width;
            Height = height;

            _playfield = new FieldType[width, height];
            _changedData = new FieldType[width, height];

            //Rand einzeichnen
            for (int n = 0; n < width; n++)
            {
                ChangeField(n, 0, FieldType.Border);
                ChangeField(n, height - 1, FieldType.Border);
            }
            for (int n = 0; n < height; n++)
            {
                ChangeField(0, n, FieldType.Border);
                ChangeField(width - 1, n, FieldType.Border);
            }
        }

        public int Width { get; }
        public int Height { get; }

        private void ChangeField(int x, int y, FieldType fieldType)
        {
            var oldType = _playfield[x, y];
            if (oldType != fieldType)
            {
                _playfield[x, y] = fieldType;
                _changedData[x, y] = fieldType;

                if (fieldType == FieldType.Fruit1)
                    _fruits++;

                if (oldType == FieldType.Fruit1)
                {
                    _fruits--;
                    CreateRandomFruit();
                }
            }
        }

        public void ChangeField(Point point, FieldType fieldType)
        {
            ChangeField(point.X, point.Y, fieldType);
        }

        public FieldType GetField(Point point)
        {
            return _playfield[point.X, point.Y];
        }

        public void CreateRandomFruit()
        {
            if (_fruits < maxFruits)
            {
                var fruitIndex = rnd.Next(this.Width * this.Height);
                var x = fruitIndex % Width;
                var y = fruitIndex / Width;
                while (_playfield[x, y] != FieldType.None)
                {
                    x--;
                    if (x < 0)
                        x = Width - 1;
                    y--;
                    if (y < 0)
                    {
                        x = Width - 1;
                        y = Height - 1;
                    }
                }
                ChangeField(x, y, FieldType.Fruit1);
            }
        }

        public void Render(IRenderer renderer)
        {
            renderer.Render(_changedData);

            for (int x = 0; x < _changedData.GetLength(0); x++)
            {
                for (int y = 0; y < _changedData.GetLength(1); y++)
                {
                    _changedData[x, y] = FieldType.NoChange;
                }
            }
        }
    }
}
