using UnityEngine;
using System.Collections;

public class IView : MonoBehaviour
{
	public virtual void ActivateView()
	{
		this.GetComponent<Camera>().enabled = true;
	}
	
	public virtual void DeactivateView()
	{
		this.GetComponent<Camera>().enabled = false;
	}
}
