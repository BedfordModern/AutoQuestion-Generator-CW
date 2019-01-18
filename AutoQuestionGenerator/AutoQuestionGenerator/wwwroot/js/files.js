"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/fileHub").build();

connection.on("ReceiveMessage", function (id, message, error) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var err = error.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var ele = document.getElementById("file-" + id);
    var container = document.getElementById("container-" + id);
    if (msg == "success") {
        ele.innerHTML = "&check;";
        ele.style.color = "green";
        container.innerHTML = container.innerHTML + "<br />" + "Name: "
        child = document.createElement("input");
        child.type = "text";
        child.className = "form-control";
        container.addChildNode(child);
    } else if(msg == "fail") {
        ele.innerHTML = "&times;";
        ele.style.color = "red";
        container.title = err;
    }
});

connection.start();

setTimeout(function () {
    var url = new URL(connection.connection.transport.webSocket.url);
    var UserID = url.searchParams.get("id");
    var guid = document.getElementById("guid").value;
    connection.invoke("GetUserID", UserID, guid).catch(function (err) {
        return console.error(err.toString());
    });
}, 1000);