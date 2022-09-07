
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TestTask
{
    public class ApplicationSession
    {
        private readonly Cube _cube;

        private readonly Field _field;
        
        #region Ctor

        public ApplicationSession(Cube cube, Field field)
        {
            _cube = cube;
            _field = field;
        }
        
        #endregion
        
        #region Methods

        public async UniTask RunSession()
        {
            _field.RebuildField();
            var worldSpacePath = _field.WorldSpacePath;
            await _cube.RunAlongPath(worldSpacePath);
        }

        #endregion
 
    }
}


