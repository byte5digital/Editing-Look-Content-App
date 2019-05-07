using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Editors;
using byte5.EditingLookContentApp.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.Composing;
using NPoco;
using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace byte5.EditingLookContentApp.Controllers
{
    [PluginController("byte5EditingLook")]
    public class ApiController : UmbracoAuthorizedJsonController
    {
        #region ClassMember
        //-----------------------------------------------------------------
        string DbName = "EditingLook";
        //-----------------------------------------------------------------
        #endregion // ClassMember

        #region GetCurrentUser
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets the user that is currently editing the node.
        /// </summary>
        /// <param name="nodeUid">The uid of the node to get user for</param>
        /// <returns>The current user.</returns>
        //-----------------------------------------------------------------
        [HttpGet]
        public Umbraco.Core.Models.Membership.IUser GetCurrentUser(string nodeUid)
        {
            EditingLook entry = this.GetEditingEntry(nodeUid);

            if (entry != null)
            {
                // return the umbraco user
                return Services.UserService.GetUserById(entry.UserId);
            }

            return null;
        }
        #endregion // GetCurrentUser

        #region CreateEditingEntry
        //-----------------------------------------------------------------
        /// <summary>
        /// Creates and saves a new editing entry or if it already exists, updates it.
        /// </summary>
        /// <param name="nodeUid">The uid of the node</param>
        /// <param name="currentUserId">the id of the current user</param>
        /// <returns>The created or updated EditingLook</returns>
        //-----------------------------------------------------------------
        [HttpPost]
        public EditingLook CreateEditingEntry(string nodeUid, int currentUserId)
        {
            // check for existing entry
            EditingLook entry = this.GetEditingEntry(nodeUid);

            if (entry == null)
            {
                // create editing look object
                EditingLook editingLook = new EditingLook()
                {
                    NodeUid = nodeUid,
                    UserId = currentUserId,
                    SubscribeDate = DateTime.Now
                };

                using (var scope = Current.ScopeProvider.CreateScope(autoComplete: true))
                {
                    // insert editing entry
                    scope.Database.Insert<EditingLook>(editingLook);
                }

                return editingLook;
            }

            // entry already exists -> update entry
            return this.UpdateEditingEntry(nodeUid, currentUserId, entry);
        }
        #endregion // CreateEditingEntry

        #region UpdateEditingEntry
        //-----------------------------------------------------------------
        /// <summary>
        /// Updates an existing editing entry, or if it doesn't exist, creates it.
        /// </summary>
        /// <param name="nodeUid">The uid of the node</param>
        /// <param name="currentUserId">The id of the current user</param>
        /// <param name="entry">The entry to update</param>
        /// <returns>The updated or created editing entry</returns>
        //-----------------------------------------------------------------
        [HttpPut]
        public EditingLook UpdateEditingEntry(string nodeUid, int currentUserId, EditingLook entry = null)
        {
            // check for existing entry
            if (entry == null) entry = GetEditingEntry(nodeUid);

            if (entry != null)
            {
                entry.UserId = currentUserId;
                entry.SubscribeDate = DateTime.Now;

                using (var scope = Current.ScopeProvider.CreateScope(autoComplete: true))
                {
                    // update editing entry
                    scope.Database.Update(entry);
                }
            }
            else
            {
                // entry doesn't exist -> create the entry
                this.CreateEditingEntry(nodeUid, currentUserId);
            }

            return entry;
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
        [HttpDelete]
        public void DeleteEditingEntry(string nodeUid, int currentUserId)
        {
            using (var scope = Current.ScopeProvider.CreateScope(autoComplete: true))
            {
                // delete editing entry
                Sql sql = scope.SqlContext.Sql().Select("*").From(DbName).Where<EditingLook>(e => e.NodeUid == nodeUid && e.UserId == currentUserId);
                scope.Database.Delete<EditingLook>(sql);
            }
        }
        #endregion // DeleteEditingEntry

        #region GetEditingEntry
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets the current editing entry with specific nodeUid.
        /// </summary>
        /// <param name="nodeUid">The uid of the node to get the entry for</param>
        /// <returns>The current editing entry</returns>
        //-----------------------------------------------------------------
        internal EditingLook GetEditingEntry(string nodeUid)
        {
            EditingLook result = null;

            using (var scope = Current.ScopeProvider.CreateScope(autoComplete: true))
            {
                // get editing entry
                Sql sql = scope.SqlContext.Sql().Select("*").From(DbName).Where<EditingLook>(x => x.NodeUid == nodeUid);
                result = scope.Database.SingleOrDefault<EditingLook>(sql);
            }
            
            if (result != null && result.SubscribeDate.AddMinutes(2) < DateTime.Now)
            {
                // entry is expired -> delete this entry
                this.DeleteEditingEntry(result.NodeUid, result.UserId);
                return null;
            }
            
            return result;
        }
        #endregion // GetEditingEntry
    }
}