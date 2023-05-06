using UnitService.Library.Models;

namespace UnitService.Library.Constants
{
    public static class Dimensions
    {
        public static Dimension NONE => new Dimension();
        public static Dimension LENGTH => new Dimension(lengthExp:1, 0, 0, 0, 0);
        public static Dimension TIME => new Dimension(0, timeExp:1, 0, 0, 0);
        public static Dimension MASS => new Dimension(0, 0, massExp:1, 0, 0);
        public static Dimension CURRENT => new Dimension(0, 0, 0, currentExp:1, 0);
        public static Dimension TEMPERATURE => new Dimension(0, 0, 0, 0, tempExp:1);
    }
}
