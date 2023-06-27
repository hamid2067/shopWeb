var connection = new signalR.HubConnectionBuilder().withUrl("/OnlineHub").build();
async function start() {

    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);

    }
};

connection.onclose(async () => {
    await start();
});
start();

connection.on("WebLogin", function (id) {
    if (id == $("#uxValQrCode").val()) {
        $.blockUI({
            css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#000',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                opacity: .5,
                color: '#fff'
            }
        });
        $.post("/Account/TokenVerification/", { token: id })
            .done(function (data) {
                if (data.success) {
                    toastr.success(data.msg, '', {
                        "closeButton": true,
                        "progressBar": true,
                        "positionClass": "toast-bottom-full-width",
                    });
                    window.location.reload();
                }
                else {
                    $.unblockUI();
                    toastr.warning(data.msg, '', {
                        "closeButton": true,
                        "progressBar": true,
                        "positionClass": "toast-bottom-full-width",
                    });
                }
            });
    }
});
