﻿var loginController = function()
{
    this.initialize = function()
    {
        registerEvents();
     }
    var registerEvents = function()
      {
        $('#btnLogin').click(function () {
            var user = $('#txtUserName').val();
            var pass = $('#txtPassword').val();
           login(user, pass);
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
                        if(res.Success = true)
                        {
                            window.location.href= '/admin/home';
                        }
                        else
                        {
                            tedu.notify('login feild','error')
                        }
                    }
                  })
    }

}