using UnityEngine;
using System.Collections;

public class LoginView : IView
{
	#region Propriedades
	
	/// <summary>
	/// The profile view.
	/// </summary>
	public IView ProfileView;
	
	/// <summary>
	/// The register view.
	/// </summary>
	public IView RegisterView;

	public IView HomeView;
	
	/// <summary>
	/// The login label.
	/// </summary>
	public UILabel LoginLabel;
	
	/// <summary>
	/// The password label.
	/// </summary>
	public UILabel PasswordLabel;
	
	#endregion Propriedades
	
	#region Metodos
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	private void Start()
	{
		LoginLabel.text = "";
		PasswordLabel.text = "";
	}
	
	/// <summary>
	/// Metodo chamado pelo botao de login
	/// </summary>
	public void LoginButton()
	{
		StopCoroutine("CoLogin");
		StartCoroutine("CoLogin");
	}
	
	/// <summary>
	/// Cos the login.
	/// </summary>
	/// <returns>
	/// The login.
	/// </returns>
	private IEnumerator CoLogin()
	{
		string auxLogin = LoginLabel.text;
		string auxPassword = PasswordLabel.text;
		
		WWWForm form = new WWWForm();
		
		form.AddField("login", auxLogin);
		
		form.AddField("password", auxPassword);
		
		WWW www = new WWW("localhost/login.php?", form); 
		
		yield return www;
		
		
		
		if(www.text.Equals("success"))
		{
			this.DeactivateView();
		
			HomeView.gameObject.SetActive(true);
			
			HomeView.ActivateView();
			
			this.gameObject.SetActive(false);
		}
		else
		{
			Debug.Log(www.text + "          " + www.error);			
		}
	}
	
	/// <summary>
	/// Metodo chamado pelo botao de cadastrar
	/// </summary>
	public void RegisterButton()
	{
		this.DeactivateView();
		
		RegisterView.gameObject.SetActive(true);
		
		RegisterView.ActivateView();
		
		this.gameObject.SetActive(false);
	}
	
	#endregion Metodos
}
