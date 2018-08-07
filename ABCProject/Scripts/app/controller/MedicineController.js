var app = angular.module('app');

app.controller('MedicineController', ['$scope', '$http', 'medicineService', 'config', function ($scope, $http, medicineService, config) {
    $scope.medicines = [];

    $scope.medName;
    $scope.medPkgDate;
    $scope.medExpiryDate;
    $scope.medUnitPrice;
    $scope.medQtyAvailable;
    $scope.searchName;

    $scope.medicineToAdd =
        {
            medicineName: '',
            pkgDate: Date.now(),
            expiryDate: Date.now(),
            unitPrice: 0,
            quantity: 0
        };

    $scope.medicineInit = function () {
        $scope.getMedicines();
    }

    $scope.getMedicines = function () {
        medicineService.getMedicines(config)
            .then(function (successResponse) {
                $scope.medicines = successResponse.data;
            }, function (errorResponse) {

            });
    }

    $scope.getMedicinesByName = function () {
        medicineService.getMedicinesByName(config, $scope.searchName)
            .then(function (successResponse) {
                $scope.medicines = successResponse.data;
            }, function (errorResponse) {

            });
    }

    $scope.reset = function () {
        $scope.searchName = '';
        $scope.getMedicines();
    }

    $scope.addMedicine = function () {

        $scope.medicineToAdd = {
            medicineName: $scope.medName,
            pkgDate: $scope.medPkgDate,
            expiryDate: $scope.medExpiryDate,
            unitPrice: $scope.medUnitPrice,
            quantity: $scope.medQtyAvailable
        };

        medicineService.SaveMedicineDetails($scope.medicineToAdd, config).then(function (successResponse) {
            $scope.getMedicines();
        }, function (errorResponse) {
            alert('Failure');
            });
    }
}]);