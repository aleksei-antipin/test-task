using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  TestTask
{
    public enum TurningDirection
    {
        Clockwise = 0,
        Counterclockwise = 1,
    }
    
    public static class TurningDirectionUtils
    {
        public static TurningDirection GetRandom()
        {
            var directionNumber = Random.Range(0, 2);
            return (TurningDirection)directionNumber;
        }

        public static TurningDirection Opposite(this TurningDirection direction)
        {
            if (direction == TurningDirection.Clockwise)
                return TurningDirection.Counterclockwise;

            return TurningDirection.Clockwise;
        }

        public static int Sign(this TurningDirection direction)
        {
            var sign = direction switch
            {
                TurningDirection.Clockwise => 1,
                TurningDirection.Counterclockwise => -1,
            };

            return sign;
        }
        
        
    }
}

