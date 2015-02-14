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
        icon: 'exchange',
        title: 'Pipeline'
      }
    }, {
      name: 'plugIn',
      url: '/plugIn',
      templateUrl: 'template/page-plugIn',
      controller: 'phoneCtrl',
      data: {
        icon: 'soundcloud',
        title: '3rd Party PlugIn'
      }
    }, {
      name: 'task',
      url: '/task',
      templateUrl: 'template/page-task',
      controller: 'taskCtrl',
      data: {
        icon: 'tags',
        title: 'task list'
      }
    }, {
      name: 'phoneList',
      url: '/phoneList',
      templateUrl: 'template/page-phoneList',
      controller: 'phoneCtrl',
      data: {
        icon: 'tags',
        title: 'phone list'
      }
    }, {
      name: 'pipelineList',
      url: '/pipelineList',
      templateUrl: 'template/page-pipelineList',
      controller: 'pipelineCtrl',
      data: {
        icon: 'tags',
        title: 'pipeline list'
      }
    }]
  });

}

function run ($rootScope, $state, $stateParams) {
  $rootScope.$state = $state;
  $rootScope.$stateParams = $stateParams;
}