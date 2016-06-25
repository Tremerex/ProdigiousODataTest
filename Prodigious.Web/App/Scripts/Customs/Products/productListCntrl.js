app.controller('productListCntrl', productListCntrl);

productListCntrl.$inject = ['$scope', 'ngTableExtend', 'productService'];

function productListCntrl($scope, ngTableExtend, productService) {

    var vm = this;

    vm.busy = {
        delay: 0,
        minDuration: 0,
        message: 'Please Wait...',
        backdrop: true,
        promise: null
    }

    vm.tableParams = new ngTableExtend({
        page: 1,
        count: 10,
        sorting: {
            ProductID: 'asc',
        }
    }, {
        total: 0,
        filterDelay: 1200,
        getData: function ($defer, params) {
            vm.busy.promise = productService.query({
                $top: params.count(),
                $skip: params.getSkip(),
                $orderby: params.getOrderBy(),
                $inlinecount: 'allpages',
                $filter: params.getFilter(),
                $select: 'ProductID,Name,ProductNumber,Color,ListPrice,ModifiedDate'
            }, function (data) {
                params.total(data.count);
                $defer.resolve(data.items);
            });
        }
    });

    $scope.$watch('vm.enableFilter', function (newValue, oldValue) {
        if (newValue != oldValue) {
            var filter = vm.tableParams.filter();
            Object.keys(filter).forEach(function (a) {
                filter[a] = null;
            });
        }
    });

}
