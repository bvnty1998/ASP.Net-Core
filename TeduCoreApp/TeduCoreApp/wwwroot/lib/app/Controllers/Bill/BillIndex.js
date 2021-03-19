var BillController = function () {
    // new obj 
    var cacheObj = {
        paymentmenthods:[],
        products:[],
        colors: [],
        size:[]
    }
    // initialize
    this.initialize = function () {
        getDataPaymentMenthod();
        getDataProduct();
        getDataSize();
        getDataColor();
        loadData();
        registerEvent();
        

    }
    var registerEvent = function () {
        
        // new modal add bill and initialize payment menthod and bill status
        $('#btn-CreateBill').on('click', function () {
            getPaymentMethod();
            getBillStatus();
            $('#billAddEditModal').modal('show');
        })
        // add bill detail
        $('#btnAddDetail').on('click', function () {
            addBillDetail(null, null, null, 1, null);
            getPrice();
           
        })
        //save bill detail
        $('#btn-save').on('click', function () {
            saveBill();
        })
        // view bill detail
        $('body').on('click', '.btn_ViewBill', function () {
           
            var ID = $(this).prop('id')
            $.ajax({
                type: 'GET',
                data: {
                    id :ID
                },
                dataType: 'JSON',
                url: '/admin/bill/FindBillById',
                success: function (response) {
                   
                    $('#tbl-Detail-details').html('');
                    $('#txtCustomerName').val(response.customerName);
                    $('#txtAddress').val(response.customerAddress);
                    $('#txtPhone').val(response.customerMobile);
                    $('#txtMessage').val(response.customerMessage);
                    getPaymentMethod();
                    getBillStatus();
                    $.each(response.billDetails, function (i, item) {
                        addBillDetail(item.productId, item.sizeId, item.colorId, item.quantity,item.price)
                    })
                    $('#billAddEditModal').modal('show');
                    console.log(response)
                },
                error: function (status) {
                    tedu.notify('Error while load bill detail !!', 'error');
                }
            })
            getPrice();
        })
       
    }
    //get data payment menthod
    function getDataPaymentMenthod() {
        $.ajax({
            type: 'GET',
            url: '/admin/bill/GetPaymentMenthod',
            success: function (response) {
                cacheObj.paymentmenthods = response;
            },
            error: function (status) {
                tedu.notify('Error while loading data payment Menthod !', 'error')
            }
        })
    }

    // get payment method 
    var getPaymentMethod = function ()
    {
        var html = ''
        $.each(cacheObj.paymentmenthods, function (i, item) {
            html += "<option value=" + item.value + ">" + item.name + "</option>"
        })
        $('#paymentMenthod').html(html)
       
    }
    // get bill status
    var getBillStatus = function () {
        $.ajax({
            type: 'GET',
            url: '/admin/bill/GetBillStatus',
            success: function (response) {
                var html = '';
                $.each(response, function (i, item) {
                    html += "<option value=" + item.value + ">" + item.name + "</option>"
                })
                $('#billStatus').html(html);

            },
            error: function (status) {
                tedu.notify('error while loading bill status !', 'error');
            }
        })
    }
    // bill detail
    function addBillDetail(productId, sizeId, colorId, quantity, price) {
       
        var template = $('#template-table-bill-details').html();
        var products = getProductOptions(productId);
        var sizes = getSizeOptions(sizeId);
        var color = getColorOptions(colorId);
        var quantity = quantity;
        var price = price;
        var render = "";
        render = Mustache.render(template, {
            Products: products,
            Colors: color ,
            Sizes: sizes,
            Quantity: quantity,
            Price: price
        })
        $('#tbl-Detail-details').append(render);
    }

    // Get data product 
    function getDataProduct() {
        $.ajax({
            type: 'GET',
            url: '/admin/product/GetAll',
            success: function (response) {
                cacheObj.products = response;
            },
            erorr: function (status) {
                tedu.notify('error while loading product !','error')
            }
        })
    }
    // Get data Size
    function getDataSize() {
        $.ajax({
            type: 'GET',
            url: '/admin/size/getall',
            success: function (response) {
                cacheObj.size = response
            },
            erorr: function (status) {
                tedu.notify('error while loading size','error')
            }
        })
    }
    //get data color
    function getDataColor() {
        $.ajax({
            type: 'GET',
            url: '/admin/color/getall',
            success: function (response) {
                cacheObj.colors = response
            },
            error: function (status) {
                tedu.notify('error while loading color !', 'error');
            }
        })
    }


    // get product 
    function getProductOptions(selectedId) {
        var html = ' <select class="' + 'productId "' +'data-live-search="' +'true"'+'>';
        $.each(cacheObj.products, function (i, item) {
            if (item.id == selectedId) {
                html += '<option value="' + item.id + '"'+ 'selected="select">' + item.name + "</option>"
            }
            else {
                html += "<option value=" + item.id + ">" + item.name + "</option>"
            }
        })
        html += '</select>'
        return html;
    }

    // get size
    function getSizeOptions(selectedId) {
        var html = '<select class=' + 'sizeId' +'>';
        $.each(cacheObj.size, function (i, item) {
            if (item.id == selectedId) {
                html += '<option value="' + item.id + '"'+ 'selected="select">' + item.name + '</option>'
            }
            else {
                html += "<option value=" + item.id + ">" + item.name + "</option>"
            }
            
        })
        html += '</select>'
        return html;
    }

    // get color
    function getColorOptions(selectId) {
        var html = '<select class=' + 'colorId' + '>';
        $.each(cacheObj.colors, function (i, item) {
            if (item.id == selectId) {
                html += '<option value="' + item.id + '"'+'selected="select">' + item.name + "</option>"
            }
            else {
                html += "<option value=" + item.id + ">" + item.name + "</option>"
            }
            
        })
        html += '</select>'
        return html;
    }

    // btn save
    function saveBill() {
        var customerName = $('#txtCustomerName').val();
        var customerAddress = $('#txtAddress').val();
        var customerMobile = $('#txtPhone').val();
        var customerMessage = $('#txtMessage').val();
        var paymentMethod = $('#paymentMenthod').val();
        var billStatus = $('#billStatus').val();
        var arrBillDetail = [];
        $.each($('#tbl-Detail-details tr'), function (i, item) {
            var billDetail = {
                ProductId: $(item).find('select.productId').val(),
                ColorId:$(item).find('select.colorId').val(),
                SizeId: $(item).find('select.sizeId').val(),
                Quantity: $(item).find('input.txtQuantity').val(),
                Price: $(item).find('label.lblPrice').text()
            }
            arrBillDetail.push(billDetail)
        });
        $.ajax({
            type: 'POST',
            data: {
                CustomerName: customerName,
                CustomerAddress: customerAddress,
                CustomerMobile: customerMobile,
                CustomerMessage: customerMessage,
                PaymentMethod: paymentMethod,
                BillStatus: billStatus,
                BillDetailViewModel:arrBillDetail
            },
            url: '/admin/Bill/SaveBill',
            success: function (response) {
                window.location.href='/admin/bill/index'
            },
            error: function (status) {

            }
        })
        //console.log(arrBillDetail);  
    }

    // get bill deltail
    function getPrice() {
        //var text = $(this)
        $('body').on('change','.productId', function () {
            var currentRow = $(this).closest('tr');
            var id = $(currentRow).find('select.productId').val();
            $.ajax({
                type: 'GET',
                data: {
                    id: id
                },
                url: '/admin/product/GetById',
                success: function (response) {
                    $(currentRow).find('label.lblPrice').text(response.price)
                },
                error: function (status) {
                    tedu.notify('load price of product fail !', 'error');
                }
            })
            
            
        })
    }

    function getPaymentNameById(paymentID) {
        var name = '';
        var data = cacheObj.paymentmenthods.find(x => x.value == paymentID);
        name = data.name;
        
        return name;
    }

    // load data
    function loadData() {
        var tempalate = $('#template-table-bill').html();
        var render = '';
        $.ajax({
            type: 'GET',
            dataType:'JSON',
            data: {
                keyWord: '',
                fromDate: '',
                toDate: '',
                page: 1,
                pageSize: tedu.cofigs.pageSize,
            },
            url: '/admin/Bill/GetAllPaging',
            success: function (respense) {
                $.each(respense.result, function (i, item) {
                    //console.log(getPaymentNameById(item.paymentMethod))
                    render += Mustache.render(tempalate, {
                        id:item.id,
                        customerName : item.customerName,
                        paymentMenthod: getPaymentNameById(item.paymentMethod),
                        dateCreate: item.dateCreated,
                        status : item.status

                    })
                })
                $('#tbl-bill').html(render);
                //console.log(respense)
            },
            error: function (status) {
                tedu.notify('error while load data !', 'error');
            }
        })
    }
    
    
}