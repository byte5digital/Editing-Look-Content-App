angular.module("umbraco")
    .controller("byte5.EditingLookContentApp", function ($scope, editorState, byte5EditingLookResource) {
        var vm = this;
        vm.currentNodeId = editorState.current.udi;

        byte5EditingLookResource.getCurrentUser(vm.CurrentNodeId).then(function (response) {

            if (response.Status === 200) {
                if (!response.data) {
                    //No user found
                    lockNode(vm, byte5EditingLookResource);
                }
                else if (response.data.UserId === vm.currentUserId) {
                    //Current user locks the node, so the node isnt locked for him
                    setNodeStatus(vm, 'Success');
                }
                else {
                    //User found isnt the current user, so the node needs to be locked
                    setNodeStatus(vm, 'Alert');
                }
            } else {
                //If the API returns an error the node wont get locked, so we need to set the status to success
                setNodeStatus(vm, 'Success');
            }
        });
    });

function setNodeStatus(vm, status) {
    switch (status) {
        case 'Success':
            vm.lockNode = false;
            break;
        case 'Alert':
            vm.lockNode = true;
            break;
    }
    vm.badgeStatus = status;
}

function lockNode(vm, resource) {
    resource.createEditingEntry(vm.currentNodeId, vm.currentUserId).then(function (response) {
        if (response.Status === 200) {
            setNodeStatus(vm, 'Success');
            keepLockAlive($scope);
            lockView();
        } else {
            //Error node could not be locked
        }
    });
}

function unlockNode(vm, resource) {
    resource.deleteEditingEntry(vm.currentNodeId, vm.currentUserId).then(function (response) {
        if (response.Status === 200) {
            //User left page
        } else {
            //Error node could not be unlocked - node will automatically be unlocked after 2 mins
        }
    });
}
function lockView() {
    //Html Style changes etc...
}

function unlockView() {
    //Html Style changes etc...
}

function keepLockAlive(scope) {
    $interval(function () {
        t++;

        console.log(t);

    }, 1000, 5);
}

function registerEvents(scope) {
    scope.$on(
        "$locationChangeSuccess",
        handleLocationChangeEvent(event)
    );
}

function handleLocationChangeEvent(event) {

    console.log("Location Change from " + vm.currentPath + " to  " + $location.path());

    alert("Finished!");

}

