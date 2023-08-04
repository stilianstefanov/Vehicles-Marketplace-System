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
            url: '/Company/AllFilteredAndPaged',
            type: 'GET',
            data: {
                searchTerm: search,
                companySorting: sort,
                companiesPerPage: perPage,
                currentPage: currentPage
            },
            success: function (result) {
                $('#companyContainer').empty();

                result.companies.forEach(function (company) {
                    var card = '<div class="col-md-6 col-lg-4 mt-4">' +
                        '<div class="card">' +
                        '<img src="data:' + company.image.imageType + ';base64,' + company.image.data + '" class="img-fluid image-equal" alt="Company Image">' +
                        '<div class="card-body">' +
                        '<h5 class="card-title">' + company.name + '</h5>' +
                        '<p class="card-text">' +
                        '<strong>Website:</strong> <a href="' + company.website + '" target="_blank">' + company.website + '</a><br>' +
                        '<strong>Founded:</strong> ' + company.foundedDate + '</p>' +
                        '</div>' +
                        '<div class="card-footer d-flex justify-content-center">' +
                        '<div class="btn-group" role="group">' +
                        '<a href="/Company/Details/' + company.id + '" class="btn btn-primary">Details</a>' +
                        '<a href="/Product/AllByCompanyId/' + company.id + '" class="btn btn-secondary">Products</a>';


                    if (!isUserRegisteredAsACompany) {
                        card += '<a href="/Review/AddToCompany/' + company.id + '" class="btn btn-info">Add Review</a>';
                    }

                    card += '</div>' +
                        '</div>' +
                        '</div></div>';

                    $('#companyContainer').append(card);
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