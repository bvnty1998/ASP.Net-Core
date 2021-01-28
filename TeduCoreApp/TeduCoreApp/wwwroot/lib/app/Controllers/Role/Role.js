var RoleController = function () {
    this.initialize = function () {
        loadData();
        ResgisterEvent();
    }
    //resgisrter event
    function ResgisterEvent() {
        // search roles
        $('#btnSearch').on('click', function () {
            loadData();
        })
        //Create user
        $('#btn_Create_Role').on('click', function () {
            resetFrom();
            $('#fmModalRole').modal('show');
        })
        // save data when admin add or update roel
        $('#btn_SaveRole').on('click', function () {
            $.ajax({
                type: 'POST',
                dataType: 'JSON',
                data:
                {
                    id: $('#hidIdM').val(),
                    Name: $('#txtNameM').val(),
                    Description: $('#txtDescriptionM').val()
                },
                url: '/admin/role/SaveRoleAsync',
                success: function (response) {
                    if (response == true) {
                        tedu.notify('save data success !', 'success');
                        window.location.href = '/admin/role/index'
                    }
                },
                error: function (status) {
                    tedu.notify('Error while save data ', 'error');
                }
            })
        })
        // edit Role
        $('body').on('click', '.btnEdit', function () {
            var id = $(this).attr('id');
            $('#txtNameM').attr('disabled', true);
            $.ajax({
                type: 'POST',
                data: {
                    Id: id
                },
                url: '/admin/role/GetRoleByIdAsync',
                success: function (response) {
                    $('#hidIdM').val(response.id);
                    $('#txtNameM').val(response.name);
                    $('#txtDescriptionM').val(response.description);
                    $('#fmModalRole').modal('show');
                },
                error: function (status) {
                    tedu.notify('Edit not error ','error')
                }
            })
        })
        // delete role
        $('body').on('click', '.btnDelete', function () {
            var id = $(this).attr('id');
            tedu.confirm('Do you want delete ?', function () {
                $.ajax({
                    type: 'POST',
                    data: {
                        Id: id
                    },
                    url: '/admin/role/DeleteAsync',
                    success: function (response) {
                        if (response == true) {
                            tedu.notify('delete role success ', 'success');
                            window.location.href = '/admin/role/index'
                        }
                    },
                    error: function (status) {
                        tedu.notify('delete role faild ', 'error')
                    }
                })
            })
        })
    }
    function loadData() {
        $.ajax({
            type: 'GET',
            dataType: 'JSON',
            data: {
                keyWord: $('#txtkeyword').val(),
                pageCurrent: tedu.cofigs.pageIndex,
                pageSize: tedu.cofigs.pageSize
            },
            url: '/admin/Role/GetAllPagingAsync',
            success: function (response) {
                var htm = $('#table_template').html();
                var render = '';
                $.each(response.result, function (i, item) {
                    render += Mustache.render(htm, {
                        Id: item.id,
                        Name: item.name,
                        Description: item.description
                    })
                })
                $('#tbl-Content').html(render)
                $('#lblTotalRecord').text(response.rowCount);
                wrapPaging(response.pageCount);
            },
            error: function (status) {
                tedu.notify('Error while load list role', 'error')
            }
        })
    }
    // paging use library twbsPagination
    function wrapPaging(pageCount) {
        $('#paginationaUL').twbsPagination({
            totalPage: pageCount,
            visibalePages: 7,
            first: 'Đầu',
            prev: 'Sau',
            next: 'Tiếp',
            last: 'cuối',
            onPagesClick: function (event, p) {
                if (onPagesClick = true) {
                    tedu.cofigs.pageIndex = p;
                    loadData();
                }
            }
        })
    }
    //Reset from
    function resetFrom() {
        $('#txtNameM').val('');
        $('#txtDescriptionM').val('');
    }
}