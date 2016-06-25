app.factory('ngTableExtend', ngTableExtend);

ngTableExtend.$inject = [
    'ngTableParams'
];

function ngTableExtend(ngTableParams) {
    ngTableParams.prototype = {
        getSkip: function () {
            return this.count() * (this.page() - 1);
        },
        getOrderBy: function () {
            return this.orderBy().map(function (_, j) {
                var arr = _.split(/(^[\-\+])/g);
                return arr[2] + (arr[1] == '+' ? '' : ' desc');
            }).join(',');
        },
        getFilter: function () {
            var obj = this.filter();
            return Object.keys(obj).reduce(function (a, b) {
                if (obj[b]) a.push('startswith(tolower(' + b + '),tolower(\'' + obj[b] + '\'))');
                return a;
            }, []).join(' and ') || null;
        }
    }
    return ngTableParams;
}