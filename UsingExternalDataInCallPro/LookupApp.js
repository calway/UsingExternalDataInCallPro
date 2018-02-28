angular.module('lookupApp', ['ngRoute'])
    .service("LookupService", function ($http) {
        this.getLookup = function () {
            var r = $http.get("/Lookup/GetList");
            return r;
        }
        this.reserve = function (id,clentryid) {
            return $http.post("/Lookup/Reserve/"+ id + "?clentryid=" + clentryid);
        }
        this.unreserve = function (id) {
            return $http.post("/Lookup/Unreserve/" + id);
        }
    })
    .controller('LookupController', function ($scope, $log, LookupService) {
        getLookup();

        lookup = null;
        selectedid = 0; 
        clentryid = 0;

        function getLookup() {
            var serviceCall = LookupService.getLookup();
            serviceCall.then(function (data) {
                $scope.lookup = data.data;
            }, function(error)
            {
                $log.error('Oops! Something went wrong while fetching the data!')
            })
        } 

        $scope.updateLookup = function (item) {
            var newlookup = new Array(0);
            angular.forEach($scope.lookup, function (value, key) {
                if (item.ID == value.ID) {
                    newlookup.push(item)
                }
                else {
                    newlookup.push(value);
                }
            });
            $scope.lookup = newlookup;
        }

        $scope.reserve = function() {
            var serviceCall = LookupService.reserve($scope.selectedid, $scope.clentryid);
            serviceCall.then(function (data) {
                var reservedItem = data.data;
                $scope.updateLookup(reservedItem);
            }, function (error) {
                    $log.error('Oops! Something went wrong while reserving the item!')
                })
        }

        $scope.unreserve = function () {
            var serviceCall = LookupService.unreserve($scope.selectedid);
            serviceCall.then(function (data) {
                var reservedItem = data.data;
                $scope.updateLookup(reservedItem);
            }, function (error) {
                $log.error('Oops! Something went wrong while unreserving the item!')
            })
        }
    });