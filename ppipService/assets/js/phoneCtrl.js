angular
  .module("ppip")
  .controller('phoneCtrl', ['$scope', function($scope) {
    $scope.message = "Click + to add a phone to your inventory"

    $scope.phones = [
      {
        name: "MyWP0",
        accessCode: "12345",
        online: true
      },
      {
        name: "MyAndroid0",
        accessCode: "67890",
        online: false
      },
      {
        name: "MyIPhone0",
        accessCode: "13579",
        online: true
      }
    ];

    // phones that are pending in creation zone
    $scope.newPhones = [];

    $scope.getExistingPhones = function() {
      // todo: add logic to get phones from the backend
    };

    $scope.submitPhone = function(phone) {
      if (!!phone.name) {
        if (phone.name.length <= 10) {
          var idx = $scope.newPhones.indexOf(phone);
          $scope.newPhones.splice(idx, 1);
          $scope.phones.push(phone);
        } else {
          alert("Constrain yourself! Dump your phone if its name is more than 10 characters!")
        }
      } else {
        alert("Why $$TF$$ do you want to create a phone without a name!")
      }
    }

    $scope.cancelPhone = function(phone) {
      var idx = $scope.newPhones.indexOf(phone);
      $scope.newPhones.splice(idx, 1);
    }

    $scope.newPhone = function() {
      $scope.newPhones.push({
        name: ""
      });
    };
  }]);
