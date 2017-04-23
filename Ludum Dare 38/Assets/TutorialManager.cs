using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

	public static TutorialManager instance;

	public GameObject collectibles;
	public AntController antLeader;
	public Collectible food;
	public Collectible material;
	public FoodBarController foodBar;
	public MravecBar mravecBar;
	public TutorialCanvas finalTutorial;

	private void Awake()
	{
		instance = this;
	}

	IEnumerator Start () {
		collectibles.SetActive(false);
		food.gameObject.SetActive(false);
		material.gameObject.SetActive(false);
		foodBar.gameObject.SetActive(false);
		mravecBar.gameObject.SetActive(false);
		finalTutorial.gameObject.SetActive(false);

		antLeader.tutorial.gameObject.SetActive(true);

		while (antLeader.tutorial.gameObject.activeSelf)
		{
			yield return null;
		}

		food.gameObject.SetActive(true);
		food.tutorial.gameObject.SetActive(true);

		while (food.tutorial.gameObject.activeSelf)
		{
			yield return null;
		}

		foodBar.gameObject.SetActive(true);
		material.gameObject.SetActive(true);
		material.tutorial.gameObject.SetActive(true);

		while (material.tutorial.gameObject.activeSelf)
		{
			yield return null;
		}

		mravecBar.gameObject.SetActive(true);
		finalTutorial.gameObject.SetActive(true);
		collectibles.SetActive(true);
	}
}
