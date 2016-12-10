using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

	public CollectibleType Type;
}

public enum CollectibleType {
	STAR,
	TREE,
	SNOWFLAKE,
	LAMP,
	CANDLES
}
