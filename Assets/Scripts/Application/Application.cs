using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TestTask
{
    public class Application : MonoBehaviour
    {
        private MeshCutter _meshCutter;

        private ApplicationSession _currentSession;
        
        #region MonoBehaviour
        
        private void Start()
        {
            ResolveDependencies();
            StartSession();
        }

        #endregion

        private void ResolveDependencies()
        {
            _meshCutter = new MeshCutter();
        }

        private async UniTask StartSession()
        {
            _currentSession = new ApplicationSession();
            await _currentSession.RunSession();
            StartSession();
        }
    }
}

