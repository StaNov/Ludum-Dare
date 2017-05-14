using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSlot : MonoBehaviour {

	public bool isPopulated { get { return transform.childCount > 0; } }

}
