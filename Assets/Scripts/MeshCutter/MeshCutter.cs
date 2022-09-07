using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

namespace  TestTask
{
    public class MeshCutter 
    {
        #region Methods

        public static (Mesh positive, Mesh negative) CutMesh(Mesh sourceMesh, Plane plane)
        {
            return (null, null);
        }

        public static (Mesh positive, Mesh negative) CutMesh(Mesh sourceMesh, SegmentedLine cuttingLine)
        {
            // var localStartPoint = sourceMeshGo.transform.InverseTransformPoint(startPoint);
            // var localEndPoint = sourceMeshGo.transform.InverseTransformPoint(endPoint);
            //
            // var midPoint = (localEndPoint + localStartPoint) * 0.5f;
            // var thirdPoint = new Vector3(midPoint.x, 1.0f, midPoint.z);
            //
            // var side1 = localEndPoint - localStartPoint;
            // var side2 = thirdPoint - localStartPoint;;
            //
            // var normal = Vector3.Cross(side1, side2).normalized;
            //
            // var plane = new Plane();
            // plane.Set3Points(localStartPoint, localEndPoint, thirdPoint);
            //
            //
            // var sliceMetaData = new SlicesMetadata(plane, sourceMesh, false, false, false, false);
            //
            // var positive = sliceMetaData.PositiveSideMesh;
            // var negative = sliceMetaData.NegativeSideMesh;
            //
            // positive.name = "positive";
            // negative.name = "negative";
            //
            return (null, null);
        }
        
        #endregion
    }
}

