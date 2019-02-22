"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/mesHub").build();

//Disable send button until connection is established
$("#sendButton").disabled = true;

connection.on("ReceiveMessage", function (id, table,action) {
	var msg = table.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
	var encodedMsg = id + " says " + table+" "+action;
	var li = document.createElement("li");
	li.textContent = encodedMsg;
	$("#messagesList").append(li);
});

connection.start().then(function(){
	$("#sendButton").disabled = false;
}).catch(function (err) {
	return console.error(err.toString());
});

//document.getElementById("sendButton").addEventListener("click", function (event) {
//	var id = document.getElementById("userInput").value;
//    var table = document.getElementById("messageInput").value;
//    var action = document.getElementById("messageInpu1").value;
//	connection.invoke("SendMessage", id, table,action).catch(function (err) {
//		return console.error(err.toString());
//	});
//	event.preventDefault();
//});
$('#sendButton').on('click', function (event) {
    var id = $('#id').val();
    var table = $('#table').val();
    var action = $('#action').val();
    connection.invoke("SendMessage", id, table, action).catch(function (err) {
	    return console.error(err.toString());
    });
    event.preventDefault();
});