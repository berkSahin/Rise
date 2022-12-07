using ContactService.Entities.Contact;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ContactService.Data
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>();

                if (!context.ContactInfoTypes.Any())
                {
                    context.ContactInfoTypes.Add(new ContactInfoType() { Value = "Phone Number" });
                    context.ContactInfoTypes.Add(new ContactInfoType() { Value = "E-Mail" });
                    context.ContactInfoTypes.Add(new ContactInfoType() { Value = "Location" });
                    context.SaveChanges();
                }

                if (!context.Contacts.Any())
                {
                    context.Contacts.Add(new Contact() { Name = "Sample", Surname = "sample", Company = "red" });
                    context.Contacts.Add(new Contact() { Name = "Sample", Surname = "sample", Company = "blue" });
                    context.Contacts.Add(new Contact() { Name = "Sample", Surname = "sample", Company = "green" });
                    context.SaveChanges();
                }

                if (!context.ContactInfos.Any())
                {
                    context.ContactInfos.Add(new ContactInfo
                    {
                        ContactId = 1,
                        ContactInfoTypeId = 3,
                        Value = "Ank"
                    });

                    context.ContactInfos.Add(new ContactInfo
                    {
                        ContactId = 2,
                        ContactInfoTypeId = 3,
                        Value = "Ank"
                    });

                    context.ContactInfos.Add(new ContactInfo
                    {
                        ContactId = 3,
                        ContactInfoTypeId = 3,
                        Value = "Ist"
                    });

                    context.ContactInfos.Add(new ContactInfo
                    {
                        ContactId = 1,
                        ContactInfoTypeId = 1,
                        Value = "123"
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}