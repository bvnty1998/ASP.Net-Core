var productCategory = function () {
    this.initialize = function () {
        registerEvents();
        loadProductCategory();
    }
    
}

function registerEvents() {
    //validate form fmMaintainance
    $('#fmMaintainance').validate({
        errorClass: 'red',
        ignore: [],
        lang: 'en',
        rules: {
            txtNameM:
            {
                required:true,
            },
            txtSeoAliasM:
            {
                required:true,
            },
            txtSortOrderM:
            {
                required: true,
            }
        },
        messages:
        {
            txtNameM:
            {
                required: 'enter name product category  !',
            },
            txtSeoAliasM:
            {
                required: 'enter alias product category  !',
            },
            txtSortOrderM:
            {
                required: 'enter sortoder product category  !',
            }
        }
    })
    // Create Product Category
    $('#bntCreate').off('click').on('click', function () {
        $('#modalLabel').text('Add Product Category');
        $('#txtNameM').val('');
        //$('#txtParentM').val(reponse.perentId);
        initialProductCategort('');
        $('#txtSeoAliasM').val('');
        $('#txtDescriptionM').val('');
        $('#txtSortOrderM').val('');
        $('#txtHomeOrderM').val('');
        $('#txtImageM').val('');
        $('#txtSeoTitleM').val('');
        //$('#txtUrlSeoM').val(reponse.urlseo);
        $('#txtSeoKeyWordM').val('');
        $('#txtDescriptionM').val('');
        $('#ckbActiveM').prop('checked', true);
        $('#ckbShowOnHomeM').prop('checked', false);
        // show modal 
        $('#AddEditModal').modal('show');
    })
    // Load Product Category to form Edit
    $('#btnEdit').off('click').on('click', function () {
        $('#modalLabel').text('Update Product Category');
        var id = $('#hidIdM').val();
        $.ajax({
            type: 'GET',
            data: {
                id: id
            },
            url: '/admin/ProductCategory/GetById',
            dataType: 'JSON',
            beforeSend: function () {
                tedu.startLoading()
            },
            success: function (reponse) {
                $('#txtNameM').val(reponse.name);
                //$('#txtParentM').val(reponse.perentId);
                initialProductCategort(reponse.perentId);
                $('#txtSeoAliasM').val(reponse.seoAlias);
                $('#txtDescriptionM').val(reponse.description);
                $('#txtSortOrderM').val(reponse.sortOrder);
                $('#txtHomeOrderM').val(reponse.homeOrder);
                $('#txtImageM').val(reponse.image);
                $('#txtSeoTitleM').val(reponse.seoPageTitle);
                //$('#txtUrlSeoM').val(reponse.urlseo);
                $('#txtSeoKeyWordM').val(reponse.seoKeywords);
                $('#txtDescriptionM').val(reponse.seodescription);
                $('#ckbActiveM').prop('checked', reponse.status == 1);
                $('#ckbShowOnHomeM').prop('checked', reponse.homeFlag == 1);
                // show modal 
                $('#AddEditModal').modal('show');
                tedu.stopLoading();
            },
            error: function (status) {

            }
        })
    })
    // Update Product Category
    $('#btnSaved').off('click').on('click', function (e) {
        e.preventDefault();
       
        //if ($('#ckbActiveM').val() == 'checked') { status = 1 } else { status = 0 }
        //console.log($('#ckbActiveM').prop('checked'))
        if ($('#fmMaintainance').valid()) {
            console.log($('#hidIdM').val());
            $.ajax({
                type: 'POST',
                data: {
                    Id: $('#hidIdM').val(),
                    Name: $('#txtNameM').val(),
                    PerentId: $('#txtParentM').val(),
                    HomeOrder: $('#txtHomeOrderM').val(),
                    SeoAlias: $('#txtSeoAliasM').val(),
                    SortOrder: $('#txtSortOrderM').val(),
                    Image: $('#txtImageM').val(),
                    SeoPageTitle: $('#txtSeoTitleM').val(),
                    Description: $('#txtDescriptionM').val(),
                    Status: $('#ckbActiveM').prop('checked') == true ? 1 : 0,
                    Description: $('#txtDescriptionM').val(),
                    SeoKeywords: $('#txtSeoKeyWordM').val()
                },
                //dataType:'JSON',
                url: '/admin/ProductCategory/Saved',
                success: function (reponse) {
                    tedu.notify('Update Product Category Success', 'success');
                    window.location.href = '/admin/ProductCategory/index';
                },
                error: function (status) {
                    tedu.notify('Can not update Product category', 'error');
                }
            })
        }
    })
    $('#btnDelete').off('click').on('click', function (e) {
        e.preventDefault();
        var id = $('#hidIdM').val();
        tedu.confirm('Do you want delete product category ?', function () {
            $.ajax({
                type: 'POST',
                data: {
                    Id: id
                },
                url: '/admin/ProductCategory/Delete',
                beforeSend: tedu.startLoading(),
                success: function (reponse) {
                    tedu.notify('Delete success', 'success');
                    window.location.href = '/admin/ProductCategory/index';
                    tedu.stopLoading();
                },
                error: function (status) {
                    tedu.notify('Can not delete product category !', 'error');
                }
            })
        })
    })
}

