using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TestTask
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private BoxCollider _cutBounds;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private MeshFilter _sourceMeshFilter;
        [SerializeField] private MeshRenderer _sourceMeshRenderer;
        [SerializeField] private LineRenderer _lineRenderer;
        [Range(0.1f, 1)] [SerializeField] private float _maxStepSizeCoefficient;
        [Range(0.1f, 1)] [SerializeField] private float _minStepSizeCoefficient;
        [Range(0.1f, 1)] [SerializeField] private float _maxBaseLineOffsetCoefficient;
        [Range(0.1f, 1)] [SerializeField] private float _minBaseLineOffsetCoefficient;
        public SegmentedLine WorldSpacePath { get; private set; }

        #region MonoBehaviour

        private void OnDestroy()
        {
            _lineRenderer.positionCount = 0;
            _lineRenderer.SetPositions(new Vector3[]{});
        }
        
        #endregion

        public void RebuildField()
        {
            var distance = (_endPoint.position - _startPoint.position).magnitude;
            var width = _cutBounds.bounds.size.x;
            var maxStepSize = distance * _maxStepSizeCoefficient;
            var maxBaseLineOffset = width * _maxBaseLineOffsetCoefficient;
            var generationSettings = new GeneratorSettings
            {
                maxStepSize = maxStepSize,
                minStepSize = maxStepSize * _minStepSizeCoefficient,
                maxBaseLineOffset = maxBaseLineOffset,
                minBaseLineOffset = maxBaseLineOffset * _minBaseLineOffsetCoefficient
            };
            var plane = new Plane(Vector3.up, Vector3.zero);
            WorldSpacePath = LineGenerator.Generate(_startPoint.position, _endPoint.position, plane, generationSettings);

            _lineRenderer.positionCount = WorldSpacePath.Points.Length;
            _lineRenderer.SetPositions(WorldSpacePath.Points);
            
            var localSpacePath = _sourceMeshRenderer.gameObject.transform.InverseTransformSegmentLine(WorldSpacePath);
            var (positive, negative) = MeshCutter.CutMesh(_sourceMeshFilter.mesh, localSpacePath);
        }
        
        
    }
}

