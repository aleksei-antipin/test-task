using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask
{
    public class Cube : MonoBehaviour
    {
        private enum CubePosition
        {
            Center = 0,
            Left = 1, 
            Right = 2,
        }
        
        [SerializeField] private float _speed = 1.0f;

        [SerializeField] private float _rotationSpeed = 50.0f;

        [SerializeField] private float _shiftDistance = 2.0f;

        private Vector3 _offset = Vector3.zero;
        
        private InputHandler _inputHandler;
        
        private float _epsilon = 0.01f;

        private bool _isCancelled = false;

        private CubePosition _currentCubePosition = CubePosition.Center;
        
        #region MonoBehaviour

        private void OnDestroy()
        {
            CancelRunning();
        }

        #endregion
        
        #region  Methods

        public void Initialize(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _inputHandler.SwipeRecognized += OnSwipeRecognized;
        }
        
        public async UniTask RunAlongPath(SegmentedLine path)
        {
            _offset = Vector3.zero;
            _currentCubePosition = CubePosition.Center;
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
            var currentTargetRotation = Quaternion.LookRotation(currentTarget + _offset - transform.position);
            while (true)
            {
                var targetReached = IsPointReached(transform.position, currentTarget + _offset, _epsilon);

                if (targetReached)
                {
                    currentTargetIndex++;
                    if (currentTargetIndex == path.Points.Length)
                    {
                        break;
                    }
                    currentTarget = path.Points[currentTargetIndex];
                    currentTargetRotation = Quaternion.LookRotation(currentTarget + _offset - transform.position);
                }
                
                await UniTask.Yield();

                if (_isCancelled)
                    return;
                
                transform.position = Vector3.MoveTowards(transform.position, currentTarget + _offset, Time.deltaTime * _speed);
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

        private void OnSwipeRecognized(SwipeDirection direction)
        {
            if (_currentCubePosition == CubePosition.Center)
            {
                if (direction == SwipeDirection.Left)
                {
                    _currentCubePosition = CubePosition.Left;
                    ShiftLeft();
                    return;
                }

                if (direction == SwipeDirection.Right)
                {
                    _currentCubePosition = CubePosition.Right;
                    ShiftRight();
                    return;
                }
            }

            if (_currentCubePosition == CubePosition.Left && direction == SwipeDirection.Right)
            {
                ShiftRight();
                return;
            }

            if (_currentCubePosition == CubePosition.Right && direction == SwipeDirection.Left)
            {
                ShiftLeft();
                return;
            }

            _currentCubePosition = CubePosition.Center;
        }

        private void ShiftLeft()
        {
            _offset -= Vector3.left * _shiftDistance;
        }

        private void ShiftRight()
        {
            _offset -= Vector3.right * _shiftDistance;
        }
        
        private bool IsPointReached(Vector3 fromPoint, Vector3 point, float epsilon)
        {
            var distance = (point - fromPoint).magnitude;
            return distance < epsilon;
        }
    }
}
