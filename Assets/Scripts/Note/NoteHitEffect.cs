using System;
using System.Collections;
using UnityEngine;

namespace Rabbit
{
    public class NoteHitEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particleSystem;
        
        public void Effect(Texture2D tex)
        {
            StartCoroutine(EffectRoutine(tex));
        }

        private IEnumerator EffectRoutine(Texture2D tex)
        {
            particleSystem.GetComponent<Renderer>().material.mainTexture = tex;
            yield return null;
            
            particleSystem.Play();
            yield return new WaitForSeconds(3.0f);
            
            DestroyImmediate(gameObject);
        }
    }
}