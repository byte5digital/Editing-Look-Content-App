angular.module("umbraco")
    .controller("byte5.EditingLookContentApp", function ($scope) {

        // Change this to fit the needs of the container.
        // It shows how to set a class on a certain node.
        // We need to set is-locked to show the lock icon. See editingLook.css.
        $scope.setLocked = function () {
            $scope.currentNode.cssClasses.push("is-locked");
            // to remove the lock just remove "is-locked" from cssClasses
        };
       
    });