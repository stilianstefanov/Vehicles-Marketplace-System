$(document).ready(function () {
    var currentPage = 1;

    var isUserRegisteredAsACompany = $('#isUserRegisteredAsACompany').val() === 'true';

    var currentUserCompanyId = $('#currentUserCompanyId').val();

    loadData();

    $('#search').on('input', function () {
        currentPage = 1;
        loadData();
    });


    $('#sort').on('change', function () {
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

        if (pageDirection === 'previous' && currentPage > 1) {
            currentPage--;
        } else if (pageDirection === 'next') {
            currentPage++;
        }

        $.ajax({
            url: '/Accessory/AllFilteredAndPaged',
            type: 'GET',
            data: {
                searchTerm: search,
                accessorySorting: sort,
                accessoriesPerPage: perPage,
                currentPage: currentPage,
            },
            success: function (result) {
                $('#productsContainer').empty();

                result.accessories.forEach(function (accessory) {
                    var card =
                        '<div class="card mb-3">' +
                        '<div class="row g-0">' +
                        '<div class="col-md-4">' +
                        '<img src="data:' +
                        accessory.product.image.imageType +
                        ';base64,' +
                        accessory.product.image.data +
                        '" class="img-fluid image-equal" alt="Company Image">' +
                        '</div>' +
                        '<div class="col-md-8">' +
                        '<div class="card-body">' +
                        '<h5 class="card-title">' +
                        accessory.product.name +
                        '</h5>' +
                        '<p class="card-text">' +
                        '<strong>Price:</strong> ' +
                        accessory.product.price +
                        '<br>' +
                        '<strong>Quantity:</strong> ' +
                        accessory.product.quantity +
                        '<br>' +
                        '<strong>Brand:</strong> ' +
                        accessory.brand +
                        '<br>' +
                        '<strong>Compatible Brand:</strong> ' +
                        accessory.compatibleBrand +
                        '<br>' +
                        '<strong>Compatible Model:</strong> ' +
                        accessory.compatibleModel +
                        '<br>' +
                        '</p>' +
                        '</div>' +
                        '</div>' +
                        '</div>' +
                        '<div class="card-footer d-flex justify-content-center">' +
                        '<div class="btn-group" role="group">' +
                        '<a href="/Accessory/Details/' +
                        accessory.id +
                        '" class="btn btn-primary">Details</a>';

                    if (!isUserRegisteredAsACompany) {
                        card +=
                            '<a href="/Cart/AddToCart/' +
                            accessory.product.id +
                            '" class="btn btn-success">Add to Cart</a>' +
                            '<a href="/Review/AddToProduct/' +
                            accessory.product.id +
                            '" class="btn btn-info">Add Review</a>';
                    }
                    else if (accessory.product.companyId.toLowerCase() == currentUserCompanyId) {
                        card += '<a href="/Product/Edit/' + accessory.product.id + '" class="btn btn-warning">Edit</a>' +
                            '<a href="/Product/Details/' + accessory.product.id + '" class="btn btn-danger">Delete</a>';
                    }


                    card += '</div>' + '</div>' + '</div>';

                    $('#productsContainer').append(card);
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