using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace AI.Actions.TEst {
    public class MoveToPlayer : ActionNode {
        public float runSpeed = 4f;
        
        [Tooltip("Degrees per second the character can turn")] [SerializeField]
        float rotationSpeed = 150f;
        
        public Vector3 lastPlayerPosition;
        
        CharacterController _cc;
        GameObject _player;
        
        protected override void OnStart() {
            _player = GameObject.FindWithTag("Player");
            _cc = context.gameObject.GetComponent<CharacterController>();
        }

        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            UpdatePerception();
            UpdatePositionAndLookDirection();
            
            return State.Success;
        }

        void UpdatePositionAndLookDirection()
        {
            MoveInDirection(offsetToPlayer);
            LookAtDirection(offsetToPlayer);
        }
        
        void UpdatePerception()
        {
            UpdateLastPlayerPosition();
            UpdateDistanceToPlayer();
            UpdateOffsetToPlayer();
        }
        
        public void MoveInDirection(Vector3 movementDirection) =>
            _cc.SimpleMove(movementDirection.normalized * runSpeed);
        
        void UpdateLastPlayerPosition()
        {
            lastPlayerPosition.x = _player.transform.position.x;
            lastPlayerPosition.z = _player.transform.position.z;
        }

        public float distanceToPlayer;

        void UpdateDistanceToPlayer() => distanceToPlayer =
            Vector3.Distance(context.transform.position, lastPlayerPosition);

        public Vector3 offsetToPlayer;

        void UpdateOffsetToPlayer() => offsetToPlayer =
            lastPlayerPosition - context.transform.position;
        
        public void LookAtDirection(Vector3 direction) =>
            context.transform.rotation = Quaternion.RotateTowards(
                from:  context.transform.rotation,
                to: Quaternion.LookRotation(direction),
                maxDegreesDelta: Time.fixedDeltaTime * rotationSpeed);
    }
}