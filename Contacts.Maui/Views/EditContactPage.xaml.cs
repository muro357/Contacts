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
                entryName.Text = _contact.Name;
                entryEmail.Text = _contact.Email;
                entryPhone.Text = _contact.Phone;
                entryAddress.Text = _contact.Address;
            }
        }
    }

    private void btnUpdate_Clicked(object sender, EventArgs e)
    {
        if(_contact != null )
        {

            if(nameValidator.IsNotValid)
            {
                DisplayAlert("Error","Name is required","OK");
                return;
            }

            if(emailValidator.IsNotValid)
            {
                StringBuilder listErrors = new StringBuilder();
                foreach (var error in emailValidator.Errors)
                {
                    listErrors.AppendLine(error.ToString());
                }

                DisplayAlert("Error",listErrors.ToString(),"OK");
                return;
            }

            var contact = new Contact()
            {
                ContactId=_contact.ContactId,
                Name = entryName.Text,
                Email = entryEmail.Text,
                Phone = entryPhone.Text,
                Address = entryAddress.Text
            };

            ContactRepository.UpdateContact(_contact.ContactId, contact);
            Shell.Current.GoToAsync("..");
        }
    }
}