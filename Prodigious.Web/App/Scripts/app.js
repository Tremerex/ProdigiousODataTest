var app = angular.module('app', ['ui.router', 'ngResource', 'cgBusy', 'ngTable']);

app.config(["$stateProvider", "$urlRouterProvider",
        function ($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise("/Product");
            $stateProvider
                .state("ProductList", {
                    url: "/Product",
                    templateUrl: "App/Views/Product/list.html",
                    controller: "productListCntrl",
                    controllerAs: 'vm'
                })
                .state("Product", {
                    url: "/Product/:id",
                    templateUrl: "App/Views/Product/details.html",
                    controller: "productDetailsCntrl",
                    controllerAs: "vm"
                })
        }]
);

if (!Array.prototype.map) {
    Array.prototype.map = function (c, a) {
        if (this == null)
            throw new TypeError(' this is null or not defined');
        if (typeof c !== 'function')
            throw new TypeError(c + ' is not a function');
        var l = this.length, _this = this, arr = [], i = 0;
        while (l > i)
            arr[i] = c.call(a, _this[i], i++, _this);
        return arr;
    }
}

if (!Array.prototype.reduce) {
    Array.prototype.reduce = function (c) {
        if (this == null)
            throw new TypeError(' this is null or not defined');
        if (typeof c !== 'function')
            throw new TypeError(c + ' is not a function');
        var l = this.length, _this = this, i = 0, s;
        s = arguments.length > 1 ? arguments[1] : _this[i++];
        while (l > i)
            s = c.call(null, s, _this[i], i++, _this);
        return s;
    }
}