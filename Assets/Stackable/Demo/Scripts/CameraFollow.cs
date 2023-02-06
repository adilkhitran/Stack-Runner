using UnityEngine;

namespace KHiTrAN.LevelManager
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed;
        [SerializeField] private Vector3 Offset;
        [SerializeField] private UpdateType updateType;


        private Vector3 _targetPosition;

        private void Start()
        {
            Init();
        }
        private void Update()
        {
            if (updateType == UpdateType.Update)
                FollowTarget();
        }

        private void FixedUpdate()
        {
            if (updateType == UpdateType.FixUpdate)
                FollowTarget();
        }

        private void LateUpdate()
        {
            if (updateType == UpdateType.LateUpdate)
                FollowTarget();
        }


        private void Init()
        {
            _targetPosition = target.position + Offset;
            transform.position = _targetPosition;
            transform.LookAt(target);
        }

        private void FollowTarget()
        {
            _targetPosition = target.position + Offset;
            _targetPosition.x = transform.position.x;
            transform.position = Vector3.Lerp(transform.position, _targetPosition, speed);
        }


        private enum UpdateType
        {
            Update,
            FixUpdate,
            LateUpdate
        }
    }
}
