﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace MoreMountains.Feedbacks
{
/// <summary>
/// Events triggered by a MMFeedbacks when playing a series of feedbacks
/// - play : when a MMFeedbacks starts playing
/// - pause : when a holding pause is met
/// - resume : after a holding pause resumes
/// - revert : when a MMFeedbacks reverts its play direction
/// - complete : when a MMFeedbacks has played its last feedback
///
/// to listen to these events :
///
/// public virtual void OnMMFeedbacksEvent(MMFeedbacks source, EventTypes type)
/// {
///     // do something
/// }
/// 
/// protected virtual void OnEnable()
/// {
///     MMFeedbacksEvent.Register(OnMMFeedbacksEvent);
/// }
/// 
/// protected virtual void OnDisable()
/// {
///     MMFeedbacksEvent.Unregister(OnMMFeedbacksEvent);
/// }
/// 
/// </summary>
public struct MMFeedbacksEvent
{
    public enum EventTypes
    {
        Play,
        Pause,
        Resume,
        Revert,
        Complete
    }

    public delegate void Delegate(MMFeedbacks source, EventTypes type);

    static private event Delegate OnEvent;

    static public void Register(Delegate callback)
    {
        OnEvent += callback;
    }

    static public void Unregister(Delegate callback)
    {
        OnEvent -= callback;
    }

    static public void Trigger(MMFeedbacks source, EventTypes type)
    {
        OnEvent?.Invoke(source, type);
    }
}

/// <summary>
/// A subclass of MMFeedbacks, contains UnityEvents that can be played, 
/// </summary>
[Serializable]
public class MMFeedbacksEvents
{
    /// whether or not this MMFeedbacks should fire MMFeedbacksEvents
    [Tooltip("whether or not this MMFeedbacks should fire MMFeedbacksEvents")]
    public bool TriggerMMFeedbacksEvents = false;

    /// whether or not this MMFeedbacks should fire Unity Events
    [Tooltip("whether or not this MMFeedbacks should fire Unity Events")]
    public bool TriggerUnityEvents = true;

    /// This event will fire every time this MMFeedbacks gets played
    [Tooltip("This event will fire every time this MMFeedbacks gets played")]
    public UnityEvent OnPlay;

    /// This event will fire every time this MMFeedbacks starts a holding pause
    [Tooltip("This event will fire every time this MMFeedbacks starts a holding pause")]
    public UnityEvent OnPause;

    /// This event will fire every time this MMFeedbacks resumes after a holding pause
    [Tooltip("This event will fire every time this MMFeedbacks resumes after a holding pause")]
    public UnityEvent OnResume;

    /// This event will fire every time this MMFeedbacks reverts its play direction
    [Tooltip("This event will fire every time this MMFeedbacks reverts its play direction")]
    public UnityEvent OnRevert;

    /// This event will fire every time this MMFeedbacks plays its last MMFeedback
    [Tooltip("This event will fire every time this MMFeedbacks plays its last MMFeedback")]
    public UnityEvent OnComplete;

    public bool OnPlayIsNull { get; protected set; }
    public bool OnPauseIsNull { get; protected set; }
    public bool OnResumeIsNull { get; protected set; }
    public bool OnRevertIsNull { get; protected set; }
    public bool OnCompleteIsNull { get; protected set; }

    /// <summary>
    /// On init we store for each event whether or not we have one to invoke
    /// </summary>
    public virtual void Initialization()
    {
        OnPlayIsNull = OnPlay == null;
        OnPauseIsNull = OnPause == null;
        OnResumeIsNull = OnResume == null;
        OnRevertIsNull = OnRevert == null;
        OnCompleteIsNull = OnComplete == null;
    }

    /// <summary>
    /// Fires Play events if needed
    /// </summary>
    /// <param name="source"></param>
    public virtual void TriggerOnPlay(MMFeedbacks source)
    {
        if (!OnPlayIsNull && TriggerUnityEvents) { OnPlay.Invoke(); }

        if (TriggerMMFeedbacksEvents) { MMFeedbacksEvent.Trigger(source, MMFeedbacksEvent.EventTypes.Play); }
    }

    /// <summary>
    /// Fires pause events if needed
    /// </summary>
    /// <param name="source"></param>
    public virtual void TriggerOnPause(MMFeedbacks source)
    {
        if (!OnPauseIsNull && TriggerUnityEvents) { OnPause.Invoke(); }

        if (TriggerMMFeedbacksEvents) { MMFeedbacksEvent.Trigger(source, MMFeedbacksEvent.EventTypes.Pause); }
    }

    /// <summary>
    /// Fires resume events if needed
    /// </summary>
    /// <param name="source"></param>
    public virtual void TriggerOnResume(MMFeedbacks source)
    {
        if (!OnResumeIsNull && TriggerUnityEvents) { OnResume.Invoke(); }

        if (TriggerMMFeedbacksEvents) { MMFeedbacksEvent.Trigger(source, MMFeedbacksEvent.EventTypes.Resume); }
    }

    /// <summary>
    /// Fires revert events if needed
    /// </summary>
    /// <param name="source"></param>
    public virtual void TriggerOnRevert(MMFeedbacks source)
    {
        if (!OnRevertIsNull && TriggerUnityEvents) { OnRevert.Invoke(); }

        if (TriggerMMFeedbacksEvents) { MMFeedbacksEvent.Trigger(source, MMFeedbacksEvent.EventTypes.Revert); }
    }

    /// <summary>
    /// Fires complete events if needed
    /// </summary>
    /// <param name="source"></param>
    public virtual void TriggerOnComplete(MMFeedbacks source)
    {
        if (!OnCompleteIsNull && TriggerUnityEvents) { OnComplete.Invoke(); }

        if (TriggerMMFeedbacksEvents) { MMFeedbacksEvent.Trigger(source, MMFeedbacksEvent.EventTypes.Complete); }
    }
}
}