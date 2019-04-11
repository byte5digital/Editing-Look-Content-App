angular.module('umbraco.resources').factory('byte5EditingLookResource',
    function ($q, $http, umbRequestHelper) {
        return {
            getCurrentUser: function (currentUdi) {
                return umbRequestHelper.resourcePromise(
                    $http.get("backoffice/b5LinkedNodes/LinkedNodesContentAppApi/GetLinkedNodes?currentUdi="
                        + currentUdi),
                    "Failed to retrieve linked nodes for this node");
            },
            createEditingEntry: function (currentUdi, currentUserId) {
                return umbRequestHelper.resourcePromise(
                    $http.get("backoffice/b5LinkedNodes/LinkedNodesContentAppApi/GetLinkedNodes?currentUdi="
                        + currentUdi + "&isContent=" + currentUserId),
                    "Failed to retrieve linked nodes for this node");
            },
            UpdateEditingEntry: function (currentUdi, currentUserId) {
                return umbRequestHelper.resourcePromise(
                    $http.get("backoffice/b5LinkedNodes/LinkedNodesContentAppApi/GetLinkedNodes?currentUdi="
                        + currentUdi + "&isContent=" + currentUserId),
                    "Failed to retrieve linked nodes for this node");
            },
            deleteEditingEntry: function (currentUdi, currentUserId) {
                return umbRequestHelper.resourcePromise(
                    $http.get("backoffice/b5LinkedNodes/LinkedNodesContentAppApi/GetLinkedNodes?currentUdi="
                        + currentUdi + "&isContent=" + currentUserId),
                    "Failed to retrieve linked nodes for this node");
            }
        };
    }
);