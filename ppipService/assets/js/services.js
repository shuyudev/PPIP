angular
  .module('ppip')
  .factory('apiService', ['$http', '$state', '$log', api]);

function api ($http, $state, $log) {
  var apiService = {};

  var errorAction = function (data, status) {
    $log.debug(data || 'request failed');
  }

  apiService.phone = {
    list: function () {
      return $http.get('phone').error(errorAction);
    },
    create: function (phoneName) {
      return $http.get('phone/create?name=' + phoneName).error(errorAction);
    },
    refreshToken: function (phoneId) {
      return $http.get('phone/refreshToken/' + phoneId).error(errorAction);
    }
  }

  apiService.pipeline = {
    list: function () {
      return $http.get('pipeline').error(errorAction);
    },
    create: function (pipelineName, pipelineType, inputPhones, outputPhones) {
      return $http({
        url: 'pipeline/create',
        method: 'POST',
        params: {
          name: pipelineName,
          type: pipelineType,
          input: inputPhones,
          output: outputPhones
        }
      }).error(errorAction);
    },
    enable: function (pipelineId) {
      return $http.get('pipeline/enable/' + pipelineId).error(errorAction);
    },
    disable: function (pipelineId) {
      return $http({
        url: 'pipeline/update/' + pipelineId,
        method: 'GET',
        params: {
          status: 'disabled'
        }
      }).error(errorAction);
    }
  }

  return apiService;
}