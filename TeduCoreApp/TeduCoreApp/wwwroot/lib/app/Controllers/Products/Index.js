var productController = function () {
    this.initialize = function () {
        loadData();
        loadCategory();
        registerEvents();
       
       
    }
    function registerEvents() {
        $('#ddlShowPage').on('change', function () {
            tedu.cofigs.pageSize = $(this).val();
            tedu.cofigs.pageIndex = 1;
            loadData(true);
        });
        $('#btnSearch').on('click', function () {
            console.log($('#ddlProductCategory').val());
            loadData();
        });

    }
    function loadData(isPageChaged) {
        var template = $('#table_tamplate').html();
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
                categoryId: $('#ddlProductCategory').val(),
                keyword: $('#txtKeyWord').val(),
                page: tedu.cofigs.pageIndex,
                pageSize: tedu.cofigs.pageSize
            },
            url: '/admin/Product/GetAllPaging',
            dataType: 'JSON',
            success: function (response) {
                $.each(response.result, function (i, item)
                {
                    //console.log(item.productCategory.name);
                    
                    render += Mustache.render(template, {
                        Id: item.id,
                        Name: item.name,
                        Images: item.image == null ? '<img src="~/images/user.png" />' : '' + item.image + '',
                        CreatedDate: tedu.dateTimeFormatJSon(item.dateCreated),
                        Price: tedu.formatNumber(item.price,0),
                        CategoryName: item.productCategory.name,
                        Status: tedu.getStatus(item.status),
                    });
                   
                    if (render != '') {
                      
                        $('#tbl-content').html(render);
                    }
                   
                })
                $('#lblTotalRecord').text(response.pageCount);
                wrapPaging(response.rowCount, function () {
                    loadData()
                }, isPageChaged)
            },
            error: function (status) {
                console.log(status);
                tedu.notify('Canot loading data from product !','error')
            }
        })
    }

    function wrapPaging(recordCount, callBack, changePageSize) {
        var totalSize = Math.ceil(recordCount / tedu.cofigs.pageSize)
        
        $('#paginationaUL').twbsPagination({
            totalPages : totalSize,
            visiblePages: 7,
            first: 'Đầu',
            prev: 'Trước',
            next: 'Tiếp',
            last: 'Cuối',
            onPageClick: function (event, p) {
                if (onPageClick = true) {
                    tedu.cofigs.pageIndex = p;
                    loadData();
                }
            }
        });
        
    }

    function loadCategory() {
        var render = $('#ddlProductCategory').html();

        $.ajax({
            type: 'GET',
            data: {

            },
            url: '/admin/ProductCategory/GetAllCategory',
            dataType: 'JSON',
            success: function (reponse) {
                $.each(reponse, function (i, item) {
                    render += '<option value="' + item.id + '">' + item.name + '</option>';
                    $('#ddlProductCategory').html(render);
                })
            },
            error: function (status) {
                tedu.notify('Can not loading Product Category', 'error')
            }
        })
    }
}
