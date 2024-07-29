using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public static class PublishingEvents {
    public static Events events = new();
}
public class Events {
    public UnityEvent Win = new();
    public UnityEvent Lost = new(); 
    public UnityEvent RewardedAd = new();
    public UnityEvent Interstitial = new();
}