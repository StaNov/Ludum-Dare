using UnityEngine;
using UnityEngine.UI;

public class TimeToFinishUpdater : MonoBehaviour
{
	public Text Text;
	public HoveredActionEffect Effect;
	
	void Update ()
	{
		Text.text = Effect.TimeToFinish > 0 ? Effect.TimeToFinish + "s" : "";
	}
}
