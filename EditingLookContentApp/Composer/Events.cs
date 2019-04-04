using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace byte5.EditingLookContentApp.Composer
{
    public class Events : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<EventHandler>();
        }
    }
}