function loadProductCategory() {

    $.ajax({
        type: 'GET',
        data: {

        },
        url: '/admin/ProductCategory/GetAllCategory',
        //dataType: 'JSON',
        success: function (reponse) {
            var arr = [];
            $.each(reponse, function (i, item) {
                var obj = {
                    id: item.id,
                    text: item.name,
                    parentId: item.perentId,
                    sortOrder: item.sortOrder
                }
                arr.push(obj);
            })

            var rs = tedu.unflattern(arr);

            $('#treeCategory').tree({
                data: rs,
                dnd: true,
                onDrop: function (target, source, point) {
                    var targetNode = $(this).tree('getNode', target);
                    
                    if (point === 'append') {
                        console.log(source.id);
                        console.log(targetNode.id);
                        $.ajax({
                            type: 'POST',
                            data: {
                                sourceId: source.id,
                                targetId: targetNode.id,
                                items: null
                            },
                            url: '/admin/ProductCategory/UpdateParentId',
                            success: function (reponse) {
                                loadProductCategory();
                            },
                            error: function (status) {
                                tedu.notify('Update parentId not success', 'error');
                            }
                        })

                    }
                    else if (point === 'bottom' || point === 'top') {
                        $.ajax({
                            type: 'POST',
                            data: {
                                sourceId: source.id,
                                targetNodeId: targetNode.id,// vị trị mới của item
                            },
                            url: '/admin/ProductCategory/ReOrder',
                            success: function (reponse) {
                                loadProductCategory();
                            },
                            error: function (status) {
                                tedu.notify('can not update position categort ', 'error');
                            }
                        })
                    }
                },
                onContextMenu: function (e, node) {
                    e.preventDefault();
                    // select the node
                    $('#tt').tree('select', node.target);
                    $('#hidIdM').val(node.id);
                    // display context menu
                    $('#contextMenu').menu('show', {
                        left: e.pageX,
                        top: e.pageY
                    });
                }
            });
            
        },
        error: function (status) {
            tedu.notify('Can not loading product catagory', 'error');
        }
    })
}

function initialProductCategort(categoryId) {
    $.ajax({
        type: "GET",
        data: {

        },
        dataType: 'JSON',
        url: '/admin/ProductCategory/GetAllCategory',
        success: function (reponse) {
            var arr = [];
            $.each(reponse, function (i, item) {
                var obj =
                {
                    id: item.id,
                    text: item.name,
                    parentId: item.perentId,
                    sortOrder: item.sortOrder
                };
                arr.push(obj);
            });
            var res = tedu.unflattern(arr);
            $('#txtParentM').combotree({
                data: res
            });
            if (categoryId != undefined) {
                $('#txtParentM').combotree('setValue', categoryId);
            }
        },
        error: function (status) {
            tedu.error('can not loading product category', 'error');
        }
    })
}

