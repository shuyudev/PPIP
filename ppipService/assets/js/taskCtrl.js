angular
  .module("ppip")
  .controller('taskCtrl', ['$scope', 'apiService', function($scope, apiService) {

    function listTask() {
      apiService.task.list().success(function (res) {
        $scope.tasks = res;
      });
    }

    listTask();

    $scope.delete = function (task) {
      apiService.task.delete(task.id).success(function (res) {
        listTask();
      })
    }
  }]);
