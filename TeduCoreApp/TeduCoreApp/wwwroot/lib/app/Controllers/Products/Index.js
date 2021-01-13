var productController = function () {
    this.initialize = function () {
        loadData();
        loadCategory();
        registerEvents();
        registerController();
       
       
    }
    function registerEvents() {
        //quantity product show on page
        $('#ddlShowPage').on('change', function () {
            tedu.cofigs.pageSize = $(this).val();
            tedu.cofigs.pageIndex = 1;
            loadData(true);
        });
        //search product
        $('#btnSearch').on('click', function () {
            console.log($('#ddlProductCategory').val());
            loadData();
        });
        //show form create product
        $('#btnCreateProduct').off('click').on('click', function () {
            $('#modalLabel').text('Add Product');
            Resetform();
            console.log($('#ckbActiveM').val())
                
            initializerProductCategory('');
            $('#AddEditModal').modal('show');
            
        })
        //Load product to form edit 
        $('body').on('click', '.btn-edit', function () {
            $('#modalLabel').text('Edit Product');
            //initializerProductCategory('');
            //console.log($(this).data());
            //alert($(this).attr('id'));
            //$('#AddEditModal').modal('show');
            var id = $(this).attr('id');
            $.ajax({
                type: 'GET',
                dataType: 'JSON',
                data: {
                    Id:id
                },
                url:'/Admin/Product/GetById',
                success: function (response) {
                    console.log(response);
                    $('#hidIdM').val(response.id);
                    $('#txtNameM').val(response.name);
                    initializerProductCategory(response.categoryId);
                    $('#txtDescriptionM').val(response.description);
                    $('#txtUnitM').val(response.unit);
                    $('#txtSellPriceM').val(response.price);
                    $('#txtOriginalPriceM').val(response.originalPrice);
                    $('#txtPromotionM').val(response.promotionPrice);
                    CKEDITOR.instances.txtContentM.setData(response.content);
                    $('#txtSeoTitleM').val(response.seoPageTitle);
                    $('#txtSeoAliasM').val(response.seoAlias);
                    $('#txtSeoKeywordM').val(response.seoKeywords);
                    $('#txtSeoDescriptionM').val(response.seodescription);
                    $('#txtTagM').val(response.tag);
                    $('#txtImageM').val(response.image);
                    $('#ckbActiveM').prop('checked', response.status == 1);
                    $('#ckbShowOnHomeM').prop('checked', response.homeFlag == 1);
                    $('#AddEditModal').modal('show');
                },
                error: function (status) {

                }
            })
        })
        //validation form 
        $('#fmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules:
            {
                txtNameM:
                {
                    required:true,
                },
                txtCategoryM:
                {
                    required: true
                },
                txtUnitM:
                {
                    required: true,
                    
                },
                txtSeoAliasM:
                {
                    required: true
                },
                txtTagM:
                {
                    required: true
                }

            },
            messages:
            {
                txtNameM:
                {
                    required: "Please enter product name !",
                },
                txtCategoryM:
                {
                    required: "Please choose product category !"
                },
                txtUnitM:
                {
                    required: "Please enter product unit ! ",
                },
                txtSeoAliasM:
                {
                    required: "Please enter SeoAlias ! "
                },
                txtTagM:
                {
                    required: "Please enter Tag ! "
                }
            }
        })
        // saved product before update or add product
        $('#btnSaved').off('click').on('click', function () {
            if ($('#fmMaintainance').valid()) {
                $.ajax({
                    type: 'POST',
                    dataType: 'JSON',
                    url: '/Admin/Product/Saved',
                    data: {
                        Id: $('#hidIdM').val(),
                        Name: $('#txtNameM').val(),
                        CategoryId: $('#txtCategoryM').val(),
                        Image: $('#txtImageM').val(),
                        Price: $('#txtSellPriceM').val(),
                        PromotionPrice: $('#txtPromotionM').val(),
                        OriginalPrice: $('#txtOriginalPriceM').val(),
                        Content: CKEDITOR.instances.txtContentM.getData(),
                        HomeFlag: $('#ckbShowOnHomeM').val(),
                        Tag: $('#txtTagM').val(),
                        Unit: $('#txtUnitM').val(),
                        Status: $('#ckbActiveM').val(),
                        SeoAlias: $('#txtSeoAliasM').val(),
                        SeoKeywords: $('#txtSeoKeywordM').val(),
                        Description: $('#txtDescriptionM').val(),
                        Status: $('#ckbActiveM').prop('checked') == true? 1:0
                    },
                   
                    success: function (response) {
                        window.location.href='/admin/product/index';

                    },
                    error: function (status) {
                        console.log(status);
                        tedu.notify('can not add or update product', 'error');
                    }
                })
            }
           
        })
        // Delete product category
        $('body').on('click', '.btn-delete', function () {
            var id = $(this).attr('id');
            tedu.confirm("Do you want delete product", function () {
                $.ajax({
                    type: 'POST',
                    data: {
                        Id: id
                    },
                    url: '/admin/product/Delete',
                    success: function (response) {
                        tedu.notify('delete product success', 'success');
                        window.location.href = '/admin/product/index';
                    },
                    error: function (status) {
                        tedu.notify('error while delete product', 'error');
                    }
                })
            })
        })
        // Change status Product
        $('body').on('click', '.btn-changeStatus', function () {
            var id = $(this).attr('id');
            alert(id);
        })
        $('#btnSelectImg').on('click', function () {
            $('#fileInputImage').click();
        })
        $('#fileInputImage').on('change', function () {
            var fileUpload = $(this).get(0);
            console.log($(this).get(0));
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }
            $.ajax({
                type: "POST",
                url: "/Admin/Upload/UploadImage",
                contentType: false,
                processData: false,
                data: data,
                success: function (path) {
                    $('#txtImageM').val(path);
                    tedu.notify('up load successful !', 'success');
                },
                error: function () {
                    tedu.notify('there was error uploading files ','error')
                }
            })
        })
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
                        Images: item.image == null ? '<img src="/images/user.png" width="30" height="30"/>' : '<img src="' + item.image + '" width="30" height="30"/>',
                        CreatedDate: tedu.dateTimeFormatJSon(item.dateCreated),
                        Price: tedu.formatNumber(item.price,0),
                        CategoryName: item.productCategory.name,
                        Status: tedu.getStatus(item.status, item.id),
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
    // Register ckeditor
    function registerController() {
        CKEDITOR.replace('txtContentM');
        //Fix: cannot click on element ck in modal
        $.fn.modal.Constructor.prototype.enforceFocus = function () {
            $(document)
                .off('focusin.bs.modal') // guard against infinite focus loop
                .on('focusin.bs.modal', $.proxy(function (e) {
                    if (
                        this.$element[0] !== e.target && !this.$element.has(e.target).length
                        // CKEditor compatibility fix start.
                        && !$(e.target).closest('.cke_dialog, .cke').length
                        // CKEditor compatibility fix end.
                    ) {
                        this.$element.trigger('focus');
                    }
                }, this));
        };
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

    function initializerProductCategory(categorySelected) {
        $.ajax({
            type: 'GET',
            data: {},
            dataType: 'JSON',
            url: '/admin/ProductCategory/GetAllCategory',
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
                var rs = tedu.unflattern(arr)
                $('#txtCategoryM').combotree({
                    data: rs,

                })
                if (categorySelected !== undefined) {
                    $('#txtCategoryM').combotree('setValue', categorySelected)
                    //$('#txtParentM').combotree('setValue', categoryId);
                }
            },
            error: function (status) {
                tedu.notify('Can loading product category !','error')
            }
        })
    }
    //Reset from 
    function Resetform() {

        $('#txtNameM').val('');
        $('#txtDescriptionM').val('');
        $('#txtUnitM').val('');
        $('#txtSellPriceM').val('');
        $('#txtOriginalPriceM').val('');
        $('#txtPromotionM').val('');
        $('#txtSeoTitleM').val('');
        $('#txtSeoAliasM').val('');
        $('#txtSeoKeywordM').val('');
        $('#txtSeoDescriptionM').val('');
        $('#txtTagM').val('');
        $('#txtImageM').val('');
    }
}
