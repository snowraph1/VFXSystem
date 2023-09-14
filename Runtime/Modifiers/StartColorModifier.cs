using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowraph.VFXSystems
{
    public class StartColorModifier : MonoBehaviour, IVFXModifier
    {
        #region Private Fields

        private ParticleVFX _particleVFX;

        #endregion

        #region IVFXModifier

        public void Initialize(IVFX vfx)
        {
            _particleVFX = (ParticleVFX)vfx;
        }

        #endregion

        #region Public Methods

        public void SetRandomColors(Color[] colors)
        {
            Gradient gradient = new Gradient();

            gradient.mode = GradientMode.Fixed;

            GradientColorKey[] colorKeys = new GradientColorKey[colors.Length];

            int colorKeyIndex = 0;

            float separator = 1f / colors.Length;

            for (int i = 0; i < colors.Length; i++)
            {
                colorKeys[colorKeyIndex] = new GradientColorKey(colors[i], separator * i + separator);

                colorKeyIndex++;
            }

            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
            alphaKeys[0] = new GradientAlphaKey(1.0f, 0.0f);
            alphaKeys[1] = new GradientAlphaKey(1.0f, 1.0f);

            gradient.SetKeys(colorKeys, alphaKeys);

            var main = _particleVFX.Particles.main;

            ParticleSystem.MinMaxGradient minMaxGradient;
            minMaxGradient = new ParticleSystem.MinMaxGradient(gradient);
            minMaxGradient.mode = ParticleSystemGradientMode.RandomColor;

            main.startColor = minMaxGradient;
        }

        public void ResetModifier()
        {
            var main = _particleVFX.Particles.main;
            main.startColor = Color.white;
        }

        #endregion
    }
}
