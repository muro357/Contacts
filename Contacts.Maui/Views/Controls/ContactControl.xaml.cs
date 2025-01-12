namespace Contacts.Maui.Views.Controls;

public partial class ContactControl : ContentView
{
	public event EventHandler<string> OnError;

	public ContactControl()
	{
		InitializeComponent();
	}

    public string Name 
	{
		get => entryName.Text;
		set => entryName.Text = value; 
	}

    public string Email 
	{
		get => entryEmail.Text; 
		set => entryEmail.Text = value;
	}

    public string Phone 
	{ 
		get => entryPhone.Text;
		set => entryPhone.Text = value; 
	}

    private void btnSave_Clicked(object sender, EventArgs e)
    {

    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {

    }
}