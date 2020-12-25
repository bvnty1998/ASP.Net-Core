var roductCategory = function () {
    this.initialize = function () {
        registerEvents();
    }
    
}
function registerEvents() {
    var render = $('#ddlProductCategory').html();
    console.log(render);
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
            tedu.notify('Can not loading Product Category','error')
        }
    })
}