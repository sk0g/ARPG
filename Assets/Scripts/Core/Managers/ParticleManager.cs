using System;
using UnityEngine;

[Serializable]
public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;
    public Particle blood, chunks;

    void Awake()
    {
        Instance = this;
    }

#if UNITY_EDITOR
    void FixedUpdate()
    {
        blood.MaybeEmit();
        chunks.MaybeEmit();
    }
#endif

    [Serializable]
    public class Particle
    {
        [SerializeField] ParticleSystem particlesToSpawn;

        [SerializeField] float defaultScale = 1f;

        public void Emit(Vector3 atPosition, float scaleSizeBy = 1f, int instancesToSpawn = 1, bool applyShape = true)
        {
            particlesToSpawn.Emit(new ParticleSystem.EmitParams
            {
                position = atPosition,
                applyShapeToPosition = applyShape,
                startSize = defaultScale * scaleSizeBy
            }, instancesToSpawn);
        }

#if UNITY_EDITOR
        [Header("DEBUG ONLY")] [SerializeField]
        bool testEmit;

        [SerializeField] Vector3 testEmitLocation;

        public void MaybeEmit()
        {
            if (testEmit) { Emit(testEmitLocation); }
        }
#endif
    }
}