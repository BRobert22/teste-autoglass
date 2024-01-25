using System;
using Flunt.Notifications;
using Contract = Flunt.Br.Contract;

namespace Domain.Entities
{
    public class Product : Notifiable<Notification>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte Situation { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Supplier Supplier { get; set; }


        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Description, nameof(Description), "The description cannot be null or empty.")
                .IsLowerThan(ManufactureDate, DateTime.Now, nameof(ManufactureDate), "Manufacturing date cannot be later than the current date.")
                .IsGreaterThan(ExpirationDate, DateTime.Now, nameof(ExpirationDate), "Expiration date cannot be earlier than the current date.")
                .IsGreaterThan(ExpirationDate, ManufactureDate, nameof(ExpirationDate), "Expiration date must be later than the manufacturing date.")
            );
        }
    }
}