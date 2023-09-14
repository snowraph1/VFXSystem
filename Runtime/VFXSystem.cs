using Snowraph.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowraph.VFXSystems
{
    public class VFXSystem : MonoBehaviour, IGameSystem
    {
        #region Private Fields

        [SerializeField] private VFXPool _genericVFXPool;

        private List<IVFX> _vfxInstances = new List<IVFX>();

        private List<VFXHandle> _usedHandles = new List<VFXHandle>();

        #endregion

        #region Monobehaviour Methods

        private void OnDestroy()
        {
            for (int i = 0; i < _usedHandles.Count; i++)
            {
                if (_usedHandles[i] != null)
                {
                    _usedHandles[i].HandleStopped -= HandleStopped;
                }
            }
        }

        #endregion

        #region GameSystem Methods

        public void AwakeSystem()
        {
            LoadVFX(_genericVFXPool);
        }

        public void StartSystem()
        {

        }

        public void StopSystem()
        {

        }

        public void UpdateSystem()
        {

        }

        #endregion

        #region Public Methods

        public VFXHandle GetVFX(string id)
        {
            for (int i = _vfxInstances.Count - 1; i >= 0; i--)
            {
                if (_vfxInstances[i].Id == id)
                {
                    IVFX instance = _vfxInstances[i];
                    _vfxInstances.RemoveAt(i);

                    VFXHandle handle = new VFXHandle();

                    handle.SetupVFX(instance);

                    handle.HandleStopped += HandleStopped;

                    _usedHandles.Add(handle);

                    return handle;
                }
            }

            return null;
        }

        #endregion

        #region Private Methods

        private void LoadVFX(VFXPool vfxPool)
        {
            for (int i = 0; i < vfxPool.Pool.Length; i++)
            {
                for (int t = 0; t < vfxPool.Pool[i].Amount; t++)
                {
                    GameObject newVFX = Instantiate(vfxPool.Pool[i].Vfx, new Vector3(0, 3000, 0), Quaternion.identity);

                    newVFX.transform.parent = transform;

                    IVFX vfx = newVFX.GetComponent<IVFX>();

                    _vfxInstances.Add(vfx);
                }
            }
        }

        private void HandleStopped(VFXHandle handle)
        {
            handle.HandleStopped -= HandleStopped;

            handle.RootGameObject.transform.position = new Vector3(0, 3000, 0);
            handle.RootGameObject.transform.parent = transform;

            _usedHandles.Remove(handle);

            _vfxInstances.Add(handle.CurrentVFX);
        }

        #endregion
    }
}
