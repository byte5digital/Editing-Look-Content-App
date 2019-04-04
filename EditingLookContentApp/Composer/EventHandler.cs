using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;

namespace byte5.EditingLookContentApp.Composer
{
    public class EventHandler : IComponent
    {
        public void Initialize()
        {
            ContentService.Saving += ContentService_Saving;
        }

        private readonly IUserService userService;

        public EventHandler(
            IUserService userService)
        {
            this.userService = userService;
        }


        private void ContentService_Saving(IContentService sender, ContentSavingEventArgs e)
        {            
            var user = userService.GetByUsername(HttpContext.Current.User.Identity.Name);
            foreach (IContent node in e.SavedEntities)
            {
                GuidUdi udi = node.GetUdi();

                bool isLocked = true; //TODO: Datenbank-Abfrage, ob Node geloggt
                if(isLocked)
                {
                    e.Cancel = true;
                    e.Messages.Add(new EventMessage("Saving cancelled", "The Saving was cancelled, cause the node is locked by ", EventMessageType.Error));
                }
            }

        public void Terminate()
        {
            
        }
    }
}