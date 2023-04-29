using RGB.modell.enums;

namespace RGB.modell.structs
{
    public struct Task
    {
        public BoxColor[,] task;
        public Direction direction;
        public Int32 expiration;
    }
}
