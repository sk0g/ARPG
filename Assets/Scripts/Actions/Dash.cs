using System.Collections;
using Interfaces;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Actions
{
    public class Dash : MonoBehaviour
    {
        [SerializeField] [Tooltip("How long the charge up phase of the dash lasts for, before the movement starts")]
        float chargeTime = .1f;

        [SerializeField] [Tooltip("How long the dash will move the character for")]
        float movementTime = .25f;

        [SerializeField] [Tooltip("How long till after this dash can the character dash again")]
        float cooldownTime = 1f;

        [SerializeField] [Tooltip("The speed of the movement phase of the jump")]
        float speed = 20f;

        [SerializeField] [Tooltip("Feedback to play on dash. Fine to leave unfilled")]
        MMFeedbacks feedback;

        public bool isDashing;
        public bool canDash = true;

        IPusher _pusher;

        void Awake()
        {
            _pusher = GetComponent<IPusher>();
        }

        bool _inMovePhase;

        void FixedUpdate()
        {
            if (_inMovePhase) { _pusher.PushForward(Time.fixedDeltaTime * speed); }
        }

        public IEnumerator StartDash()
        {
            canDash = false;
            isDashing = true;

            // start dash feedback, then charge up the dash
            if (feedback != null) { feedback.PlayFeedbacks(); }

            yield return new WaitForSeconds(chargeTime);

            // put dash in move phase for movementTime
            _inMovePhase = true;
            yield return new WaitForSeconds(movementTime);

            // no longer dashing, so other movement can be used
            isDashing = false;
            _inMovePhase = false;

            // new dashes can be started after cooldownTime is up
            yield return new WaitForSeconds(cooldownTime);

            canDash = true;

            yield return null;
        }
    }
}