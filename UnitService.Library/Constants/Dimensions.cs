using UnitService.Library.Models;

namespace UnitService.Library.Constants
{
    public static class Dimensions
    {
        // FUNDAMENTAL GROUP
        public static Dimension NONE => new Dimension();
        public static Dimension LENGTH => new Dimension(lengthExp:1, 0, 0, 0, 0);
        public static Dimension TIME => new Dimension(0, timeExp:1, 0, 0, 0);
        public static Dimension MASS => new Dimension(0, 0, massExp:1, 0, 0);
        public static Dimension CURRENT => new Dimension(0, 0, 0, currentExp:1, 0);
        public static Dimension TEMPERATURE => new Dimension(0, 0, 0, 0, tempExp:1);

        // DERIVED GROUP
        public static Dimension DISTANCE => LENGTH;
        public static Dimension DISPLACEMENT => LENGTH;
        public static Dimension SPEED => LENGTH / TIME;
        public static Dimension VELOCITY => LENGTH / TIME;
        public static Dimension ACCELERATION => SPEED / TIME;
        public static Dimension FORCE => MASS * ACCELERATION;
        public static Dimension ENERGY => FORCE * DISTANCE;
        public static Dimension POWER => ENERGY / TIME;
    }
}
