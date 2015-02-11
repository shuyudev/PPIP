angular
  .module('ppip', [
    'ui.router',
    'ui.router.stateHelper',
    'ngMaterial'
  ])
  .config([
    '$urlRouterProvider',
    '$stateProvider',
    '$logProvider',
    'stateHelperProvider',
    config
  ])
  .run(['$rootScope', '$state', '$stateParams', run]);

function config ($urlRouterProvider, $stateProvider, $logProvider, stateHelperProvider) {
  $logProvider.debugEnabled(true);

  $urlRouterProvider.otherwise('/phone');

  stateHelperProvider.setNestedState({
    name: 'page',
    abscract: true,
    templateUrl: '/template/page',
    controller: 'pageCtrl',
    data: {
      title: ''
    },
    children: [{
      name: 'phone',
      url: '/phone',
      templateUrl: 'template/page-phone',
      controller: 'phoneCtrl',
      data: {
        icon: 'mobile',
        title: 'Phone'
      }
    }, {
      name: 'pipeline',
      url: '/pipeline',
      templateUrl: 'template/page-pipeline',
      controller: 'pipelineCtrl',
      data: {
        icon: '',
        title: 'Pipeline'
      }
    }]
  });

}

function run ($rootScope, $state, $stateParams) {
  $rootScope.$state = $state;
  $rootScope.$stateParams = $stateParams;
}