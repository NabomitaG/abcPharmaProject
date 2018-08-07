var app = angular.module("app", ['ngRoute',
    'ngResource',
    'ui.grid', 'ui.grid.selection', 'ui.bootstrap'
    //,'ngGrid'
    //,'ui.grid.pagination',
    //'ui.grid.selection',
    //'ui.grid.cellNav',
    //'ui.grid.expandable',
    //'ui.grid.edit',
    //'ui.grid.rowEdit',
    //'ui.grid.saveState',
    //'ui.grid.resizeColumns',
    //'ui.grid.pinning',
    //'ui.grid.moveColumns',
    //'ui.grid.exporter',
    //'ui.grid.infiniteScroll',
    //'ui.grid.importer',
    //'ui.grid.grouping'
    //,'ui.bootstrap'
]);

app.constant('config', {
    apiUrl: 'http://localhost:61217/',
    baseUrl: '/',
    enableDebug: true
});

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/start', { templateUrl: './home/store', controller: 'StoreController' });
    $routeProvider.when('/medicines', { templateUrl: './home/medicines', controller: 'MedicineController' });
    $routeProvider.when('/purchase', { templateUrl: './home/purchase', controller: 'PurchaseController' });
    $routeProvider.when('/invoice', { templateUrl: './home/invoice', controller: 'InvoiceController' });
    $routeProvider.otherwise({ redirectTo: '/start' });
}])
    .controller('RootController', ['$scope', '$route', '$routeParams', '$location', function ($scope, $route, $routeParams, $location) {

    }]);
