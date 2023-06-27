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

connection.on("AjaxPriceReceive", function (id) {
    $("#uxAjaxValue").text(id);
});
