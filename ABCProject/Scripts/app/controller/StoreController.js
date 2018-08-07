var app = angular.module('app');

app.controller('StoreController', ['$scope', '$http', 'storesService', 'config', function ($scope, $http, storesService, config) {
    $scope.stores = [];
    $scope.storeToAdd =
        {
        storeName: "",
        storeAddress: "",
        storePhoneNo: ""
        };
    $scope.strName;
    $scope.strAddress;
    $scope.strPhone;

    $scope.init = function () {
        $scope.getStores();
    }

    $scope.getStores = function () {
        storesService.getStores(config)
            .then(function (successResponse) {
                $scope.stores = successResponse.data;
            }, function (errorResponse) {

            });
    }

    $scope.addStore = function () {
        $scope.storeToAdd = {
            storeName: $scope.strName,
            storeAddress: $scope.strAddress,
            storePhoneNo: $scope.strPhone
        };
        storesService.SaveStoreDetails($scope.storeToAdd, config).then(function (successResponse) {
            $scope.getStores();
        }, function (errorResponse) {
            alert('Failure');
        });
    }
}]);