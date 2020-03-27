using SharpSDL;
using Snake.Enums;
using Snake.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake
{
    public class Game
    {
        IGameObject[] _gameObjects;
        IMap _map;
        IRenderer _renderer;

        public void Run()
        {
            _renderer = new SdlRenderer();

            _map = new Map(50, 30);
            var snakes = new[]{
                new Snake(0, _map, 10, _map.Height / 2),
                new Snake(1, _map, _map.Width - 10, _map.Height / 2) 
            };

            _map.CreateRandomFruit();

        var gameObjects = new List<IGameObject>();
            gameObjects.AddRange(snakes);
            _gameObjects = gameObjects.ToArray();

            unsafe
            {
                var evt = new Event();
                Event* e = &evt;
                bool quit = false;
                long? last = null;
                while (!quit)
                {
                    while (SDL.PollEvent(e) != 0)
                    {
                        switch (e->Type)
                        {
                            case EventType.Quit:
                                quit = true;
                                break;
                            case EventType.KeyDown:
                                switch (e->Keyboard.Keysym.Sym)
                                {
                                    case Keycode.Q:
                                        quit = true;
                                        break;
                                    case Keycode.Up: snakes[0].Input(InputType.Up); break;
                                    case Keycode.Down: snakes[0].Input(InputType.Down); break;
                                    case Keycode.Left: snakes[0].Input(InputType.Left); break;
                                    case Keycode.Right: snakes[0].Input(InputType.Right); break;
                                }
                                break;
                        }
                    }

                    var current = DateTime.UtcNow.Ticks;
                    if (last == null)
                        last = current;
                    Update(current - last.Value);
                    last = current;
                    Render();
                }
            }

            
            SDL.Quit();
        }

        void Update(long ticks)
        {
            foreach (var g in _gameObjects)
                g.Update(ticks);
        }

        void Render()
        {
            _map.Render(_renderer);
        }
    }
}
