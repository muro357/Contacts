using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Plugins.DataStore.InMemory
{
    public class ContactInMemoryRepository : UseCases.PluginInterfaces.IContactRepository
    {
        private readonly List<Contact> _contacts;

        public ContactInMemoryRepository()
        {
             _contacts = new List<Contact>()
            {
                new Contact{ ContactId = 1, Name = "John Doe", Email="john@doe.com" },
                new Contact{ContactId = 2, Name="Jane Doe", Email="jane@doe.com" },
                new Contact{ContactId = 3, Name="Tom Hanks", Email="tom@hanks.com" },
                new Contact{ContactId = 4, Name="Frank Liu", Email="frank@liu.com" },
                new Contact{ContactId=5, Name="James Watts",Email="james@watts.com" }
            };
        }

        public Task AddContactAsync(Contact contact)
        {
            var maxId = _contacts.Max(x => x.ContactId);
            contact.ContactId = maxId + 1;
            _contacts.Add(contact);
            return Task.CompletedTask;
        }

        public Task DeleteContactAsync(int contactId)
        {
            var contact = _contacts.FirstOrDefault(x => x.ContactId == contactId);
            if (contact != null)
            {
                _contacts.Remove(contact);
            }

            return Task.CompletedTask;
        }

        public Task<List<Contact>> GetContactAsync(string filter)
        {
            if(string.IsNullOrWhiteSpace(filter))
            {
                return Task.FromResult(_contacts);
            }

            var contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.StartsWith(filter, StringComparison.OrdinalIgnoreCase))?.ToList();

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Email) && x.Email.StartsWith(filter, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
                return Task.FromResult(contacts);

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Phone) && x.Phone.StartsWith(filter, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
                return Task.FromResult(contacts);

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Address) && x.Address.StartsWith(filter, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
                return Task.FromResult(contacts);

            return Task.FromResult(contacts);
        }

        public Task<Contact> GetContactByIdAsync(int contactId)
        {
            var contact = _contacts.FirstOrDefault(x => x.ContactId == contactId);

            if (contact != null)
            {
                return Task.FromResult(contact);
            }
            else
                return null;

        }

        public Task UpdateContactAsync(int contactId, Contact contact)
        {
            if (contactId != contact.ContactId) return Task.CompletedTask;

            var contactToUpdate = _contacts.FirstOrDefault(x => x.ContactId == contactId);

            if (contactToUpdate != null)
            {
                contactToUpdate.Name = contact.Name;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Phone = contact.Phone;
                contactToUpdate.Address = contact.Address;
            }

            return Task.CompletedTask;
        }
    }

    
}
