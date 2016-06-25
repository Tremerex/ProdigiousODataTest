app.controller('productDetailsCntrl', productDetailsCntrl);

productDetailsCntrl.$inject = [
    '$scope',
    '$stateParams',
    '$filter',
    'ngTableExtend',
    'productService'
];

function productDetailsCntrl($scope, $stateParams, $filter, ngTableExtend, productService) {

    var vm = this;

    vm.busy = {
        delay: 0,
        minDuration: 0,
        message: 'Please Wait...',
        backdrop: true,
        promise: null
    }

    if ($stateParams.id != null) {
        vm.busy.promise = productService.query({
            id: $stateParams.id,
            $expand: 'ProductCategory,ProductModel/ProductModelProductDescriptions/ProductDescription'
        }, function (data) {
            vm.product = data;
            vm.productDescription = new ngTableExtend({}, {
                getData: function ($defer, params) {
                    vm.values = $filter('filter')(vm.product.productModel.productModelProductDescriptions, params.filter());
                    vm.values = $filter('orderBy')(vm.values, params.orderBy());
                    $defer.resolve(vm.values);
                },
                counts: null
            })
        });
    }

}