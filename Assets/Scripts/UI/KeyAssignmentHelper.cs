using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyAssignmentHelper : MonoBehaviour
{
	public Button[] buttons;// children;

	private void Start()
	{
		//children = GetComponentsInChildren<Button>();
		updateButtonText();
		
	}
	public void AssignKey(int index)
	{
		Controls.Instance.UpdateKeyOfIndex(index);
	}

	private void Update()
	{
		updateButtonText();
	}

	private void updateButtonText()
	{
		buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = Controls.Instance.Left.ToString();
		buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = Controls.Instance.Right.ToString();
		buttons[2].GetComponentInChildren<TextMeshProUGUI>().text = Controls.Instance.Up.ToString();
		buttons[3].GetComponentInChildren<TextMeshProUGUI>().text = Controls.Instance.Down.ToString();
		buttons[4].GetComponentInChildren<TextMeshProUGUI>().text = Controls.Instance.Fire.ToString();
		buttons[5].GetComponentInChildren<TextMeshProUGUI>().text = Controls.Instance.Interact.ToString();
		buttons[6].GetComponentInChildren<TextMeshProUGUI>().text = Controls.Instance.Transform.ToString();
	}
}
