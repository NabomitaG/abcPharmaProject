app.service('purchaseService', ['$http', function ($http) {

    this.generateInvoice = function (dataToPost,config) {
        return $http.post(config.apiUrl + 'api/Purchase/GenerateInvoice', dataToPost);
    }

}])