using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	public AudioSource Source;
	public AudioClip MainTune;
	public AudioClip StressTune;
	public AudioClip EndTune;
	public GameState State;

	void Start ()
	{
		Source.clip = MainTune;
		Source.Play();
	}
	
	void Update ()
	{
		if (State.GameOver != GameOverReason.StillPlaying)
		{
			Source.clip = EndTune;
			Source.Play();
			Source.loop = false;
			enabled = false;
			return;
		}

		if (State.MyEnergy < 15 && Source.clip == MainTune)
		{
			Source.clip = StressTune;
			Source.Play();
		}

		if (State.MyEnergy > 20 && Source.clip == StressTune)
		{
			Source.clip = MainTune;
			Source.Play();
		}
	}
}
