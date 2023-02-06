using UnityEngine;

namespace KHiTrAN.StackRunner
{
    [RequireComponent(typeof(BoxCollider))]
    public class Stackable : StackNode
    {

        private Vector3 _targetPosition;

        public StackPlayer player { get { return StackPlayer.instance; } }

        protected override void Start()
        {
            Initialize();
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (stackProperties.isStacked)
            {
                FollowNode();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((LayerMask.value & (1 << other.gameObject.layer)) > 0)
            {
                if (other.gameObject.tag.Equals("Stackable"))
                {
                    if (!stackProperties.isStacked && other.GetComponent<Stackable>().stackProperties.isStacked)
                    {
                        AddToStack(this);
                        Debug.Log("Added to Stack", gameObject);
                    }
                }
                else if (other.gameObject.tag.Equals("Obstacle"))
                {
                    if (stackProperties.isStacked)
                    {
                        RemoveFromStack(this);
                        Debug.Log("Remove from Stack", gameObject);
                    }
                }
            }
        }

        protected override void AddToStack(StackNode node)
        {
            if (!stackProperties.isStacked)
                player.AddToStack(node);
        }

        protected override void RemoveFromStack(StackNode node)
        {
            if (stackProperties.isStacked)
                player.RemoveFromStack(node);
        }

        private void Initialize()
        {

            switch (player.properties.StackType)
            {
                case StackPlayer.StackTypes.FORWARD:
                    stackProperties.frontJoint = jointPositions.forward;
                    stackProperties.backJoint = jointPositions.backward;
                    break;
                case StackPlayer.StackTypes.BACKWARD:
                    stackProperties.frontJoint = jointPositions.backward;
                    jointPositions.backward = jointPositions.forward;
                    break;
                case StackPlayer.StackTypes.UPWARD:
                    stackProperties.frontJoint = jointPositions.upward;
                    stackProperties.backJoint = jointPositions.downward;
                    break;
                case StackPlayer.StackTypes.DOWNWARD:
                    stackProperties.frontJoint = jointPositions.downward;
                    stackProperties.backJoint = stackProperties.frontJoint;
                    break;
            }
        }

        private void FollowNode()
        {
            _targetPosition = transform.position;
            _targetPosition.x = stackProperties.previousNode.transform.position.x;


            if (player.properties.StackType.Equals(StackPlayer.StackTypes.FORWARD) || player.properties.StackType.Equals(StackPlayer.StackTypes.BACKWARD))
            {
                _targetPosition.z = stackProperties.previousNode.stackProperties.frontJoint.position.z;
                _targetPosition.y = stackProperties.previousNode.transform.position.y;
            }
            else if (player.properties.StackType.Equals(StackPlayer.StackTypes.UPWARD) || player.properties.StackType.Equals(StackPlayer.StackTypes.DOWNWARD))
            {
                _targetPosition.y = stackProperties.previousNode.stackProperties.frontJoint.position.y;
                _targetPosition.z = stackProperties.previousNode.transform.position.z;
            }

            transform.position = Vector3.Slerp(transform.position, _targetPosition, Time.deltaTime * player.properties.FollowBackTransformFactor);
        }
    }
}