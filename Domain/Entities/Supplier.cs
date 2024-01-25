using System;
using Flunt.Br;
using Flunt.Br.Extensions;
using Flunt.Notifications;

namespace Domain.Entities
{
    public class Supplier : Notifiable<Notification>
    {
        public int ID { get; set; }
        public String Description { get; set; }
        public String CNPJ { get; set; }
        
        public void Validate()
        {
            AddNotifications(new Contract()
                    .IsCnpj(CNPJ, nameof(CNPJ), "The CNPJ is invalid.")
                    .IsNotNullOrEmpty(Description, nameof(Description), "The description cannot be null or empty.")
            );
        }
    }
}