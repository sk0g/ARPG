using JetBrains.Annotations;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Player.Feedbacks
{
[RequireComponent(typeof(MMFeedbacks))]
public class FeedbacksListener : MonoBehaviour
{
    MMFeedbacks _feedbacks;

    void Awake()
    {
        _feedbacks = GetComponent<MMFeedbacks>();
    }

    [UsedImplicitly]
    void PlayFeedbackNamed(string feedbackName)
    {
        if (feedbackName != name || !_feedbacks) return;

        _feedbacks.PlayFeedbacks();
    }
}
}