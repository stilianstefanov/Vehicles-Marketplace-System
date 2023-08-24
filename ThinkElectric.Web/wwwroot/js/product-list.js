$(document).ready(function () {
    var currentPage = 1;

    var isUserRegisteredAsACompany = $('#isUserRegisteredAsACompany').val() === 'true';

    var currentUserCompanyId = $('#currentUserCompanyId').val();

    var isUserAdmin = $('#isUserAdmin').val() === 'true';

    var url = window.location.href;
    var parts = url.split('/');
    var companyId = parts.pop();

    loadData();

    $('#search').on('input', function () {
        currentPage = 1;
        loadData();
    });

    $('#sort').on('change', function () {
        currentPage = 1;
        loadData();
    });

    $('#type').on('change', function () {
        currentPage = 1;
        loadData();
    });


    $('#perPage').on('change', function () {
        currentPage = 1;
        loadData();
    });


    $('#previousPage').on('click', function () {
        if (currentPage > 1) {
            loadData('previous');
        }
    });


    $('#nextPage').on('click', function () {
        loadData('next');
    });

    function loadData(pageDirection) {
        var search = $('#search').val();
        var sort = $('#sort').val();
        var perPage = $('#perPage').val();
        var type = $('#type').val();

        if (pageDirection === 'previous' && currentPage > 1) {
            currentPage--;
        } else if (pageDirection === 'next') {
            currentPage++;
        }

        $.ajax({
            url: '/Product/AllByCompanyIdFilteredAndPaged',
            type: 'GET',
            data: {
                searchTerm: search,
                productSorting: sort,
                productsPerPage: perPage,
                currentPage: currentPage,
                companyId: companyId,
                queryProductType: type
            },
            success: function (result) {
                $('#productContainer').empty();

                result.products.forEach(function (product) {
                    var card = '<div class="col-md-6 col-lg-4">' +
                        '<div class="card">' +
                        '<img src="data:' + product.image.imageType + ';base64,' + product.image.data + '" class="img-fluid image-equal" alt="Company Image">' +
                        '<div class="card-body">' +
                        '<h5 class="card-title">' + product.name;

                    if (product.quantity == 0) {
                        card += ' <span class="text-danger">Out of stock</span>';
                    }

                        card +=
                        '</h5>' +
                        '<p class="card-text">' +
                        '<strong>Price: </strong>' + product.price + '<br>' +
                        '<strong>Quantity:</strong> ' + product.quantity + '<br>' +
                        '<strong>Added On:</strong> ' + product.createdOn + '<br>' +
                        '<strong>Type:</strong> ' + product.productType + '<br>' +
                        '</p>' +
                        '</div>' +
                        '<div class="card-footer d-flex justify-content-center">' +
                        '<div class="btn-group" role="group">' +
                        '<a href="/Product/Details/' + product.id + '" class="btn btn-primary">Details</a>';


                    if (!isUserRegisteredAsACompany)
                    {
                        if (product.quantity > 0)
                        {
                            card += '<a href="/Cart/AddToCart/' + product.id + '" class="btn btn-success">Add to Cart</a>';
                        }
                        card +=
                            '<a href="/Review/AddToProduct/' + product.id + '" class="btn btn-info">Add Review</a>';
                    }

                    if (product.companyId.toLowerCase() == currentUserCompanyId || isUserAdmin) {
                        card += '<a href="/Product/Edit/' + product.id + '" class="btn btn-warning">Edit</a>' +
                            '<a href="/Product/Details/' + product.id + '" class="btn btn-danger">Delete</a>';
                    }

                    card +=
                        '</div>' +
                        '</div>' +
                        '</div></div>';

                    $('#productContainer').append(card);
                });


                $('#previousPage').prop('disabled', currentPage === 1);
                $('#nextPage').prop('disabled', currentPage === result.totalPages);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
});