using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public readonly struct SegmentedLine
    {
        private readonly Vector3[] _points;
        public Vector3[] Points => _points;

        #region Ctor

        public SegmentedLine(params Vector3[] points)
        {
            _points = points;
        }
        
        #endregion

    }
}

