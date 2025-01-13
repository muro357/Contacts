using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Maui.Models
{
    public static class ContactRepository
    {
        public static List<Contact> _contacts = new List<Contact>()
        {
            new Contact{ ContactId = 1, Name = "John Doe", Email="john@doe.com" },
            new Contact{ContactId = 2, Name="Jane Doe", Email="jane@doe.com" },
            new Contact{ContactId = 3, Name="Tom Hanks", Email="tom@hanks.com" },
            new Contact{ContactId = 4, Name="Frank Liu", Email="frank@liu.com" },
            new Contact{ContactId=5, Name="James Watts",Email="james@watts.com" }
        };


        public static List<Contact> GetContacts() => _contacts;

        public static Contact GetContactById(int Id)
        {
            return _contacts.FirstOrDefault(x => x.ContactId == Id);
        }

        public static void UpdateContact(int Id, Contact contact)
        {
            if (Id != contact.ContactId) return;
            
                var contactToUpdate = _contacts.FirstOrDefault(x => x.ContactId == Id);

                if(contactToUpdate != null)
                {
                    contactToUpdate.Name = contact.Name;
                    contactToUpdate.Email = contact.Email;
                    contactToUpdate.Phone = contact.Phone;
                    contactToUpdate.Address = contact.Address;
                }
        }

        public static void AddContact(Contact contact)
        {
            var maxId = _contacts.Max(x => x.ContactId);
            contact.ContactId = maxId+1;
            _contacts.Add(contact);
        }

        public static void DeleteContact(int Id)
        {
            var contact = _contacts.FirstOrDefault(x =>x.ContactId == Id);
            if (contact != null)
            {
                _contacts.Remove(contact);
            }
        }

        public static List<Contact> SearchContacts(string filter)
        {
            var contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.StartsWith(filter,StringComparison.OrdinalIgnoreCase))?.ToList();

            if(contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Email) && x.Email.StartsWith(filter, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
                return contacts;

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Phone) && x.Phone.StartsWith(filter, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
                return contacts;

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Address) && x.Address.StartsWith(filter, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
                return contacts;

            return contacts;
        }
    }
}
