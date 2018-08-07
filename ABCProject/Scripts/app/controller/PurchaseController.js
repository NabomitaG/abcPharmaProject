var app = angular.module('app');

app.controller('PurchaseController', ['$scope', '$http', 'config', 'purchaseService', 'storesService', 'medicineService',
    '$location', '$rootScope', '$modal', '$log',
    function ($scope, $http, config, purchaseService, storesService, medicineService, $location, $rootScope, $modal, $log) {
        $scope.stores = [];
        $scope.medicines = [];
        $scope.selectedStoreId = '';
        $scope.selectedMedicineId = '';
        $scope.selectedStore = {};
        $scope.selectedMedicine = {};
        $scope.isStoreSelected = true;
        $scope.isMedicineSelected = true;
        $scope.medicinesInCart = [];
        $scope.medicinesList = [];
        $scope.quantityPurchase;
        $scope.isMedicineAdded = false;
        $rootScope.invoiceData = '';

        $scope.purchaseInit = function () {
            $scope.getStores();
            $scope.getMedicines();
        }

        $scope.getStores = function () {
            storesService.getStores(config)
                .then(function (successResponse) {
                    $scope.stores = successResponse.data;
                }, function (errorResponse) {

                });
        }

        $scope.getMedicines = function () {
            medicineService.getMedicines(config)
                .then(function (successResponse) {
                    $scope.medicines = successResponse.data;
                }, function (errorResponse) {

                });
        }

        $scope.medicineSelectionChanged = function () {
            $scope.selectedMedicine = $scope.medicines.filter(function (med) {
                return (med.itemId == $scope.selectedMedicineId);
            });
            $scope.selectedMedicine = $scope.selectedMedicine[0];
            if ($scope.selectedMedicine != null) {
                $scope.isMedicineSelected = false;
            }
        }

        $scope.storeSelectionChanged = function () {
            if ($scope.selectedStoreId) {
                $scope.isStoreSelected = false;
            }
        }

        $scope.generateInvoice = function () {
            $scope.getInvoiceDetails();
        }

        $scope.getInvoiceDetails = function () {
            angular.forEach($scope.medicinesInCart, function (value, key) {
                $scope.medicinesList.push({
                    'medicineId': value.medicineId,
                    'quantityToPurchase': value.quantityToPurchase,
                    'subTotalPrice': value.subTotalPrice
                })
            });

            var dataToPost = {
                "storeId": $scope.selectedStoreId,
                "medicineList": $scope.medicinesList
            };
            purchaseService.generateInvoice(dataToPost, config).then(function (successResponse) {
                $rootScope.invoiceData = successResponse.data;
                $scope.addUser();
                //$location.path("/invoice");
            }, function (errorResponse) {
                alert('Failure');
            });
        }

        $scope.addUser = function () {
            var dialogInst = $modal.open({
                templateUrl: './home/invoice',
                controller: 'InvoiceController',
                size: 'lg',
                resolve: {
                    storeId: function () {
                        return $scope.selectedStoreId;
                    }
                }
            });
            //dialogInst.result.then(function (newusr) {
            //    $scope.usrs.push(newusr);
            //    $scope.usr = { name: '', job: '', age: '', sal: '', addr: '' };
            //}, function () {
            //    $log.info('Modal dismissed at: ' + new Date());
            //});
        };

        $scope.gridOptions = {
            data: $scope.medicinesInCart,
            columnDefs: [
                { field: 'medicineName', name: 'medicineName', width: '25%' },
                { field: 'unitPrice', name: 'unitPrice', width: '12%' },
                { field: 'quantityToPurchase', name: 'quantityToPurchase', width: '12%' },
                { field: 'subTotalPrice', name: 'subTotalPrice', width: '12%' },
                {
                    name: 'actions', field: 'edit', width: '12%',
                    cellTemplate: '<div style="text-align: center; padding-top: 5px; padding-bottom: 5px;"><button ng-show="!row.entity.editable" ng-click="grid.appScope.editRow(row.entity)" class="btn btn-info btn-xs"><i class="fa fa-pencil"></i>Edit</button>' +  //Edit Button
                        '<button ng-show="row.entity.editable" ng-click="grid.appScope.updateRow(row.entity)" class="btn btn-info btn-xs"><i class="fa fa-save"></i>Update</button>' +//Save Button
                        '<button ng-show="row.entity.editable" ng-click="grid.appScope.cancelEdit(row.entity)" class="btn btn-info btn-xs"><i class="fa fa-times"></i>Cancel</button>' + //Cancel Button
                        '<button ng-show="!row.entity.editable" ng-click="grid.appScope.deleteRow(row.entity)" class="btn btn-danger btn-xs"><i class="fa fa-trash-o"></i>Delete</button>' + //Delete Button
                        '</div>'
                }
            ],
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi; // i'd recommend a promise or deferred for this
            }
        };

        $scope.addMedicineToCart = function () {
            if ($scope.selectedMedicine != null) {
                if ($scope.quantityPurchase > $scope.selectedMedicine.quantity) {
                    alert("Sorry we only have " +$scope.selectedMedicine.quantity+" units available for this medicine");
                }
                else {

                    $scope.medicinesInCart.push({
                        'medicineName': $scope.selectedMedicine.medicineName,
                        'medicineId': $scope.selectedMedicine.itemId,
                        'unitPrice': $scope.selectedMedicine.unitPrice,
                        'quantityToPurchase': $scope.quantityPurchase,
                        'subTotalPrice': $scope.selectedMedicine.unitPrice * $scope.quantityPurchase
                    })
                }
            }
            if ($scope.medicinesInCart.length >= 1) {
                $scope.isMedicineAdded = true;
            }
            else {
                $scope.isMedicineAdded = false;
            }
        }
    }]);

