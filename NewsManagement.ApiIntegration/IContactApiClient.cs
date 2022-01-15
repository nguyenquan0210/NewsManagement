using NewsManagement.ViewModels.Catalog.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ApiIntegration
{
    public interface IContactApiClient
    {
        Task<bool> Update(UpdateContactRequest request);

        Task<ContactVm> GetById();
    }
}
