angular
  .module("ppip")
  .controller('phoneCtrl', ['$scope', 'apiService', function($scope, apiService) {
    $scope.message = "Click + to add a phone to your inventory"

    $scope.phones = [
      {
        name: "MyWP0",
        toke: "12345",
        status: 'online'
      },
      {
        name: "MyAndroid0",
        toke: "67890",
        status: 'offline'
      },
      {
        name: "MyIPhone0",
        toke: "13579",
        status: 'online'
      }
    ];

    apiService.phone.list().success(function (res) {
      $scope.phones = res;
    })

    // phones that are pending in creation zone
    $scope.newPhones = [];

    $scope.getExistingPhones = function() {
      // todo: add logic to get phones from the backend
    };

    $scope.submitPhone = function(phone) {
      if (!!phone.name) {
        if (phone.name.length <= 10) {
          var idx = $scope.newPhones.indexOf(phone);

          apiService.phone.create(phone.name).success(function (res) {
            $scope.newPhones.splice(idx, 1);
            $scope.phones.push(phone);
          });

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
