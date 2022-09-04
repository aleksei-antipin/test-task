using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TestTask
{
    public class ApplicationSession 
    {
        #region Ctor

        public ApplicationSession()
        {
                
        }
        
        #endregion
        
        #region Methods

        public async UniTask RunSession()
        {
            CutCloth();
            await MoveCube();
        }
        
        #endregion

        private void CutCloth()
        {
            Debug.Log("Mesh cutting");
        }

        private async UniTask MoveCube()
        {

            await Task.Delay(1);

            Debug.Log("Cube movement started");
            await UniTask.Delay(3000);
            Debug.Log("Cube movement ended");
        }
        
    }
}


