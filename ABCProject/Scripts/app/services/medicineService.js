app.service('medicineService', ['$http', function ($http) {

    this.getMedicines = function (config) {

        return $http.get(config.apiUrl + 'api/Medicine/GetAllMedicine');
    }

    this.getMedicinesByName = function (config,medicineName) {

        return $http.get(config.apiUrl + 'api/Medicine/GetMedicineByName?medicineName_=' + medicineName);
    }

    this.SaveMedicineDetails = function (dataToPost, config) {
        return $http.post(config.apiUrl + 'api/Medicine/SaveMedicines', dataToPost);
    }
}])