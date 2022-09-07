using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TestTask
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private float _speed = 1.0f;

        [SerializeField] private float _rotationSpeed = 50.0f;

        private float _epsilon = 0.01f;

        private bool _isCancelled = false;
        
        #region MonoBehaviour

        private void OnDestroy()
        {
            CancelRunning();
        }

        #endregion
        
        #region  Methods
        
        public async UniTask RunAlongPath(SegmentedLine path)
        {
            _isCancelled = false;
            
            if (path.Points.Length == 0)
                return;
            
            var firsPoint = path.Points[0];
            var lastPoint = path.Points[path.Points.Length - 1];
            transform.position = firsPoint;
            
            if (path.Points.Length < 1)
                return;

            var currentTargetIndex = 1;
            var currentTarget = path.Points[currentTargetIndex];
            var currentTargetRotation = Quaternion.LookRotation(currentTarget - transform.position);
            while (true)
            {
                var targetReached = IsPointReached(transform.position, currentTarget, _epsilon);

                if (targetReached)
                {
                    currentTargetIndex++;
                    if (currentTargetIndex == path.Points.Length)
                    {
                        break;
                    }
                    currentTarget = path.Points[currentTargetIndex];
                    currentTargetRotation = Quaternion.LookRotation(currentTarget - transform.position);
                }
                
                await UniTask.Yield();

                if (_isCancelled)
                    return;
                
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, Time.deltaTime * _speed);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, currentTargetRotation, Time.deltaTime * _rotationSpeed);
            }
            transform.position = lastPoint;
        }

        public void Pause()
        {
 
        }

        public void Resume()
        {
            
        }

        public void CancelRunning()
        {
            _isCancelled = true;
        }
        
        #endregion

        private bool IsPointReached(Vector3 fromPoint, Vector3 point, float epsilon)
        {
            var distance = (point - fromPoint).magnitude;
            return distance < epsilon;
        }

    }
}
