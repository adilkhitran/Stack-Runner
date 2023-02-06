using UnityEngine;
using System.Collections.Generic;

namespace KHiTrAN.StackRunner
{
    public class StackPlayer : Stackable
    {
        public enum StackTypes : byte
        {
            FORWARD,
            BACKWARD,
            UPWARD,
            DOWNWARD
        }



        public PlayerProperties properties;

        private float directionML = 1;

        private Transform stackedNodeParent;
        private Transform unstackedNodeParent;

        private StackNode _temoNode;

        private List<StackNode> _stackNodeList;
        private List<StackNode> _removeNodeList;


        public static StackPlayer instance;

        private void Awake()
        {
            instance = this;
        }

        protected override void Start()
        {
            base.Start();

            Application.targetFrameRate = 30;
            properties.PlayerVelocityVector = new Vector3(0f, 0f, properties.PlayerForwardVelocity);

            _stackNodeList = new List<StackNode>();
            _removeNodeList = new List<StackNode>();

            _stackNodeList.Add(this);
            stackProperties.isStacked = true;

            stackedNodeParent = new GameObject("StackedNodeParent").transform;
            unstackedNodeParent = new GameObject("UnStackedNodeParent").transform;
        }

        void Update()
        {
            properties.PlayerVelocityVector.z = properties.PlayerForwardVelocity * directionML;
            rigidbody.velocity = properties.PlayerVelocityVector;
        }

        protected override void AddToStack(StackNode node)
        {

            if (_stackNodeList.Contains(node))
                return;
            else
            {
                _stackNodeList.Add(node);

                _temoNode = this.stackProperties.nextNode;

                node.transform.SetParent(this.stackProperties.frontJoint);
                node.transform.localPosition = Vector3.zero;
                node.transform.localRotation = Quaternion.identity;
                node.stackProperties.previousNode = this;
                node.stackProperties.isStacked = true;
                node.transform.SetParent(stackedNodeParent);

                this.stackProperties.nextNode = node;

                if (_temoNode != null)
                {
                    node.stackProperties.nextNode = _temoNode;
                    _temoNode.transform.SetParent(node.stackProperties.frontJoint);
                    _temoNode.transform.localPosition = Vector3.zero;
                    _temoNode.transform.localRotation = Quaternion.identity;
                    _temoNode.stackProperties.previousNode = node;
                    _temoNode.transform.SetParent(stackedNodeParent);
                }
            }

        }
        protected override void RemoveFromStack(StackNode node)
        {
            if (!_stackNodeList.Contains(node))
                return;


            if (node.Equals(this)) // If obstacle collided with player.
                node = node.stackProperties.nextNode;

            if (!node) // player have zero follwing node.
                return;


            directionML = -1.5f;
            Invoke("ResetReverse", 1);


            node.stackProperties.previousNode.stackProperties.nextNode = null;
            Remove(node);
            Invoke("AddForceToRemovedTransforms", 0.5f);
            AddForceToRemovedTransforms();
        }

        private void ResetReverse()
        {
            directionML = 1;
        }

        private void Remove(StackNode node)
        {
            _removeNodeList.Add(node);
            _stackNodeList.Remove(node);

            node.stackProperties.isStacked = false;
            node.transform.SetParent(unstackedNodeParent);

            if (properties.removeType == PlayerProperties.RemoveEffectType.Force)
                node.GetComponent<BoxCollider>().enabled = false;

            if (node.stackProperties.nextNode != null)
                Remove(node.stackProperties.nextNode);
        }

        private void AddForceToRemovedTransforms()
        {
            foreach (StackNode item in _removeNodeList)
            {
                item.stackProperties.previousNode = null;
                item.stackProperties.nextNode = null;

                if (properties.removeType == PlayerProperties.RemoveEffectType.Force)
                {
                    item.rigidbody.isKinematic = false;
                    item.rigidbody.AddForce(
                        Random.Range(properties.RemovedObjectForceRangeMin, properties.RemovedObjectForceRangeMax),
                        properties.RemovedObjectForceUp,
                        Random.Range(properties.RemovedObjectForceRangeMin, properties.RemovedObjectForceRangeMax),
                        ForceMode.Impulse
                        );
                }
                else
                {
                    item.rigidbody.isKinematic = true;
                    Vector3 _pos = item.transform.position;
                    _pos.x += Random.Range(-properties.ScatteredRange.x, properties.ScatteredRange.x);
                    _pos.z += Random.Range(-properties.ScatteredRange.z, properties.ScatteredRange.z);
                    item.transform.position = _pos;
                }
            }
            _removeNodeList.Clear();
        }
    }
}