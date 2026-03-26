$(document).ready(function () {
    initOpenStoresButton();  
    initStoreDetailsButton();
    initProductsButton();
});

function initProductsButton() {
    $(document).on('click', '.btn-products', function () {
        var storeId = $(this).data('store-id');
        var storeName = $(this).data('store-name');

        $('#storeName').text(storeName);

        $('#storeDetailsModal').modal('hide');
        $('#openStoresModal').modal('hide');

        $('#productsModalBody').html('<div class="text-center p-3">Loading...</div>');
        $('#productsModal').modal('show');

        $.get(`/Store/${storeId}/products`, function (html) {
            $('#productsModalBody').html(html);
        });
    });
}

function initOpenStoresButton() {
    $('#btnOpenStores').click(function () {

        $('#storeDetailsModal').modal('hide');
        $('#productsModal').modal('hide');

        $('#openStoresModal').modal('show');

        $.get('/Store/open', function (html) {
            $('#openStoresModalBody').html(html);
        });
    });
}

function initStoreDetailsButton() {
    $(document).on('click', '.btn-store-details', function () {
        var storeId = $(this).data('store-id');
        var storeName = $(this).data('store-name');

        $('#storeDetailsName').text(storeName);

        $('#openStoresModal').modal('hide');
        $('#productsModal').modal('hide');

        $('#storeDetailsModalBody').html('<div class="text-center p-3">Loading...</div>');
        $('#storeDetailsModal').modal('show');

        $.get(`/Store/${storeId}/details`, function (html) {
            $('#storeDetailsModalBody').html(html);
        });
    });
}