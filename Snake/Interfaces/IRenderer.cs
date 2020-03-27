using Snake.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Interfaces
{
    public interface IRenderer
    {
        void Render(FieldType[,] changedRenderFields);
    }
}
