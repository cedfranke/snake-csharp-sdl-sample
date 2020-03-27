using SharpSDL;
using Snake.Enums;
using Snake.Interfaces;
using System;

namespace Snake
{
    public class SdlRenderer:IRenderer, IDisposable
    {
        private readonly int blockSize;
        Window _window;
        Renderer _renderer;

        public SdlRenderer()
        {
            SDL.Init(InitFlags.Video);

            _window = SDL.CreateWindow(".NET Core SDL2-CS Tutorial", SDL.WINDOWPOS_CENTERED, SDL.WINDOWPOS_CENTERED, 1024, 768, WindowFlags.Resizable);
            _renderer = SDL.CreateRenderer(_window, -1, RendererFlags.Accelerated);
            this.blockSize = 20;
        }

        public void Dispose()
        {
            SDL.DestroyWindow(_window);
        }

        public void Render(FieldType[,] changedRenderFields)
        {
            unsafe
            {
                for (int x = 0; x < changedRenderFields.GetLength(0); x++)
                {
                    for (int y = 0; y < changedRenderFields.GetLength(1); y++)
                    {
                        var f = changedRenderFields[x, y];

                        var rect = new Rect();
                        rect.Height = this.blockSize;
                        rect.Width = this.blockSize;
                        rect.X = this.blockSize * x;
                        rect.Y = this.blockSize * y;

                        switch (f)
                        {
                            case FieldType.None:
                                {
                                    SDL.SetRenderDrawColor(_renderer, 230, 230, 230, 205);
                                    SDL.RenderFillRect(_renderer, &rect);
                                    break;
                                }
                            case FieldType.Fruit1:
                                {
                                    SDL.SetRenderDrawColor(_renderer, 255, 255, 0, 205);
                                    SDL.RenderFillRect(_renderer, &rect);
                                    break;
                                }
                            case FieldType.Border:
                                {
                                    SDL.SetRenderDrawColor(_renderer, 0, 255, 0, 205);
                                    SDL.RenderFillRect(_renderer, &rect);
                                    break;
                                }
                            case FieldType.Snake1:
                                {
                                    SDL.SetRenderDrawColor(_renderer, 255, 0, 0, 205);
                                    SDL.RenderFillRect(_renderer, &rect);
                                    break;
                                }
                            case FieldType.Snake2:
                                {
                                    SDL.SetRenderDrawColor(_renderer, 0, 0, 255, 205);
                                    SDL.RenderFillRect(_renderer, &rect);
                                    break;
                                }
                        }
                    }
                }
                SDL.RenderPresent(_renderer);
            }
        }
    }
}
