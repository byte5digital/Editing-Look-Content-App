using byte5.EditingLookContentApp.Controllers;
using byte5.EditingLookContentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Models.Membership;
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

        public EventHandler(IUserService userService)
        {
            this.userService = userService;
        }

        private void ContentService_Saving(IContentService sender, ContentSavingEventArgs e)
        {
            var user = userService.GetByUsername(HttpContext.Current.User.Identity.Name);
            foreach (IContent node in e.SavedEntities)
            {
                // Get the current udi from Node
                GuidUdi udi = node.GetUdi();

                // Get the current User, that is editing the node
                ApiController apiC = new ApiController();

                IUser userLocked = apiC.GetCurrentUser(udi.ToString());

                // Allow saving, if node not locked or the user, that locked the node, is the current user
                if (userLocked != null && userLocked.Id != user.Id)
                {
                    e.Cancel = true;
                    string userName = (userLocked == null) ? "John Doe" : userLocked.Name;
                    e.Messages.Add(new EventMessage("Saving cancelled",
                        "The Saving was cancelled, cause the node is locked by " + userName,
                        EventMessageType.Error));
                }
            }
        }

        public void Terminate()
        {

        }
    }
}
        