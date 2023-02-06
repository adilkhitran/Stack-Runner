using UnityEngine;
using KHiTrAN.StackRunner;

namespace KHiTrAN
{
    [System.Serializable]
    public struct StackProperties
    {
        public bool isStacked;

        public Transform frontJoint;
        public Transform backJoint;

        public StackNode nextNode;
        public StackNode previousNode;
    }
}