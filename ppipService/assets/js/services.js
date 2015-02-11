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
    }
  }

  return apiService;
}