var app = angular.module('app');

app.controller('InvoiceController', ['$scope', '$http', 'config','storesService','$rootScope', '$modalInstance', 'storeId',
    function ($scope, $http,config,storesService,$rootScope, $modalInstance, storeId) {
        $scope.invoiceDetails;
        $scope.items=[];
        $scope.totalAmount;
        $scope.taxes;
        $scope.discounts;
        $scope.storeDiscounts;
        $scope.finalAmount;
        $scope.purchaseId;
        $scope.tests = [];
        $scope.storeID = storeId;
        $scope.storeDetails = {};

        $scope.getInvoiceDetails = function () {
            $scope.invoiceDetails = angular.fromJson($rootScope.invoiceData);
            $scope.parseInvoice($scope.invoiceDetails);
        }

        $scope.parseInvoice = function (data) {
            $scope.totalAmount = $scope.invoiceDetails.totalAmount;
            $scope.taxes = $scope.invoiceDetails.taxes;
            $scope.discounts = $scope.invoiceDetails.discounts;
            $scope.storeDiscounts = $scope.invoiceDetails.storeDiscounts;
            $scope.finalAmount = $scope.invoiceDetails.finalAmount;
            $scope.purchaseId = $scope.invoiceDetails.purchaseId;
            $scope.items = $scope.invoiceDetails.purchasedMed;

            angular.forEach($scope.items, function (value, key) {
                $scope.tests.push({
                    'medicineName': value.medicinePurchased.medicineName,
                    'unitPrice': value.medicinePurchased.unitPrice,
                    'quantityToPurchase': value.quantityToPurchase,
                    'subTotalPrice': value.subTotalPrice
                })
            });

            $scope.getStoreDetails();
        }

        $scope.getStoreDetails = function () {
            storesService.getStoreById(config,$scope.storeID)
                .then(function (successResponse) {
                    $scope.storeDetails = successResponse.data;
                }, function (errorResponse) {

                });
        }

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }]);