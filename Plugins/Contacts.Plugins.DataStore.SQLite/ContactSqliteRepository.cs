using Contacts.UseCases.PluginInterfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Plugins.DataStore.SQLite
{
    public class ContactSqliteRepository : IContactRepository
    {
        private SQLiteAsyncConnection database;

        public ContactSqliteRepository()
        {
            this.database = new SQLiteAsyncConnection(Constants.DatabasePath);
            this.database.CreateTableAsync<Contact>();  
        }
        public async Task AddContactAsync(CoreBusiness.Contact contact)
        {
            await database.InsertAsync(contact);
        }

        public async Task DeleteContactAsync(int contactId)
        {
            var contact = GetContactByIdAsync(contactId);
            await database.DeleteAsync(contact);
        }

        public async Task<List<CoreBusiness.Contact>> GetContactAsync(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return await this.database.Table<Contact>().ToListAsync();
            else
                return await this.database.QueryAsync<Contact>(@"
                    SELECT * FROM Contact 
                    WHERE 
                        Name Like ? OR 
                        Email Like ? OR 
                        Phone Like ? OR 
                        Address LIKE ?",
                        $"{filter}%",
                        $"{filter}%",
                        $"{filter}%",
                        $"{filter}%");
        }
                

        public async Task<CoreBusiness.Contact> GetContactByIdAsync(int contactId)
        {
            return await this.database.Table<Contact>().Where(x => x.ContactId == contactId).FirstOrDefaultAsync();
        }

        public async Task UpdateContactAsync(int contactId, CoreBusiness.Contact contact)
        {
            if(contactId == contact.ContactId)
            {
                await this.database.UpdateAsync(contact);
            }
        }
    }
}
