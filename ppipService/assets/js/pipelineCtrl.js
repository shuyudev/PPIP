angular
  .module("ppip")
  .controller('pipelineCtrl', ['$scope', 'apiService', function($scope, apiService) {
    $scope.message = "Click + to add a pipeline to your inventory"
    $scope.newPipeline = [];

    function listPipeline () {
      apiService.pipeline.list().success(function (res) {
        $scope.pipelines = res;
      });
    }

    listPipeline();

    $scope.submit = function(pipeline) {
      if (!!pipeline.name && !!pipeline.type && !!pipeline.input && !!pipeline.output) {
        var idx = $scope.newPipeline.indexOf(pipeline);

        apiService.pipeline.create(pipeline.name, pipeline.type, [pipeline.input], [pipeline.output]).success(function (res) {
          $scope.newPipeline.splice(idx, 1);
          listPipeline();
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

    $scope.togglePipeline = function (pipeline) {
      if (pipeline.status == 'disabled') {
        return apiService.pipeline.enable(pipeline.id).success(function (res) {
          listPipeline();
        });
      }

      if (pipeline.status == 'enabled') {
        return apiService.pipeline.disable(pipeline.id).success(function (res) {
          listPipeline();
        })
      }
    }

    $scope.delete = function (pipeline) {
      apiService.pipeline.delete(pipeline.id).success(function (res) {
        listPipeline();
      })
    }
  }]);
