using Microsoft.EntityFrameworkCore;
using Contacts.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Contacts.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(option => 
            option.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            //app.UseAuthorization();


            app.MapGet("api/contacts/{id}", async (int id, ApplicationDbContext db) =>
            {
                var contact = db.Contacts.FirstOrDefault(x=> x.ContactId== id);

                return Results.Ok(contact);
            });

            app.MapGet("api/contacts", async ([FromQuery]string? s,ApplicationDbContext db) =>
            {
                List<Contact> lista;

                if(string.IsNullOrWhiteSpace(s))
                {
                    lista = await db.Contacts.ToListAsync();
                }
                else
                {
                    lista = await db.Contacts.Where(x => 
                        (!string.IsNullOrWhiteSpace(x.Name) && x.Name.ToLower().IndexOf(s.ToLower()) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.Email) && x.Email.ToLower().IndexOf(s.ToLower()) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.Phone) && x.Phone.ToLower().IndexOf(s.ToLower()) >= 0) ||
                        (!string.IsNullOrWhiteSpace(x.Address) && x.Address.ToLower().IndexOf(s.ToLower()) >= 0) 
                    ).ToListAsync();
                }

                //var contacts = await db.Contacts.ToListAsync();

                return Results.Ok(lista);
            });

            app.MapPost("api/contacts", async(Contact contact, ApplicationDbContext db) =>
            {
                db.Contacts.Add(contact);
                await db.SaveChangesAsync();
            });

            app.MapPut("api/contacts/{id}", async (int id, Contact contact, ApplicationDbContext db) =>
            {
                var contactToUpdate = await db.Contacts.FindAsync(id);

                if(contactToUpdate == null)
                {
                    return Results.NotFound();
                }
                else
                {
                    contactToUpdate.Name = contact.Name;
                    contactToUpdate.Email = contact.Email;
                    contactToUpdate.Phone = contact.Phone;
                    contactToUpdate.Address = contact.Address;

                    await db.SaveChangesAsync();

                    return Results.NoContent();
                }
            });

            app.MapDelete("api/contacts/{id}", async (int id, ApplicationDbContext db) =>
            {
                var contactToDelete = await db.Contacts.FindAsync(id);

                if (contactToDelete != null)
                {
                    db.Contacts.Remove(contactToDelete);    
                    await db.SaveChangesAsync();
                    return Results.Ok(contactToDelete);
                }

                return Results.NotFound(); 
                
            });

            app.Run();
        }
    }
}
