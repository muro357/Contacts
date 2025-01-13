using Contacts.Maui.Models;
using System.Text;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.Views;

[QueryProperty(nameof(ContactId),"Id")]
public partial class EditContactPage : ContentPage
{
    Contact _contact;
	public EditContactPage()
	{
		InitializeComponent();
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
            _contact = ContactRepository.GetContactById(int.Parse(value));
            if ( _contact != null )
            {
                contactCtrl.Name = _contact.Name;
                contactCtrl.Email = _contact.Email;
                contactCtrl.Phone = _contact.Phone;
                contactCtrl.Address = _contact.Address;
            }
        }
    }

    private void btnUpdate_Clicked(object sender, EventArgs e)
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

            ContactRepository.UpdateContact(_contact.ContactId, contact);
            Shell.Current.GoToAsync("..");
        }
    }

    private void contactCtrl_OnError(object sender, string e)
    {
        DisplayAlert("Error",e,"OK");
    }
}