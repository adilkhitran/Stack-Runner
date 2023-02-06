using UnityEngine;

namespace KHiTrAN
{
    public abstract class StackNode : MonoBehaviour
    {
        public LayerMask LayerMask;

        [HideInInspector]
        public new Rigidbody rigidbody;

        public JointPositions jointPositions;

       // [HideInInspector]
        public StackProperties stackProperties;

        protected abstract void Start();
        protected abstract void AddToStack(StackNode node);
        protected abstract void RemoveFromStack(StackNode node);
    }
}