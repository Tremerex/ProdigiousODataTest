app.factory('productService', productService);

productService.$inject = ['$resource'];

function productService($resource) {
    return $resource("api/Product/:id", null, {
        query: {
            method: 'GET'
        },
        save: {
            method: 'POST'
        },
        update: {
            method: 'PUT'
        }
    });
}