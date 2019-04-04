using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace byte5.EditingLookContentApp.Models
{
    public class EditingLook
    {
        public int Id { get; set; }

        public string NodeUid { get; set; }

        public int UserId { get; set; }
        
        public DateTime SubscribeDate { get; set; }


        public EditingLook() { }

        public EditingLook(int id, string nodeUid, int userId, DateTime subscribeDate)
        {
            this.Id = id;
            this.NodeUid = nodeUid;
            this.UserId = UserId;
            this.SubscribeDate = subscribeDate;
        }
    }
}