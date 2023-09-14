using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowraph.VFXSystems
{
    public class ParticleVFX : MonoBehaviour, IVFX
    {
        #region Private Fields

        [SerializeField] private string _id;

        [SerializeField] private ParticleSystem _particleSystem;

        [SerializeField] private Component[] _vfxModifiers;

        private Action _particleStopped;

        #endregion

        #region IVFX Properties

        public string Id
        {
            get { return _id; }
        }

        public Action VFXStopped
        {
            get { return _particleStopped; }
            set { _particleStopped = value; }
        }

        public GameObject RootGameObject
        {
            get { return _particleSystem.gameObject; }
        }

        public ParticleSystem Particles
        {
            get { return _particleSystem; }
        }

        #endregion

        #region IVFX Methods

        public void Initialize()
        {
            for (int i = 0; i < _vfxModifiers.Length; i++)
            {
                ((IVFXModifier)_vfxModifiers[i]).Initialize(this);
            }
        }

        public T GetModifiers<T>() where T : Component
        {
            for (int i = 0; i < _vfxModifiers.Length; i++)
            {
                if (_vfxModifiers[i].GetType() == typeof(T))
                {
                    return (T)_vfxModifiers[i];
                }
            }

            return default(T);
        }

        public void Play()
        {
            _particleSystem.Play();
        }

        public void Stop()
        {
            _particleSystem.Stop();
        }

        #endregion

        #region Private Methods

        private void OnParticleSystemStopped()
        {
            Stopped();
        }

        private void Stopped()
        {
            if (_particleStopped != null)
            {
                _particleStopped();
            }

            for (int i = 0; i < _vfxModifiers.Length; i++)
            {
                ((IVFXModifier)_vfxModifiers[i]).ResetModifier();
            }
        }

        #endregion
    }
}
