// using MoreMountains.Feedbacks;

using UnityEngine;

namespace Player.Feedbacks
{
// [RequireComponent(typeof(MMFeedbacks))]
public class FeedbacksListener : MonoBehaviour
{
    // MMFeedbacks _feedbacks;

    void Awake()
    {
        // _feedbacks = GetComponent<MMFeedbacks>();
        // _feedbacks.Initialization();

        // print($"{name} has feedback count {_feedbacks.Feedbacks.Count}");
    }

    void PlayFeedbacks(string feedbackName)
    {
        // if (feedbackName == name && _feedbacks)
        {
            // _feedbacks.PlayFeedbacks();
            print($"playing feedback in {name} with arg {feedbackName}");
        }
    }
}
}