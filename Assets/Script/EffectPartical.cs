using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPartical : MonoBehaviour
{
    public ParticleSystem _ParticalSystem;
    [System.Obsolete]
    private void OnEnable()
    {
        if(_ParticalSystem)
        {
            _ParticalSystem.Play();
            _ParticalSystem.loop = true;
        }
      
    }
}
