using System;

namespace byte5.EditingLookContentApp.Models
{
    public class EditingLook
    {
        public int Id { get; set; }

        public string NodeUid { get; set; }

        public int UserId { get; set; }
        
        public DateTime SubscribeDate { get; set; }
    }
}