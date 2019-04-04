using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Editors;
using byte5.EditingLookContentApp.Models;
using Umbraco.Web.Mvc;

namespace byte5.EditingLookContentApp.Controllers
{
    [PluginController("byte5EditingLook")]
    public class ApiController : UmbracoAuthorizedJsonController
    {
        #region Methods

        #region GetCurrentUser
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets the user that is currently editing the node.
        /// </summary>
        /// <param name="nodeUid">The uid of the node to get user for</param>
        /// <returns>the current user</returns>
        //-----------------------------------------------------------------
        [HttpGet]
        public Umbraco.Core.Models.Membership.IUser GetCurrentUser(string nodeUid)
        {
            throw new NotImplementedException();
        }
        #endregion // GetCurrentUser

        #region CreateEditingEntry
        //-----------------------------------------------------------------
        /// <summary>
        /// creates and saves a new editing entry.
        /// </summary>
        /// <param name="nodeUid">The uid of the node</param>
        /// <param name="currentUserId">the id of the current user</param>
        /// <returns>the created EditingLook</returns>
        //-----------------------------------------------------------------
        public EditingLook CreateEditingEntry(string nodeUid, int currentUserId)
        {
            EditingLook model = new EditingLook(0, nodeUid, currentUserId, DateTime.Now);

            //save model

            throw new NotImplementedException();
        }
        #endregion // CreateEditingEntry

        #region UpdateEditingEntry
        //-----------------------------------------------------------------
        /// <summary>
        /// Updates an existing editing entry
        /// </summary>
        /// <param name="nodeUid">The uid of the node</param>
        /// <param name="currentUserId">the id of the current user</param>
        /// <returns>the updated editing entry</returns>
        //-----------------------------------------------------------------
        public EditingLook UpdateEditingEntry(string nodeUid, int currentUserId)
        {
            throw new NotImplementedException();
        }
        #endregion // UpdateEditingEntry

        #region DeleteEditingEntry
        //-----------------------------------------------------------------
        /// <summary>
        /// Deletes an existing editing entry
        /// </summary>
        /// <param name="nodeUid">The uid of the node</param>
        /// <param name="currentUserId">the id of the current user</param>
        /// <returns></returns>
        //-----------------------------------------------------------------
        public void DeleteEditingEntry(string nodeUid, int currentUserId)
        {
            throw new NotImplementedException();
        }
        #endregion // DeleteEditingEntry

        #endregion // Methods
    }
}