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
        /// <returns>the current user</returns>
        //-----------------------------------------------------------------
        [HttpGet]
        public Umbraco.Core.Models.Membership.IUser GetCurrentUser(string nodeUid)
        {
            // TEST:
            this.CreateEditingEntry(nodeUid, 7);
            this.GetEditingEntry(nodeUid);
            this.UpdateEditingEntry(nodeUid, 1);
            this.GetEditingEntry(nodeUid);
            this.DeleteEditingEntry(nodeUid, 1);




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
        /// Creates and saves a new editing entry.
        /// </summary>
        /// <param name="nodeUid">The uid of the node</param>
        /// <param name="currentUserId">the id of the current user</param>
        /// <returns>The created EditingLook</returns>
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
        /// Updates an existing editing entry or creates it.
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
                using (var scope = Current.ScopeProvider.CreateScope(autoComplete: true))
                {
                    // update editing entry
                    NPocoSqlExtensions.SqlUpd<EditingLook> update = new NPocoSqlExtensions.SqlUpd<EditingLook>(scope.SqlContext);
                    update.SetExpressions.Add(new Tuple<string, object>("UserId", currentUserId));
                    update.SetExpressions.Add(new Tuple<string, object>("SubscribeDate", DateTime.Now));

                    Sql sql = scope.SqlContext.Sql().Update<EditingLook>(e => update);
                    scope.Database.Update<EditingLook>(sql);
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
        /// Gets an existing editing entry
        /// </summary>
        /// <param name="nodeUid">The uid of the node</param>
        /// <returns>The editing entry</returns>
        //-----------------------------------------------------------------
        internal EditingLook GetEditingEntry(string nodeUid)
        {
            EditingLook result = null;
            IList<EditingLook> resultList = null;

            using (var scope = Current.ScopeProvider.CreateScope(autoComplete: true))
            {
                // get editing entry
                Sql sql = scope.SqlContext.Sql().Select("*").From(DbName).Where<EditingLook>(x => x.NodeUid == nodeUid);
                resultList = scope.Database.Fetch<EditingLook>(sql);
            }
            
            // check found entries
            foreach (EditingLook entry in resultList)
            {
                if (entry != null && entry.SubscribeDate.AddMinutes(2) < DateTime.Now)
                {
                    // entry is expired -> delete this entry
                    this.DeleteEditingEntry(entry.NodeUid, entry.UserId);
                }
                else
                {
                    result = entry;
                }
            }
            
            return result;
        }
        #endregion // GetEditingEntry
    }
}