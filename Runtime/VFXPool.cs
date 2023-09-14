using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Snowraph.VFXSystems
{
    [CreateAssetMenu(fileName = "VFXPool", menuName = "ScriptableObjects/VFX/VFXPool", order = 2)]
    public class VFXPool : ScriptableObject
    {
        [Serializable]
        public struct VFXPoolEntry
        {
            public GameObject Vfx;
            public int Amount;
        }

        [SerializeField] private VFXPoolEntry[] _vfxPool;

        public VFXPoolEntry[] Pool
        {
            get { return _vfxPool; }
        }
    }
}
