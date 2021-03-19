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

        // popup form Permission when click icon permission
        $('body').on('click', '.btnPermission', function () {
            var roleId = $(this).attr('id');
            //load form 
            loadFormPermission(roleId);
            $.ajax({
                type: 'POST',
                data: {
                    roleId: roleId
                },
                url: '/admin/role/GetPermissonByRoleId',
                success: function (response) {
                    if (response != 0) {
                        var data = $('#tblFunction tbody tr')
                        $.each(data, function (i, item) {
                            var roleId = $(item).find('.ckAdd').attr('id');

                            $.each(response, function (j, jitem) {
                                if (jitem.functionId == roleId) {
                                    $(item).find('.ckAdd').prop('checked', jitem.canCreate);
                                    $(item).find('.ckUpdate').prop('checked', jitem.canUpdate);
                                    $(item).find('.ckDelete').prop('checked', jitem.canDelete);
                                    $(item).find('.ckView').prop('checked', jitem.canRead);
                                }
                            })
                        })
                    }
                    else {
                        loadFormPermission(roleId)
                    }
                },
                error: function (status) {
                    tedu.notify('error while load data ', 'error')
                }
            })
           
           
            //load function to form AssginPermission

        })

        // event check Add role
        $('table').on('click', '.ckAdd', function () {
            var check = $(this).prop('checked');
            if (check == true) {
                var parentId = $(this).prop('value');
                // if parentid equals null it is root
                if (parentId == null || parentId == '') {
                    $(this).prop('checked', true);
                    $('.treegrid-parent-' + $(this).prop('id') + ' .ckAdd').prop('checked', true)

                }
                // else it is children
                else {
                    $(this).prop('checked', true)
                }
            }
            else {
                var parentId = $(this).prop('value');
                // if parentid equals null it is root
                if (parentId == null || parentId == '') {
                    $(this).prop('checked', false);
                    $('.treegrid-parent-' + $(this).prop('id') + ' .ckAdd').prop('checked', false)
                }
                // else it is children
                else {
                    $(this).prop('checked', false)
                }
            }
            
           
           
        })

        // event check update role
        $('table').on('click', '.ckUpdate', function () {
            var check = $(this).prop('checked');
            if (check == true) {
                var parentId = $(this).prop('value');
                // if parentid equals null it is root
                if (parentId == null || parentId == '') {
                    $(this).prop('checked', true);
                    $('.treegrid-parent-' + $(this).prop('id') + ' .ckUpdate').prop('checked', 'checked')

                }
                // else it is children
                else {
                    $(this).prop('checked', true)
                }
            }
            else {
                var parentId = $(this).prop('value');
                // if parentid equals null it is root
                if (parentId == null || parentId == '') {
                    $(this).prop('checked', false);
                    $('.treegrid-parent-' + $(this).prop('id') + ' .ckUpdate').prop('checked', false)

                }
                // else it is children
                else {
                    $(this).prop('checked', false)
                }
            }



        })

        // event check View role
        $('table').on('click', '.ckView', function () {
            var check = $(this).prop('checked');
            if (check == true) {
                var parentId = $(this).prop('value');
                // if parentid equals null it is root
                if (parentId == null || parentId == '') {
                    $(this).prop('checked', true);
                    $('.treegrid-parent-' + $(this).prop('id') + ' .ckView').prop('checked', 'checked')

                }
                // else it is children
                else {
                    $(this).prop('checked', true)
                }
            }
            else {
                var parentId = $(this).prop('value');
                // if parentid equals null it is root
                if (parentId == null || parentId == '') {
                    $(this).prop('checked', false);
                    $('.treegrid-parent-' + $(this).prop('id') + ' .ckView').prop('checked', false)

                }
                // else it is children
                else {
                    $(this).prop('checked', false)
                }
            }



        })

        // event check Delete  role
        $('table').on('click', '.ckDelete', function () {
            var check = $(this).prop('checked');
            if (check == true) {
                var parentId = $(this).prop('value');
                // if parentid equals null it is root
                if (parentId == null || parentId == '') {
                    $(this).prop('checked', true);
                    $('.treegrid-parent-' + $(this).prop('id') + ' .ckDelete').prop('checked', 'checked')

                }
                // else it is children
                else {
                    $(this).prop('checked', true)
                }
            }
            else {
                var parentId = $(this).prop('value');
                // if parentid equals null it is root
                if (parentId == null || parentId == '') {
                    $(this).prop('checked', false);
                    $('.treegrid-parent-' + $(this).prop('id') + ' .ckDelete').prop('checked', false)

                }
                // else it is children
                else {
                    $(this).prop('checked', false)
                }
            }



        })

        

        // delete permission of role
        $('#btn_SavePermission').on('click', function () {
            //console.log($('#tblFunction tbody tr'))
            var data = $('#tblFunction tbody tr')
            var listPermission = [];
            $.each(data, function (i, item) {
                var permission =
                {
                    RoleId: $('#btn_UserId').text(),
                    FunctionId: $(item).find('.ckAdd').prop('id'),
                    CanCreate: $(item).find('.ckAdd').prop('checked'),
                    CanRead: $(item).find('.ckView').prop('checked'),
                    CanUpdate: $(item).find('.ckUpdate').prop('checked'),
                    CanDelete: $(item).find('.ckDelete').prop('checked'),
                }
                listPermission.push(permission)
            })
            console.log(listPermission);
            $.ajax({
                type: 'POST',
                data: {
                    roleId: $('#btn_UserId').text(),
                    listPermissionVM: listPermission
                },
                url: '/admin/role/DeletePermission',
                success: function (reponse) {
                    tedu.notify('add permission succees ', 'success');
                    window.location.href = '/admin/role/index';
                },
                error: function (status) {
                    tedu.notify('save permission faild ', 'error');
                }
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

    //load form Permission check role
    function loadFormPermission(roleId) {
        $('#btn_UserId').text(roleId);
        $.ajax({
            type: 'GET',
            dataType: 'JSON',
            data: {

            },
            url: '/admin/role/GetAllFunction',
            success: function (response) {
                var html = $('#table_template_AssignPermission').html();
                var render = '';
                $.each(response, function (i, item) {
                    render += Mustache.render(html, {
                        Id: item.id,
                        Name: item.name,
                        FnId: item.id

                    })
                    $.each(item.children, function (j, jitem) {
                        render += Mustache.render(html, {
                            Id: jitem.id + ' treegrid-parent-' + jitem.parentId,
                            Name: jitem.name,
                            ParentId: jitem.parentId,
                            FnId: jitem.id
                        })
                    })
                })
                //console.log(render);
                $('#tbl_Content_AssignPermission').html(render)
                $('.tree').treegrid();
                $('#fmModal_AssignPermission').modal('show');
                
            },
            error: function (status) {
                tedu.notify('Load list function fail ', 'error')
            }
        })
    }
}