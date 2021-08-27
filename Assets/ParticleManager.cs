using System;
using System.Collections;
using UnityEngine;

[Serializable]
public enum Emitter
{
    CHUNKS,
    BLOOD,
}

[Serializable]
public struct EmitterParameters
{
    public Vector3 Scale;
    [SerializeField] public ParticleSystem Emitter;

    public EmitterParameters(Vector3 scale, ParticleSystem emitter)
    {
        Scale = scale;
        Emitter = emitter;
    }
}

[Serializable]
public class ParticleManager : MonoBehaviour
{
    static ParticleManager instance;
    public ParticleSystem blood, chunks;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        Emit(Emitter.BLOOD);
        Emit(Emitter.CHUNKS);
    }

    void Emit(Emitter emitType)
    {
        StartCoroutine(DoEmit(emitType));
    }

    IEnumerator DoEmit(Emitter emitType)
    {
        if (emitType == Emitter.BLOOD)
        {
            ParticleSystem.EmitParams emitParams = new()
            {
                position = GameObject.Find("Player").transform.position,
                applyShapeToPosition = true,
                startSize = 0.05f
            };

            blood.Emit(emitParams, 1);
        }
        else if (emitType == Emitter.CHUNKS)
        {
            ParticleSystem.EmitParams emitParams = new()
            {
                position = GameObject.Find("Player").transform.position, applyShapeToPosition = true
            };
            chunks.Emit(emitParams, 1);
        }

        yield return null;
    }
}