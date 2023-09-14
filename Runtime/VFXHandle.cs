using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Snowraph.VFXSystems
{
    public class VFXHandle
    {
        #region Private Fields

        private IVFX _currentVFX;

        private bool _isPlaying = false;

        private Action<VFXHandle> _vfxStopped;

        #endregion

        #region IVFXHandle Properties

        public bool IsPlaying
        {
            get { return _isPlaying; }
        }

        public Action<VFXHandle> HandleStopped
        {
            get { return _vfxStopped; }
            set { _vfxStopped = value; }
        }

        public GameObject RootGameObject
        {
            get { return _currentVFX.RootGameObject; }
        }

        /// <summary>
        /// I don't like this please don't use
        /// </summary>
        public IVFX CurrentVFX
        {
            get { return _currentVFX; }
        }

        #endregion

        #region IVFXHandle Methods

        public void SetupVFX(IVFX vfx)
        {
            _currentVFX = vfx;

            _currentVFX.Initialize();

            _currentVFX.VFXStopped += VFXStopped;
        }

        public void Play(Vector3 position)
        {
            _isPlaying = true;

            _currentVFX.RootGameObject.transform.position = position;

            _currentVFX.Play();
        }

        public void Stop()
        {
            _isPlaying = false;

            _currentVFX.Stop();
        }

        public T GetModifiers<T>() where T : Component
        {
            return _currentVFX.GetModifiers<T>();
        }

        #endregion

        #region Private Methods

        private void VFXStopped()
        {
            _isPlaying = false;

            if (_vfxStopped != null)
            {
                _vfxStopped(this);
            }
        }

        #endregion
    }
}
