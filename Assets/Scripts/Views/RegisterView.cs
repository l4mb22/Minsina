using UnityEngine;
using System.Collections;

public class RegisterView : IView
{
	#region Propriedades
	
	/// <summary>
	/// The profile view.
	/// </summary>
	public IView ProfileView;
	
	/// <summary>
	/// The register view.
	/// </summary>
	public IView LoginView;
	
	/// <summary>
	/// The login label.
	/// </summary>
	public UILabel LoginLabel;
	
	/// <summary>
	/// The password label.
	/// </summary>
	public UILabel PasswordLabel;
	
	/// <summary>
	/// The name label.
	/// </summary>
	public UILabel NameLabel;
	
	/// <summary>
	/// The email label.
	/// </summary>
	public UILabel EmailLabel;
	
	#endregion Propriedades
	
	#region Metodos
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	private void Start()
	{
		LoginLabel.text = "";
		PasswordLabel.text = "";
		NameLabel.text = "";
		EmailLabel.text = "";
	}
	
	/// <summary>
	/// Metodo chamado pelo botao de login
	/// </summary>
	public void BackButton()
	{
		this.DeactivateView();
		
		LoginView.gameObject.SetActive(true);
		
		LoginView.ActivateView();
		
		this.gameObject.SetActive(false);
	}
	
	/// <summary>
	/// Metodo chamado pelo botao de cadastrar
	/// </summary>
	public void RegisterButton()
	{
		StopCoroutine("CoRegister");
		StartCoroutine("CoRegister");
	}
	
	/// <summary>
	/// Cos the register.
	/// </summary>
	/// <returns>
	/// The register.
	/// </returns>
	private IEnumerator CoRegister()
	{
		string auxLogin = LoginLabel.text;
		string auxPassword = PasswordLabel.text;
		string auxName = NameLabel.text;
		string auxEmail = EmailLabel.text;
		
		LoginView.gameObject.GetComponent<LoginView>().LoginLabel.text = auxLogin;
		
		WWWForm form = new WWWForm();
		
		form.AddField("login", auxLogin);
		
		form.AddField("password", auxPassword);
		
		form.AddField("name", auxName);
		
		form.AddField("email", auxEmail);
		
		WWW www = new WWW("localhost/register.php?", form); 
		
		yield return www;
		
		Debug.Log(www.text);
		
		if(www.text.Equals("done"))
		{
			this.DeactivateView();
			
			LoginView.gameObject.SetActive(true);

			LoginView.ActivateView();
			
			this.gameObject.SetActive(false);
		}
		else
		{
			
		}
	}
	
	#endregion Metodos
}
