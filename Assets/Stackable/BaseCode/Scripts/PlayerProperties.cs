using UnityEngine;
using static KHiTrAN.StackRunner.StackPlayer;

namespace KHiTrAN.StackRunner
{
    [CreateAssetMenu(fileName = "Properties", menuName = "KHiTrAN/Stackable/Properties", order = 1)]
    public class PlayerProperties : ScriptableObject
    {
        public StackTypes StackType;
        public float FollowBackTransformFactor;
        public float PlayerForwardVelocity;
        public float PlayerSideVelocity;
        [HideInInspector]
        public Vector3 PlayerVelocityVector;

        public RemoveEffectType removeType;

        [Tooltip("Determine the min. force value to apply for the object that removed from stack")]
        public float RemovedObjectForceRangeMin;
        [Tooltip("Determine the max. force value to apply for the object that removed from stack")]
        public float RemovedObjectForceRangeMax;
        [Tooltip("Determine the up direction force value to apply for the object that removed from stack")]
        public float RemovedObjectForceUp;

        public Vector3 ScatteredRange;

        public enum RemoveEffectType
        {
            Force,
            Scattered
        }
    }
}