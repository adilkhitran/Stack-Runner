using UnityEngine;

namespace KHiTrAN
{
    public class InputController : MonoBehaviour
    {

        public float sideSpeed;

        public bool isPointerDown;
        public AnimationCurve inputCurve;

        private float inputDelta = 0;

        private Vector3 inputAxis;
        private Vector2 oldMousePos;


        void Start()
        {
            inputAxis = Vector3.zero;
        }

        public Vector3 GetInput()
        {
            if (UserPrefs.isAtFinalMomentum)
            {
                return Vector3.zero;
            }
            else
            {


                if (Input.GetMouseButtonDown(0))
                {
                    isPointerDown = true;
                    oldMousePos = Input.mousePosition;
                }
                else if (Input.GetMouseButton(0))
                {
                    inputDelta = (oldMousePos.x - Input.mousePosition.x) / Screen.width * sideSpeed;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    isPointerDown = false;
                }
                inputAxis.x = inputCurve.Evaluate(-inputDelta - transform.position.x);

                if (UserPrefs.isGameStarted)
                    inputAxis.z = 1;
                else
                    inputAxis.z = 0;

                return inputAxis;
            }
        }
    }
}