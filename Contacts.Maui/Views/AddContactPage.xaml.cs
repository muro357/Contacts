namespace Contacts.Maui.Views;

public partial class AddContactPage : ContentPage
{
	public AddContactPage()
	{
		InitializeComponent();
	}

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
        //Tambien se puede ocupar la opcion de abajo pero se tiene que usar ruta absoluta ya que es la pagina o pantalla de inicio
        //sino marca error
        //Shell.Current.GoToAsync($"//{nameof(ContactsPage)}"); 
    }
}