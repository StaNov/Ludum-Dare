using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	public AudioSource Source;
	public AudioClip MainTune;
	public AudioClip StressTune;
	public AudioClip EndTune;
	public GameState State;

	void Start()
	{
		Source.clip = MainTune;
		Source.Play();
	}

	void Update()
	{
		if (State.GameOver != GameOverReason.StillPlaying)
		{
			Source.clip = EndTune;
			Source.Play();
			Source.loop = false;
			Source.volume = 0.5f;
			enabled = false;
			return;
		}

		if (IsInDanger() && Source.clip == MainTune)
		{
			Source.clip = StressTune;
			Source.Play();
		}

		if (IsOk() && Source.clip == StressTune)
		{
			Source.clip = MainTune;
			Source.Play();
		}
	}

	private static int DangerLimit = 15;
	private static int OkLimit = 20;

	private bool IsInDanger()
	{
		return State.MyEnergy < DangerLimit
			|| State.MyFood < DangerLimit
			|| State.MyHealth < DangerLimit
			|| State.MyHappiness < DangerLimit
			|| State.FamilyHappiness < DangerLimit
			|| State.FamilyHealth < DangerLimit
			|| State.FamilyFood < DangerLimit;
	}

	private bool IsOk()
	{
		return State.MyEnergy > OkLimit
		       && State.MyFood > OkLimit
		       && State.MyHealth > OkLimit
		       && State.MyHappiness > OkLimit
		       && State.FamilyHappiness > OkLimit
		       && State.FamilyHealth > OkLimit
		       && State.FamilyFood > OkLimit;
	}

}
