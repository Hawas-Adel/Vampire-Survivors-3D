using UnityEngine;

public class UIBillboarding : MonoBehaviour
{
	private void Update()
	{
		transform.forward = Camera.main.transform.forward;
	}
}
