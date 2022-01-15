using NewsManagement.Data.EF;
using NewsManagement.Utilities.Exceptions;
using NewsManagement.ViewModels.Catalog.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Catalog.Contacts
{
    public class ContactService : IContactService
    {
        private readonly DBContext _context;

        public ContactService(DBContext context)
        {
            _context = context;
        }
        public async Task<ContactVm> GetById()
        {
            var contact = await _context.Contacts.FindAsync(1);

            var rs = new ContactVm()
            {
                Email = contact.Email,
                Company = contact.Company,
                Position = contact.Position,
                Address = contact.Address,
                Leader = contact.Leader,
                License = contact.License,
                Hotline = contact.Hotline,
                ContactAdvertise = contact.Contact_Advertise
            };

            return rs;
        }

        public async Task<int> Update(UpdateContactRequest request)
        {
            var contact = await _context.Contacts.FindAsync(request.Id);
            if (contact == null) throw new NewsManageException($"Cannot find a Contact with id: { request.Id}");
            contact.Email = request.Email;
            contact.Company = request.Company;
            contact.Position = request.Position;
            contact.Address = request.Address;
            contact.License = WebUtility.HtmlDecode(request.License);
            contact.Leader = request.Leader;
            contact.Hotline = request.Hotline;
            contact.Contact_Advertise = request.ContactAdvertise;
            return await _context.SaveChangesAsync();
        }
    }
}
