app.service('storesService', ['$http', function ($http) {
    
    this.getStores = function (config) {
        return $http.get(config.apiUrl + 'api/Store/GetAllStores');
    }

    this.getStoreById = function (config, storeId) {
        return $http.get(config.apiUrl + 'api/Store/GetStoreById?storeId=' + storeId);
    }

    this.SaveStoreDetails = function (dataToPost, config) {
        return $http.post(config.apiUrl + 'api/Store/SaveStoreDetails', dataToPost);
    }
}])