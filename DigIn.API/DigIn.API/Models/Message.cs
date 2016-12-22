using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigIn.API.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ApplicationUser Author { get; set; }
        public List<ApplicationUser> Reciever { get; set; }
        public DateTime CreatedTime { get; set; }
        public MessageType MessageType { get; set; }
        public ProjectModel Project { get; set; }
        public int ProjectID { get; set; }
    }

    public class MessageStatus
    {
        public int ID { get; set; }
        public ApplicationUser User { get; set; }
        public Message Message { get; set; }
        public bool MessageRead { get; set; }
    }

    public enum MessageType
    {
        ProjectInvite,
        PersonalMessage,
        GroupMessage
    }
}