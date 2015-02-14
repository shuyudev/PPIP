angular
  .module("ppip")
  .controller('pageCtrl', ['$scope', '$state', '$mdSidenav', pageCtrl]);

function pageCtrl($scope, $state, $mdSidenav) {
  $scope.navEntries = $scope.navEntries = $state.current.parent.children;
  $scope.clickToggleNav = clickToggleNav;

  function clickToggleNav () {
    $mdSidenav('left').toggle();
  }
}
