using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Snowraph.VFXSystems
{
    public interface IVFX
    {
        public string Id { get; }

        public GameObject RootGameObject { get; }

        public Action VFXStopped { get; set; }

        public void Initialize();

        public void Play();

        public void Stop();

        T GetModifiers<T>() where T : Component;
    }
}
