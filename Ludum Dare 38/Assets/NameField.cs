using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameField : MonoBehaviour {

	private InputField field;

	public string fieldText { get
		{
			return field.text;
		}
	}
	
	void Start () {
		field = GetComponent<InputField>();
		field.text = NameManager.Name;
	}
}
