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


    $('#type').on('change', function () {
        currentPage = 1;
        loadData();
    });


    $('#perPage').on('change', function () {
        currentPage = 1;
        loadData();
    });

    $('#engineType').on('change', function () {
        currentPage = 1;
        loadData();
    });

    $('#brakesType').on('change', function () {
        currentPage = 1;
        loadData();
    });

    $('#suspensionType').on('change', function () {
        currentPage = 1;
        loadData();
    });

    $('#frameType').on('change', function () {
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
        var engineType = $('#engineType').val();
        var brakesType = $('#brakesType').val();
        var suspensionType = $('#suspensionType').val();
        var frameType = $('#frameType').val();


        if (pageDirection === 'previous' && currentPage > 1) {
            currentPage--;
        } else if (pageDirection === 'next') {
            currentPage++;
        }

        $.ajax({
            url: '/Bike/AllFilteredAndPaged',
            type: 'GET',
            data: {
                searchTerm: search,
                bikeSorting: sort,
                bikesPerPage: perPage,
                currentPage: currentPage,
                queryBikeType: type,
                queryBikeEngineType: engineType,
                queryBikeBrakesType: brakesType,
                queryBikeSuspensionType: suspensionType,
                queryBikeFrameType: frameType
            },
            success: function (result) {
                $('#productsContainer').empty();

                result.bikes.forEach(function (bike) {
                    var card =
                        '<div class="card mb-3">' +
                        '<div class="row g-0">' +
                        '<div class="col-md-5">' +
                        '<img src="data:' +
                        bike.product.image.imageType +
                        ';base64,' +
                        bike.product.image.data +
                        '" class="img-fluid image-equal" alt="Company Image">' +
                        '</div>' +
                        '<div class="col-md-7">' +
                        '<div class="card-body">' +
                        '<h5 class="card-title">' +
                        bike.product.name +
                        '</h5>' +
                        '<p class="card-text">' +
                        '<strong>Price:</strong> ' +
                        bike.product.price +
                        '<br>' +
                        '<strong>Quantity:</strong> ' +
                        bike.product.quantity +
                        '<br>' +
                        '<strong>Brand:</strong> ' +
                        bike.brand +
                        '<br>' +
                        '<strong>Color:</strong> ' +
                        bike.color +
                        '<br>' +
                        '<strong>Range:</strong> ' +
                        bike.range +
                        ' km<br>' +
                        '<strong>Max Speed:</strong> ' +
                        bike.maxSpeed +
                        ' km/h<br>' +
                        '<strong>Engine Power:</strong> ' +
                        bike.enginePower +
                        ' kW<br>' +
                        '<strong>Model:</strong> ' +
                        bike.model +
                        '<br>' +
                        '<strong>Bike Type:</strong> ' +
                        bike.bikeType +
                        '<br>' +
                        '<strong>Engine Type:</strong> ' +
                        bike.engineType +
                        '<br>' +
                        '<strong>Brakes Type:</strong> ' +
                        bike.brakesType +
                        '<br>' +
                        '<strong>Suspension Type:</strong> ' +
                        bike.suspensionType +
                        '<br>' +
                        '<strong>Frame Type:</strong> ' +
                        bike.frameType +
                        '<br>' +
                        '</p>' +
                        '</div>' +
                        '</div>' +
                        '</div>' +
                        '<div class="card-footer d-flex justify-content-center">' +
                        '<div class="btn-group" role="group">' +
                        '<a href="/Bike/Details/' +
                        bike.id +
                        '" class="btn btn-primary">Details</a>';

                    if (!isUserRegisteredAsACompany) {
                        card +=
                            '<a href="/Cart/AddToCart/' +
                            bike.product.id +
                            '" class="btn btn-success">Add to Cart</a>' +
                            '<a href="/Review/AddToProduct/' +
                            bike.product.id +
                            '" class="btn btn-info">Add Review</a>';
                    }
                    else if (bike.product.companyId.toLowerCase() == currentUserCompanyId) {
                        card += '<a href="/Product/Edit/' + bike.product.id + '" class="btn btn-warning">Edit</a>' +
                            '<a href="/Product/Details/' + bike.product.id + '" class="btn btn-danger">Delete</a>';
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