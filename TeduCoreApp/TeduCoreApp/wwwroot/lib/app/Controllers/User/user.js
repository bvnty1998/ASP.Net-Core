﻿var UserController = function () {
    this.initialize = function () {
        loadData();
        RegisterEvent();
    }
    function RegisterEvent() {
        // search user
        $('#btnSearch').on('click', function (){
            loadData();
        })
        // load user to from edit
        $('body').on('click', '.btnEdit', function () {
            var id = $(this).attr('id');
            console.log(id);
            $.ajax({
                type: 'POST',
               dataType: 'JSON',
                data: {
                    id:id
                },
                url: '/admin/User/FindById',
                success: function (response) {
                    $('#hidIdM').val(response.id);
                    $('#txtFullNameM').val(response.fullName);
                    $('#txtUserNameM').val(response.userName);
                    $('#txtEmailM').val(response.email);
                    $('#txtPhoneM').val(response.phoneNumber);
                    $('#txtPassWordM').val(response.passwordHash);
                    initialize(response.roles);
                    $('#txtPhoneM').attr('checked', response.status == 1);
                    $('#modalUser').modal('show');
                },
                error: function (status) {
                    tedu.notify('error while loading user to from edit !','error')
                }
            })
        })
        // Create User 
        $('#btnCreate').on('click', function () {
            resetForm();
            initialize('');
            $('#modalUser').modal('show');
        })
       
        $('#btn_Save').on('click', function () {
            var roles = [];
            var dt = $('[name="ckRoles"]')
            //console.log(dt)
            $.each(dt, function (i, item) {
                if (item.checked == true) {
                    roles.push(item.value);
                }
            })
            console.log(roles)
         
            $.ajax({
                type: 'POST',
                data: {
                    Roles: roles,
                    Id: $('#hidIdM').val(),
                    FullName: $('#txtFullNameM').val(),
                    UserName: $('#txtUserNameM').val(),
                    Email: $('#txtEmailM').val(),
                    PasswordHash: $('#txtPassWordM').val(),
                    PhoneNumber: $('#txtPhoneM').val(),
                    Status: $('#ckbActiveM').attr('checked') == true ? 1 : 0
                },
                url: '/admin/User/Save',
                success: function (response) {
                    if (response.status == 200) {
                        location:'/admin/User/index'
                    }
                },
                error: function (status) {
                    tedu.notify('Error while add or edit user','error')
                }
            })
          
        })
    }
    // load data 
    function loadData() {
        var tamplate = $('#table_template').html();
        //console.log($('#tbl-Content').html());
        var render = "";
        $.ajax({
            type: "GET",
            dataType: "JSON",
            url:'/admin/User/GetAll',
            data: {
                keyword: $('#txtKeyWord').val(),
                page: tedu.cofigs.pageIndex,
                pageSize: tedu.cofigs.pageSize
            },
            success: function (response) {
                //console.log(response.result);
                $.each(response.result, function (i, item) {
                    render += Mustache.render(tamplate, {
                        Id: item.id,
                        Account: item.userName,
                        Name: item.fullName,
                        Avartar: item.avartar == null ? '<img src="/images/user.png" width="30" height="30"/>' : '<img src="' + item.avartar + '" width="30" height="30"/>',
                        CreateDate: tedu.dateTimeFormatJSon(item.createDate),
                        Status: tedu.getStatus(item.status)
                    });
                })
                if (render != "") {
                    $('#tbl-Content').html(render);
                }
                $('#lblTotalRecord').val(response.result.rowCount);
                wrapPaging(response.result.rowCount);
               
            },
            error: function (status) {
                tedu.notify('Error while loading data', 'error');
            }
        })
        
    }
    // pagination (phân trang)
    function wrapPaging(totalRecord) {
        var totalPage = Math.ceil(totalRecord / tedu.cofigs.pageSize);
        $('#paginationaUL').twbsPagination({
            totalPage: totalPage,
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

    // initialize Role
    function initialize(roleChecked) {
        var role_template = $('#role_template').html();
        var render = "";
        $.ajax({
            type:'GET',
            dataType:'JSON',
            data: {

            },
            url: '/admin/Role/GetAll',
            success: function (response) {
                $.each(response.result, function (i, item) {
                    var checked = '';
                    if (roleChecked.indexOf(item.name) != -1) {
                        checked = 'checked'
                    }
                    render += Mustache.render(role_template, {
                        Id:item.id,
                        Checked: checked,
                        Name: item.name,
                        Description: item.description
                    })
                })
                if (render !== null) {
                    $('#list_roles').html(render)
                }
            },
            error: function (status) {
                tedu.notify('error while loading list roles !', 'error');
            }
        })
    }
    // reset form
    function resetForm() {
        $('#hidIdM').val('');
        $('#txtFullNameM').val('');
        $('#txtUserNameM').val('');
        $('#txtEmailM').val('');
        $('#txtPhoneM').val('');
        $('#txtPassWordM').val('');
        initialize('');
        $('#txtCofirmPassWords').val('');
    }
}