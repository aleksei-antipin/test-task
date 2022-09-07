using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public static class TransformExtension 
    {

        public static SegmentedLine TransformSegmentedLine(this Transform transform, SegmentedLine line)
        {
            var points = new Vector3[line.Points.Length];
            for (var i = 0; i < points.Length; i++)
            {
                points[i] = transform.TransformPoint(line.Points[i]);
            }
            var transformedLine = new SegmentedLine(points);
            return transformedLine;
        }

        public static SegmentedLine InverseTransformSegmentLine(this Transform transform, SegmentedLine line)
        {
            var points = new Vector3[line.Points.Length];
            for (var i = 0; i < points.Length; i++)
            {
                points[i] = transform.InverseTransformDirection(line.Points[i]);
            }
            var transformedLine = new SegmentedLine(points);
            return transformedLine;
        }
        
        
    }
}

