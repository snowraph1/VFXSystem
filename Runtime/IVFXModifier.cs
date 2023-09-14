using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowraph.VFXSystems
{
    public interface IVFXModifier
    {
        public void Initialize(IVFX vfx);

        public void ResetModifier();
    }
}
