using System;
using System.Collections.Generic;
using UnityEngine;

public enum Emitter
{
    CHUNKS,
    BLOOD,
}

[Serializable]
public struct EmitterParameters
{
    public Emitter EmitterType;
    public Vector3 Scale;
    public ParticleSystem Emitter;

    public EmitterParameters(Emitter emiterType, Vector3 scale, ParticleSystem emitter)
    {
        EmitterType = emiterType;
        Scale = scale;
        Emitter = emitter;
    }
}

[Serializable]
public struct EmitterParameters2
{
    public Vector3 Scale;
    public ParticleSystem Emitter;

    public EmitterParameters2(Emitter emiterType, Vector3 scale, ParticleSystem emitter)
    {
        Scale = scale;
        Emitter = emitter;
    }
}

[Serializable]
public class EmitterParameters3
{
    public Emitter em;
    public EmitterParameters2 pm;
}

public class ParticleManager : MonoBehaviour
{
    static ParticleManager instance;
    [SerializeField] List<EmitterParameters> ParticleEmitters = new();

    [SerializeField] EmitterParameters3 emitters = new();

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
        // var selectedWithLinq = ParticleEmitters.Where(x => x.EmitterType == emitType)
        //                                        .Select(x => (emitType?) x.EmitterType)
        //                                        .FirstOrDefault();

        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();

        if (emitType == Emitter.BLOOD)
        {
            emitParams.position = GameObject.Find("Player").transform.position;
            emitParams.applyShapeToPosition = true;
            emitParams.startSize = 0.05f;

            ParticleEmitters[0].Emitter.Emit(emitParams, 1);
        }
        else if (emitType == Emitter.CHUNKS)
        {
            emitParams.position = GameObject.Find("Player").transform.position;
            emitParams.applyShapeToPosition = true;

            ParticleEmitters[1].Emitter.Emit(emitParams, 1);
        }
        else { }
    }
}