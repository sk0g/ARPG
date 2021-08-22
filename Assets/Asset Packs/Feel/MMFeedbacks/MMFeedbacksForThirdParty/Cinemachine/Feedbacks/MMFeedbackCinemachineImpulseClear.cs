using Cinemachine;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
[AddComponentMenu("")]
[FeedbackPath("Camera/Cinemachine Impulse Clear")]
[FeedbackHelp(
    "This feedback lets you trigger a Cinemachine Impulse clear, stopping instantly any impulse that may be playing.")]
public class MMFeedbackCinemachineImpulseClear : MMFeedback
{
    /// sets the inspector color for this feedback
#if UNITY_EDITOR
    public override Color FeedbackColor
    {
        get { return MMFeedbacksInspectorColors.CameraColor; }
    }
#endif

    protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1.0f)
    {
        if (Active) { CinemachineImpulseManager.Instance.Clear(); }
    }
}
}