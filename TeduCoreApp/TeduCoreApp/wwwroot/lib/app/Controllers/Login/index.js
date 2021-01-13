var loginController = function()
{
    this.initialize = function()
    {
        registerEvents();
     }
    var registerEvents = function()
    {
        $('#frmLogin').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                userName:
                {
                    required: true,

                },
                passWord:
                {
                    required: true,
                }
            },
            messages: {
                userName:
                {
                    required: "enter user name !"
                },
                passWord:
                {
                    required:"enter password !"
                }

            }
        })
        $('#btnLogin').click(function () {
            if ($('#frmLogin').valid())
            {
                var user = $('#txtUserName').val();
                var pass = $('#txtPassword').val();
                login(user, pass);
            }
        })
     }
    var login = function (user,pass)
        {
            $.ajax({
                    type:'POST',
                    data:{
                           UserName : user,
                           Password : pass
                        },
                    dataType: 'json',
                    url: '/admin/login/authen',
                success: function (res) {
                    console.log(res);
                    if (res.success == true)
                        {
                            window.location.href = '/admin/home';

                            
                        }
                        else
                        {
                            tedu.notify('login feild','error')
                        }
                    }
                  })
    }

}