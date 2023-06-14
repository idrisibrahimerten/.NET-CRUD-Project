using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;
        public ContactsController(ContactsAPIDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contact.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(Models.AddContactRequest addContactRequest) 
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone
            };

            await dbContext.Contact.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> PutContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.Contact.FindAsync(id);

            if (contact != null)
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Address = updateContactRequest.Address;
                contact.Phone = updateContactRequest.Phone;
                contact.Email = updateContactRequest.Email;

                await dbContext.SaveChangesAsync();
                return Ok(contact);

            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contact.FindAsync(id);

            if (contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
            }

            return NotFound();
        }

    }
}
