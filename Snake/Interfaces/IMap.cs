using Snake.Enums;
using Snake.Helper;

namespace Snake.Interfaces
{
    public interface IMap
    {
        int Width { get; }
        int Height { get; }

        void Render(IRenderer renderer);

        FieldType GetField(Point point);

        void ChangeField(Point point, FieldType fieldType);

        void CreateRandomFruit();
    }
}
