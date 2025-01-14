
//using Contacts.Maui.Models;
using Contacts.UseCases.Interfaces;
using System.Collections.ObjectModel;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.Views;

public partial class ContactsPage : ContentPage
{
    private readonly IViewContactsUseCase viewContactsUseCase;
    private readonly IDeleteContactUseCase deleteContactUseCase;

    public ContactsPage(IViewContactsUseCase viewContactsUseCase, IDeleteContactUseCase deleteContactUseCase)
	{
		InitializeComponent();
        this.viewContactsUseCase = viewContactsUseCase;
        this.deleteContactUseCase = deleteContactUseCase;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        SearchBar.Text= string.Empty;
        LoadContacts();
    }

    private async void listContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if(listContacts.SelectedItem != null)
        {
            //logic
            var id = ((Contact)listContacts.SelectedItem).ContactId;
            await Shell.Current.GoToAsync($"{nameof(EditContactPage)}?Id={id}");
        }
    }

    private void listContacts_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        listContacts.SelectedItem = null;
    }

    private async void btnAdd_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddContactPage));
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var contact = menuItem.CommandParameter as Contact;
        //ContactRepository.DeleteContact(contact.ContactId);
        await deleteContactUseCase.ExecuteAsync(contact.ContactId);
        LoadContacts();
    }

    private async void LoadContacts()
    {
        var contacts = new ObservableCollection<Contact>(await this.viewContactsUseCase.ExecuteAsync(string.Empty));
        listContacts.ItemsSource = contacts;
    }

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var filter = ((SearchBar)sender).Text;
        var contacts = new ObservableCollection<Contact>(await this.viewContactsUseCase.ExecuteAsync(filter));
        listContacts.ItemsSource = contacts;
    }

    private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        DisplayAlert("Aviso","No Implementado","OK");
    }
}

