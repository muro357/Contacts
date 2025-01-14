using Contacts.UseCases.Interfaces;
using Contacts.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.UseCases
{
    public class ViewContactsUseCase : IViewContactsUseCase
    {
        private readonly IContactRepository contactRepository;
        public ViewContactsUseCase(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task<List<Contact>> ExecuteAsync(string filter)
        {
            return await contactRepository.GetContactAsync(filter);
        }
    }
}
