using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.UseCases.PluginInterfaces
{
    public interface IContactRepository
    {
        Task AddContactAsync(Contact contact);
        Task DeleteContactAsync(int contactId);
        Task<List<Contact>> GetContactAsync(string filter);
        Task<Contact> GetContactByIdAsync(int contactId);
        Task UpdateContactAsync(int contactId, Contact contact);
    }
}
