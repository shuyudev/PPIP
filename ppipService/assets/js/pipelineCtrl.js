angular
  .module("ppip")
  .controller('pipelineCtrl', ['$scope', 'apiService', function($scope, apiService) {
    $scope.message = "Click + to add a pipeline to your inventory"
    $scope.newPipeline = [];

    apiService.pipeline.list().success(function (res) {
      $scope.pipelines = res;
    })

    $scope.submit = function(pipeline) {
      if (!!pipeline.name && !!pipeline.type && !!pipeline.input && !!pipeline.output) {
        var idx = $scope.newPipeline.indexOf(pipeline);

        apiService.pipeline.create(pipeline.name, pipeline.type, [pipeline.input], [pipeline.output]).success(function (res) {
          $scope.newPipeline.splice(idx, 1);
          $scope.pipelines.push(res);
        });
      } else {
        alert("missing fields")
      }
    }

    $scope.cancel = function(pipeline) {
      var idx = $scope.newPipeline.indexOf(pipeline);
      $scope.newPipeline.splice(idx, 1);
    }

    $scope.new = function() {
      $scope.newPipeline.push({
        name: "",
        type: "sharePicture"
      });
    };
  }]);
