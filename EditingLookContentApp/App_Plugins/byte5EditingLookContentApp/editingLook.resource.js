angular.module('umbraco.resources').factory('byte5EditingLookResource',
    function ($q, $http, umbRequestHelper) {
        return {
            getCurrentUser: function (currentUdi) {
                return umbRequestHelper.resourcePromise(
                    $http.get("backoffice/byte5EditingLook/api/getcurrentuser?nodeUid="
                        + currentUdi),
                    "Failed to retrieve the current user for this node");
            },
            createEditingEntry: function (currentUdi, currentUserId) {
                return umbRequestHelper.resourcePromise(
                    $http.get("backoffice/byte5EditingLook/api/createeditingentry?nodeUid="
                        + currentUdi + "&currentUserId=" + currentUserId),
                    "Failed to create entry for this node");
            },
            UpdateEditingEntry: function (currentUdi, currentUserId) {
                return umbRequestHelper.resourcePromise(
                    $http.get("backoffice/byte5EditingLook/api/updateeditingentry?nodeUid="
                        + currentUdi + "&currentUserId=" + currentUserId),
                    "Failed to update entry for this node");
            },
            deleteEditingEntry: function (currentUdi, currentUserId) {
                return umbRequestHelper.resourcePromise(
                    $http.get("backoffice/byte5EditingLook/api/deleteeditingentry?nodeUid="
                        + currentUdi + "&currentUserId=" + currentUserId),
                    "Failed to delete entry for this node");
            }
        };
    }
);