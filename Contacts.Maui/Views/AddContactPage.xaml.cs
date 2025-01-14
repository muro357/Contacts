//using Contacts.Maui.Models;
using Contacts.UseCases.Interfaces;
using Contact = Contacts.CoreBusiness.Contact;
namespace Contacts.Maui.Views;

public partial class AddContactPage : ContentPage
{
    private readonly IAddContactUseCase addContactUseCase;

    public AddContactPage(IAddContactUseCase addContactUseCase)
	{
		InitializeComponent();
        this.addContactUseCase = addContactUseCase;
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
        //Tambien se puede ocupar la opcion de abajo pero se tiene que usar ruta absoluta ya que es la pagina o pantalla de inicio
        //sino marca error
        //Shell.Current.GoToAsync($"//{nameof(ContactsPage)}"); 
    }

    private async void contactCtrl_OnSave(object sender, EventArgs e)
    {
        var newContact = new Contact()
        {
            Name=contactCtrl.Name,
            Email=contactCtrl.Email,
            Phone=contactCtrl.Phone,
            Address=contactCtrl.Address,
        };

        //ContactRepository.AddContact(newContact);
        await addContactUseCase.ExecuteAsync(newContact);
        await Shell.Current.GoToAsync("..");
    }

    private void contactCtrl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}