﻿using UnitService.Core.Models;

namespace UnitService.Core.Constants
{
    /// <summary>
    /// A class that holds predefined dimensions
    /// </summary>
    public static class Dimensions
    {
        // FUNDAMENTAL GROUP
        /// <summary>
        /// A dimension representing no dimension
        /// </summary>
        public static Dimension NONE => new Dimension();
        /// <summary>
        /// A dimension for length
        /// </summary>
        public static Dimension LENGTH => new Dimension(lengthExp: 1);
        /// <summary>
        /// A dimension for time
        /// </summary>
        public static Dimension TIME => new Dimension(timeExp: 1);
        /// <summary>
        /// A dimension for mass
        /// </summary>
        public static Dimension MASS => new Dimension(massExp: 1);
        /// <summary>
        /// A dimension for current
        /// </summary>
        public static Dimension CURRENT => new Dimension(currentExp: 1);
        /// <summary>
        /// A dimension for temperature
        /// </summary>
        public static Dimension TEMPERATURE => new Dimension(tempExp: 1);

        /// <summary>
        /// A dimension for luminous intensity
        /// </summary>
        public static Dimension LUMINOUS_INTENSITY => new Dimension(luminousIntensityExp: 1);

        /// <summary>
        /// A dimension for amount of substance
        /// </summary>
        public static Dimension AMOUNT_OF_SUBSTANCE => new Dimension(amountOfSubstanceExp: 1);

        // DERIVED GROUP
        /// <summary>
        /// A dimension for distance
        /// </summary>
        public static Dimension DISTANCE => LENGTH;
        /// <summary>
        /// A dimension for displacement
        /// </summary>
        public static Dimension DISPLACEMENT => LENGTH;
        /// <summary>
        /// A dimension for speed
        /// </summary>
        public static Dimension SPEED => LENGTH / TIME;
        /// <summary>
        /// A dimension for velocity
        /// </summary>
        public static Dimension VELOCITY => LENGTH / TIME;
        /// <summary>
        /// A dimension for acceleration
        /// </summary>
        public static Dimension ACCELERATION => SPEED / TIME;
        /// <summary>
        /// A dimension for force
        /// </summary>
        public static Dimension FORCE => MASS * ACCELERATION;
        /// <summary>
        /// A dimension for energy
        /// </summary>
        public static Dimension ENERGY => FORCE * DISTANCE;
        /// <summary>
        /// A dimension for power
        /// </summary>
        public static Dimension POWER => ENERGY / TIME;
    }
}
