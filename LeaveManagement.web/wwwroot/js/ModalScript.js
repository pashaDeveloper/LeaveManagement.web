let modal = document.getElementById("form-modal");
let span = document.getElementsByClassName("modal-close")[0];
showInPopup = (url, title) => {
    $.ajax({
        type: "Get",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            modal.style.display = "block";
            $.validator.unobtrusive.parse("#form-modal");

        }

    })
}
span.onclick = function () {
    modal.style.display = "none";
}
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none"
    }
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.isSuccess == true) {
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    modal.style.display = "none";
                    notie.alert({ type: 1, text: data.message, time: 5 })
                    $('#view-all').html(data.html);

                }
                else {
                    $('#form-modal .modal-body').html(data.html);
                    notie.alert({ type: 3, text: data.message, time: 5 })
                   
                }
            },
            error: function (err) {
                console.log(err);
                notie.force({
                    type: 3,
                    text: 'احتمال خطای سرور ',
                    buttonText: 'تایید',
                    callback: function () {
                        notie.alert({ type: 3, text: 'مجدد تلاش کنید' })
                    }
                })
            }
        })
        return false;
    } catch (ex) {
        console.log(ex);
        notie.force({
            type: 3,
            text: ' خطای ناشناخته ای رخ داده است در صورت بروز مجدد با مدیریت تماس حاصل فرمائید ',
            buttonText: 'تایید',
            callback: function () {
                notie.alert({ type: 3, text: 'مجدد تلاش کنید' })
            }
        })
    }
}
jQueryAjaxDelete = form => {
    notie.confirm({
        text: `آیا از حذف ایتم اطمنیان دارید؟<br>${form.name}</b>`,

        submitCallback: function () {
            try {
                $.ajax({
                    type: 'POST',
                    url: form.action,
                    data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function (res) {
                        if (res.isSuccess == true) {

                            notie.alert({ type: 1, text: res.message, time: 5 })
                            $('#view-all').html(res.html);

                        }
                        else {
                            notie.alert({ type: 3, text: res.message, time: 5 })

                        }
                    },
                    error: function (err) {
                        console.log(err)
                        notie.force({
                            type: 3,
                            text: 'احتمال خطای سرور ',
                            buttonText: 'تایید',
                            callback: function () {
                                notie.alert({ type: 3, text: 'مجدد تلاش کنید' })
                            }
                        })
                    }
                })
            } catch (ex) {
                console.log(ex)
                notie.force({
                    type: 3,
                    text: ' خطای ناشناخته ای رخ داده است در صورت بروز مجدد با مدیریت تماس حاصل فرمائید ',
                    buttonText: 'تایید',
                    callback: function () {
                        notie.alert({ type: 3, text: 'مجدد تلاش کنید' })
                    }
                })
            }
        }
    })
   
    


    return false;
}
