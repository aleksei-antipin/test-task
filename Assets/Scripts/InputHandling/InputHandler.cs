using System;
using DigitalRubyShared;

namespace TestTask
{

    public enum SwipeDirection
    {
        Left = 0, 
        Right = 1,
        Up = 2,
        Down = 3,
        Any = 4,
    }
    
    public class InputHandler
    {
        private readonly SwipeGestureRecognizer _swipeGestureRecognizer;

        public event Action<SwipeDirection> SwipeRecognized; 

        #region Ctor

        public InputHandler()
        {
            _swipeGestureRecognizer = new SwipeGestureRecognizer();
            _swipeGestureRecognizer.StateUpdated += OnGestureStateChanged;
            FingersScript.Instance.AddGesture(_swipeGestureRecognizer);
        }

        #endregion

        private void OnGestureStateChanged(GestureRecognizer gestureRecognizer)
        {
            switch (gestureRecognizer.State)
            {
                case GestureRecognizerState.Ended:
                    OnSwipeRecognized();
                    break;
            }
        }

        private void OnSwipeRecognized()
        {
            var direction = _swipeGestureRecognizer.EndDirection switch
            {
                SwipeGestureRecognizerDirection.Left => SwipeDirection.Left,
                SwipeGestureRecognizerDirection.Right => SwipeDirection.Right,
                SwipeGestureRecognizerDirection.Down => SwipeDirection.Down,
                SwipeGestureRecognizerDirection.Up => SwipeDirection.Up,
                SwipeGestureRecognizerDirection.Any => SwipeDirection.Any,
            };
            
            SwipeRecognized?.Invoke(direction);
        }
        
    }
}
