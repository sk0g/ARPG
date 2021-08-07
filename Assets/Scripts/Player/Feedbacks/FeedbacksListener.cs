using MoreMountains.Feedbacks;
using UnityEngine;

namespace Player.Feedbacks
{
    public class FeedbacksListener : MonoBehaviour
    {
        void PlayFeedbacks(string feedbackName)
        {
            if (feedbackName == name)
            {
                GetComponent<MMFeedbacks>()?.PlayFeedbacks();
            }
        }
    }
}