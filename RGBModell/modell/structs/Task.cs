using RGBModell.modell.enums;

namespace RGBModell.modell.structs
{
    /// <summary>
    /// Represents a task of a game
    /// </summary>
    public struct Task
    {
        public BoxColor[,] task;
        public Direction direction;
        public Int32 expiration;
    }
}
