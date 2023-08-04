$(document).ready(function () {
    var currentPage = 1;
    var isUserRegisteredAsACompany = $('#isUserRegisteredAsACompany').val() === 'true';

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


        if (pageDirection === 'previous' && currentPage > 1) {
            currentPage--;
        } else if (pageDirection === 'next') {
            currentPage++;
        }

        $.ajax({
            url: '/Scooter/AllFilteredAndPaged',
            type: 'GET',
            data: {
                searchTerm: search,
                scooterSorting: sort,
                scootersPerPage: perPage,
                currentPage: currentPage,
                queryScooterType: type,
                queryScooterEngineType: engineType,
                queryScooterBrakesType: brakesType
            },
            success: function (result) {
                $('#productsContainer').empty();

                result.scooters.forEach(function (scooter) {
                    var card =
                        '<div class="card mb-3">' +
                        '<div class="row g-0">' +
                        '<div class="col-md-4">' +
                        '<img src="data:' +
                        scooter.product.image.imageType +
                        ';base64,' +
                        scooter.product.image.data +
                        '" class="img-fluid image-equal" alt="Company Image">' +
                        '</div>' +
                        '<div class="col-md-8">' +
                        '<div class="card-body">' +
                        '<h5 class="card-title">' +
                        scooter.product.name +
                        '</h5>' +
                        '<p class="card-text">' +
                        '<strong>Price:</strong> ' +
                        scooter.product.price +
                        '<br>' +
                        '<strong>Quantity:</strong> ' +
                        scooter.product.quantity +
                        '<br>' +
                        '<strong>Brand:</strong> ' +
                        scooter.brand +
                        '<br>' +
                        '<strong>Model:</strong> ' +
                        scooter.model +
                        '<br>' +
                        '<strong>Color:</strong> ' +
                        scooter.color +
                        '<br>' +
                        '<strong>Range:</strong> ' +
                        scooter.range +
                        ' km<br>' +
                        '<strong>Max Speed:</strong> ' +
                        scooter.maxSpeed +
                        ' km/h<br>' +
                        '<strong>Engine Power:</strong> ' +
                        scooter.enginePower +
                        ' kW<br>' +
                        '<strong>Scooter Type:</strong> ' +
                        scooter.scooterType +
                        '<br>' +
                        '<strong>Engine Type:</strong> ' +
                        scooter.engineType +
                        '<br>' +
                        '<strong>Brakes Type:</strong> ' +
                        scooter.brakesType +
                        '<br>' +
                        '</p>' +
                        '</div>' +
                        '</div>' +
                        '</div>' +
                        '<div class="card-footer d-flex justify-content-center">' +
                        '<div class="btn-group" role="group">' +
                        '<a href="/Scooter/Details/' +
                        scooter.id +
                        '" class="btn btn-primary">Details</a>';

                    if (!isUserRegisteredAsACompany) {
                        card +=
                            '<a href="/Cart/AddToCart/' +
                            scooter.product.id +
                            '" class="btn btn-success">Add to Cart</a>' +
                            '<a href="/Review/AddToProduct/' +
                            scooter.product.id +
                            '" class="btn btn-info">Add Review</a>';
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