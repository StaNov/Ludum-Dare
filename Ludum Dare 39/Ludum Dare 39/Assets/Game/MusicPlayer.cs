using GameOfLife.GameLogic;
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
		return State.State.GetStateItemValue<float>(StateItemType.MyEnergy.ToString()) < DangerLimit
			|| State.State.GetStateItemValue<float>(StateItemType.MyFood.ToString()) < DangerLimit
			|| State.State.GetStateItemValue<float>(StateItemType.MyHealth.ToString()) < DangerLimit
			|| State.State.GetStateItemValue<float>(StateItemType.MyHappiness.ToString()) < DangerLimit
			|| State.State.GetStateItemValue<float>(StateItemType.FamilyHappiness.ToString()) < DangerLimit
			|| State.State.GetStateItemValue<float>(StateItemType.FamilyHealth.ToString()) < DangerLimit
			|| State.State.GetStateItemValue<float>(StateItemType.FamilyFood.ToString()) < DangerLimit;
	}

	private bool IsOk()
	{
		return State.State.GetStateItemValue<float>(StateItemType.MyEnergy.ToString()) > OkLimit
		       && State.State.GetStateItemValue<float>(StateItemType.MyFood.ToString()) > OkLimit
		       && State.State.GetStateItemValue<float>(StateItemType.MyHealth.ToString()) > OkLimit
		       && State.State.GetStateItemValue<float>(StateItemType.MyHappiness.ToString()) > OkLimit
		       && State.State.GetStateItemValue<float>(StateItemType.FamilyHappiness.ToString()) > OkLimit
		       && State.State.GetStateItemValue<float>(StateItemType.FamilyHealth.ToString()) > OkLimit
		       && State.State.GetStateItemValue<float>(StateItemType.FamilyFood.ToString()) > OkLimit;
	}

}
