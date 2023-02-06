using UnityEngine;
using KHiTrAN.StackRunner;

namespace KHiTrAN
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private Vector2 xRange = new Vector2(-5,5);

        private StackPlayer player;
        private InputController inputCont;

        private void Start()
        {
            player = GetComponent<StackPlayer>();
            inputCont = GetComponent<InputController>();
        }

        private void Update()
        {
            var inputVector = inputCont.GetInput();
            inputVector.x *= player.properties.PlayerSideVelocity;
            player.properties.PlayerVelocityVector = inputVector;
        }

        private void FixedUpdate()
        {
            var position = transform.position;
            position.x = Mathf.Clamp(position.x, xRange.x, xRange.y);
            transform.position = position;
        }
    }
}
