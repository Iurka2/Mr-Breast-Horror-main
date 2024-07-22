using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class UICanvasManager : MonoBehaviour {
    public Canvas uiCanvas;
    public PlayableDirector playableDirector;
    public TimerTimer timer;

    void Awake ( ) {
        if(playableDirector != null) {
            playableDirector.played += OnPlayableDirectorPlayed;
            playableDirector.stopped += OnPlayableDirectorStopped;
        }
    }

    void OnEnable ( ) {
        if(playableDirector != null && playableDirector.state == PlayState.Playing) {
            StartCoroutine(DisableUIAfterFrame());
        }
    }

    void OnDisable ( ) {
        if(playableDirector != null) {
            playableDirector.played -= OnPlayableDirectorPlayed;
            playableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }

    void OnPlayableDirectorPlayed ( PlayableDirector director ) {
        if(uiCanvas != null) {
            uiCanvas.enabled = false;
        }
        if(timer != null) {
            timer.StopTimer();
        }
    }

    void OnPlayableDirectorStopped ( PlayableDirector director ) {
        if(uiCanvas != null) {
            uiCanvas.enabled = true;
        }
        if(timer != null) {
            timer.StartTimer();
        }
    }

    private IEnumerator DisableUIAfterFrame ( ) {
        yield return null; // Wait for the next frame
        if(uiCanvas != null) {
            uiCanvas.enabled = false;
        }
        if(timer != null) {
            timer.StopTimer();
        }
    }
}
