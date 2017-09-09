using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	public AudioSource Source;
	public AudioClip MainTune;
	public AudioClip StressTune;
	public AudioClip EndTune;
	public GameStateHolder State;

	void Start()
	{
		Source.clip = MainTune;
		Source.Play();
	}

	void Update()
	{
		if (State.State.GameOver != null)
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
		return State.State.MyEnergy < DangerLimit
			|| State.State.MyFood < DangerLimit
			|| State.State.MyHealth < DangerLimit
			|| State.State.MyHappiness < DangerLimit
			|| State.State.FamilyHappiness < DangerLimit
			|| State.State.FamilyHealth < DangerLimit
			|| State.State.FamilyFood < DangerLimit;
	}

	private bool IsOk()
	{
		return State.State.MyEnergy > OkLimit
		       && State.State.MyFood > OkLimit
		       && State.State.MyHealth > OkLimit
		       && State.State.MyHappiness > OkLimit
		       && State.State.FamilyHappiness > OkLimit
		       && State.State.FamilyHealth > OkLimit
		       && State.State.FamilyFood > OkLimit;
	}

}
