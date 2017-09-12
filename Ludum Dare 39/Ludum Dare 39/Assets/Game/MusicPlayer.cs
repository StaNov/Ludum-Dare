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
		return State.State.GetStateItemValue(StateItemType.MyEnergy) < DangerLimit
			|| State.State.GetStateItemValue(StateItemType.MyFood) < DangerLimit
			|| State.State.GetStateItemValue(StateItemType.MyHealth) < DangerLimit
			|| State.State.GetStateItemValue(StateItemType.MyHappiness) < DangerLimit
			|| State.State.GetStateItemValue(StateItemType.FamilyHappiness) < DangerLimit
			|| State.State.GetStateItemValue(StateItemType.FamilyHealth) < DangerLimit
			|| State.State.GetStateItemValue(StateItemType.FamilyFood) < DangerLimit;
	}

	private bool IsOk()
	{
		return State.State.GetStateItemValue(StateItemType.MyEnergy) > OkLimit
		       && State.State.GetStateItemValue(StateItemType.MyFood) > OkLimit
		       && State.State.GetStateItemValue(StateItemType.MyHealth) > OkLimit
		       && State.State.GetStateItemValue(StateItemType.MyHappiness) > OkLimit
		       && State.State.GetStateItemValue(StateItemType.FamilyHappiness) > OkLimit
		       && State.State.GetStateItemValue(StateItemType.FamilyHealth) > OkLimit
		       && State.State.GetStateItemValue(StateItemType.FamilyFood) > OkLimit;
	}

}
