using RGBModell.modell.enums;

namespace RGBModell.modell.structs
{
    public struct Task
    {
        public BoxColor[,] task;
        public Direction direction;
        public Int32 expiration;
    }
}
