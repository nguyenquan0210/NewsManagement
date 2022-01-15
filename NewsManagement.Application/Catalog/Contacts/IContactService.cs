using NewsManagement.ViewModels.Catalog.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Catalog.Contacts
{
    public interface IContactService
    {
        
        Task<int> Update(UpdateContactRequest request);

        Task<ContactVm> GetById();
    }
}
