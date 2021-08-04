using UnityEngine;

namespace AI.Actions
{
    public class BT_Movement : MonoBehaviour
    {
        [SerializeField] float runSpeed = 3.5f;
        [SerializeField] float walkSpeed = 2f;

        [Tooltip("Degrees per second the character can turn")] [SerializeField]
        float rotationSpeed = 150f;

        CharacterController _cc;
        GameObject _player;
        
        public void FootL()
        {
            // Animation Event Sink for Attack1
        }
        
        public void FootR()
        {
            // Animation Event Sink for Attack1
        }
        
        void Awake()
        {
            _cc = GetComponent<CharacterController>();
            _player = GameObject.FindWithTag("Player");
        }

        public void RunInDirection(Vector3 movementDirection) =>
            _cc.SimpleMove(movementDirection.normalized * runSpeed);

        public void WalkInDirection(Vector3 movementDirection) =>
            _cc.SimpleMove(movementDirection.normalized * walkSpeed);

        public float DistanceToPlayer() => Vector3.Distance(transform.position, _player.transform.position);

        public Vector3 OffsetToPlayer()
        {
            var offset = _player.transform.position - transform.position;
            offset.y = 0;
            return offset;
        }

        public void LookAtDirection(Vector3 direction) =>
            transform.rotation = Quaternion.RotateTowards(
                from: transform.rotation,
                to: Quaternion.LookRotation(direction),
                maxDegreesDelta: Time.fixedDeltaTime * rotationSpeed);
    }
}