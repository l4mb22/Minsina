using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProfileView : IView
{
	#region Propriedades
	
	/// <summary>
	/// The login view.
	/// </summary>
	public IView LoginView;
	
	/// <summary>
	/// The login label.
	/// </summary>
	public UILabel LoginLabel;
	
	/// <summary>
	/// The name label.
	/// </summary>
	public UILabel NameLabel;
	
	/// <summary>
	/// The email label.
	/// </summary>
	public UILabel EmailLabel;
	
	/// <summary>
	/// The message count label.
	/// </summary>
	public UILabel MessageCountLabel;
	
	#endregion Propriedades
	
	#region Metodos
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	private void Start()
	{
		LoginLabel.text = "";
		NameLabel.text = "";
		EmailLabel.text = "";
		MessageCountLabel.text = "";
		
		StopCoroutine("CoProfile");
		StartCoroutine("CoProfile");
	}
	
	/// <summary>
	/// Cos the profile.
	/// </summary>
	/// <returns>
	/// The profile.
	/// </returns>
	private IEnumerator CoProfile()
	{
		WWWForm form = new WWWForm();
		
		form.AddField("login", LoginView.gameObject.GetComponent<LoginView>().LoginLabel.text);
		
		WWW www = new WWW("localhost/get_profile.php?", form);
		
		float elapsedTime = 0.0f;
		
		while (!www.isDone) 
		{
			elapsedTime += Time.deltaTime;
			
			if (elapsedTime >= 20.0f) 
			{				
				Debug.LogError("TimeOut");
				
				break;
			}
			
			yield return null;  
		}
		
		if (!www.isDone || !string.IsNullOrEmpty(www.error))
		{
			yield break;
		}
		
		if(www.text.Contains("error") || www.text.Contains("null"))
		{
			Debug.Log("Erro");
			
			yield break;
		}
		
		string response = www.text;
		
		IList<object> infos = (IList<object>) MiniJSON.Json.Deserialize(response);
		
		foreach (IDictionary info in infos) 
		{
			LoginLabel.text = info["login"].ToString();
			NameLabel.text = info["name"].ToString();
			EmailLabel.text = info["email"].ToString();
		}
	}
	
	/// <summary>
	/// Metodo chamado pelo botao de sair.
	/// </summary>
	public void LogoutButton()
	{
		this.DeactivateView();
		
		LoginView.gameObject.SetActive(true);
		
		LoginView.ActivateView();
		
		this.gameObject.SetActive(false);
	}
	
	#endregion Metodos
}
