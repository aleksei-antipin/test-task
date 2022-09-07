using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class LineGenerator
    {
        #region Methods

        public static SegmentedLine Generate(Vector3 startPoint, Vector3 endPoint, Plane plane, GeneratorSettings settings)
        {
            var points = new List<Vector3>();
            points.Add(startPoint);

            var mainDirection = (endPoint - startPoint).normalized;
            var displacementDirection = Quaternion.AngleAxis(90, plane.normal) * mainDirection;

            var currentOrigin = startPoint;
            var turningDirection = TurningDirectionUtils.GetRandom();

            while (!IsPointReachable(currentOrigin, endPoint, settings.maxStepSize))
            {
                var origin = currentOrigin + mainDirection * Random.Range(settings.minStepSize, settings.minStepSize);
                var displacement = Random.Range(settings.minBaseLineOffset, settings.maxBaseLineOffset);
                var point = origin + turningDirection.Sign() * displacement * displacementDirection;
                points.Add(point);
                currentOrigin = origin;
                turningDirection = turningDirection.Opposite();
            }
            
            points.Add(endPoint);
            var line = new SegmentedLine(points.ToArray());
            return line;
        }
        
        #endregion

        private static bool IsPointReachable(Vector3 fromPoint, Vector3 point, float maxStepSize)
        {
            var distance = (point - fromPoint).magnitude;
            return distance < maxStepSize;
        }

    }
}
