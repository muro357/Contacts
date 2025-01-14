using Contacts.Maui.Models;
using Contacts.UseCases.Interfaces;
using System.Text;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.Views;

[QueryProperty(nameof(ContactId),"Id")]
public partial class EditContactPage : ContentPage
{
    Contact _contact;
    private readonly IViewContactUseCase viewContactUseCase;
    private readonly IEditContactUseCase editContactUseCase;

    public EditContactPage(IViewContactUseCase viewContactUseCase,IEditContactUseCase editContactUseCase)
	{
		InitializeComponent();
        this.viewContactUseCase = viewContactUseCase;
        this.editContactUseCase = editContactUseCase;
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
		//Shell.Current.GoToAsync("..");
        Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
    }

    

    public string ContactId 
    { 
        set
        {
            //_contact = ContactRepository.GetContactById(int.Parse(value));
            _contact = viewContactUseCase.ExecuteAsync(int.Parse(value)).GetAwaiter().GetResult();
            if ( _contact != null )
            {
                contactCtrl.Name = _contact.Name;
                contactCtrl.Email = _contact.Email;
                contactCtrl.Phone = _contact.Phone;
                contactCtrl.Address = _contact.Address;
            }
        }
    }

    private async void btnUpdate_Clicked(object sender, EventArgs e)
    {
        if(_contact != null )
        {

            var contact = new Contact()
            {
                ContactId=_contact.ContactId,
                Name = contactCtrl.Name,
                Email = contactCtrl.Email,
                Phone = contactCtrl.Phone,
                Address = contactCtrl.Address
            };

            //ContactRepository.UpdateContact(_contact.ContactId, contact);
            await editContactUseCase.ExecuteAsync(_contact.ContactId, contact);
            await Shell.Current.GoToAsync("..");
        }
    }

    private void contactCtrl_OnError(object sender, string e)
    {
        DisplayAlert("Error",e,"OK");
    }
}