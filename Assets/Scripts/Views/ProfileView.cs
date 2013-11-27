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
	public UIInput LoginLabel;
	
	/// <summary>
	/// The name label.
	/// </summary>
	public UIInput NameLabel;
	
	/// <summary>
	/// The email label.
	/// </summary>
	public UIInput EmailLabel;
	
	/// <summary>
	/// The message count label.
	/// </summary>
	public UIInput MessageCountLabel;

	/// <summary>
	/// The save profile button.
	/// </summary>
	public UIButton SaveProfileBtn;

	/// <summary>
	/// The edit profile check box.
	/// </summary>
	public UICheckbox EditProfileCheckBox;
	
	#endregion Propriedades

	private bool m_bCheckBoxStatus;

	#region Metodos
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	private void Start()
	{
		m_bCheckBoxStatus = true;

		SaveProfileBtn.isEnabled = false;

		LoginLabel.gameObject.GetComponent<BoxCollider>().enabled = false;
		NameLabel.gameObject.GetComponent<BoxCollider>().enabled = false;
		EmailLabel.gameObject.GetComponent<BoxCollider>().enabled = false;
		MessageCountLabel.gameObject.GetComponent<BoxCollider>().enabled = false;

		LoginLabel.label.text = "";
		NameLabel.label.text = "";
		EmailLabel.label.text = "";
		MessageCountLabel.label.text = "";
		
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

	public void StartCoUpdateProfile()
	{
		StartCoroutine("CoUpdateProfile");
	}

	/// <summary>
	/// Cos the update profile.
	/// </summary>
	/// <returns>The update profile.</returns>
	private IEnumerator CoUpdateProfile()
	{
		string auxLogin = LoginLabel.label.text;
		string auxName = NameLabel.label.text;
		string auxEmail = EmailLabel.label.text;
		
		LoginView.gameObject.GetComponent<LoginView>().LoginLabel.text = auxLogin;
		
		WWWForm form = new WWWForm();
		
		form.AddField("login", auxLogin);

		form.AddField("name", auxName);
		
		form.AddField("email", auxEmail);
		
		WWW www = new WWW("localhost/updateprofile.php?", form); 
		
		yield return www;
		
		Debug.Log(www.text);
		
		if(www.text.Equals("done"))
		{
			Debug.Log("deu certo =)");

			SaveProfileBtn.isEnabled = false;

			EditProfileCheckBox.isChecked = true;
		}
		else
		{
			
		}
	}

	/// <summary>
	/// Coroutine to remove the account.
	/// </summary>
	/// <returns>The remove acc.</returns>
	public IEnumerator CoRemoveAcc()
	{
		string auxLogin = LoginLabel.label.text;

		LoginView.gameObject.GetComponent<LoginView>().LoginLabel.text = auxLogin;

		WWWForm form = new WWWForm();
		
		form.AddField("login", auxLogin);

		WWW www = new WWW("localhost/removeaccount.php?", form); 
		
		yield return www;
		
		Debug.Log(www.text);
		
		if (www.text.Equals ("done")) 
		{

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

	public void EnableEdit()
	{
		if(!m_bCheckBoxStatus)
		{
			NameLabel.gameObject.GetComponent<BoxCollider>().enabled = true;
			EmailLabel.gameObject.GetComponent<BoxCollider>().enabled = true;

			SaveProfileBtn.isEnabled = true;

			m_bCheckBoxStatus = true;
		}
		else
		{
			NameLabel.gameObject.GetComponent<BoxCollider>().enabled = false;
			EmailLabel.gameObject.GetComponent<BoxCollider>().enabled = false;

			SaveProfileBtn.isEnabled = false;

			m_bCheckBoxStatus = false;
		}
	}
	
	#endregion Metodos
}
