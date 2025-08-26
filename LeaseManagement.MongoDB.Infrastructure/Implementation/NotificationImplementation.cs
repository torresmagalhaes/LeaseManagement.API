using LeaseManagement.Domain.Entities.MongoDB;
using LeaseManagement.Domain.MongoDB.Entities;
using LeaseManagement.Infrastructure.MongoDB.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseManagement.Infrastructure.MongoDB.Implementation
{
    public class NotificationImplementation
    {
        private readonly IMongoCollection<NotificationDocument> _notificationCollection;

        public NotificationImplementation(MongoDBService mongoDBService)
        {
            _notificationCollection = mongoDBService.Database.GetCollection<NotificationDocument>("NotificationDocument");
        }

        public void InsertNotification(NotificationDocument notificationDocument)
        {
            _notificationCollection.InsertOne(notificationDocument);
        }
    }
}
