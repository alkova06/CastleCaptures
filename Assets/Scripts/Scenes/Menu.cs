using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	Button[] buttons = new Button[4];
	void Start () 
	{
		buttons = GetComponentsInChildren<Button> ();
		buttons [3].PressButton += PlayButtonPress;
	}

	void PlayButtonPress ()
	{
		Application.LoadLevel ("map");
	}
	
	void Update () 
	{
			
	}
}
