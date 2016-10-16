(function (angular, global) {
    "use strict";

    global.app = angular.module("framework", []);

    global.app.config(function () {
            //cfg.
        })
        .controller("mainCtrl",
            function($scope, $http) {

                //$http.get("/API/foo/notfound", { cache: false })
                //    .then(function(response) {
                //        console.log(response);
                //        console.log("ok response");
                //    }, function() {
                //        console.log("error response");
                //    });

                $scope.insertUser = function() {

                    $http.post("/api/user",
                        {
                            FirstName: "First Name",
                            LastName: "Last Name",
                            Password: "123",
                            Email: "user@domain.com"
                        })
                        .then(function(response) {
                                console.log(response);
                                console.log("ok response");
                            },
                            function (response) {
                                console.log(response);
                                console.log("error response");
                            });

                };



            })
        .run();

})(angular, this);
