"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.

connection.on("ReceiveMessage", function (user, message) {
    const d = new Date();
    let dateString = d.toDateString();

    var table = document.getElementById('MessageTable').getElementsByTagName('tbody')[0];
    var row = table.insertRow();
    var CellUser = row.insertCell();
    var CellMessage = row.insertCell();
    var CellDateTime = row.insertCell();

    CellUser.innerHTML = user;
    CellMessage.innerHTML = message;
    CellDateTime.innerHTML = dateString;

    addData(dateString, message)
});


connection.on("ClientSubscribed", function (user, topic) {
    const d = new Date();
    let dateString = d.toDateString();

    var table = document.getElementById('ClientTable').getElementsByTagName('tbody')[0];
    var row = table.insertRow();
    var CellUser = row.insertCell();
    var CellTopic = row.insertCell();
    var CellDateTime = row.insertCell();

    CellUser.innerHTML = user;
    CellTopic.innerHTML = topic;
    CellDateTime.innerHTML = dateString;
});

connection.on("ClientConnected", function (userid, username,endpoint) {
    const d = new Date();
    let dateString = d.toDateString();

    var table = document.getElementById('ConnectionTable').getElementsByTagName('tbody')[0];
    var row = table.insertRow();
    var CellUser = row.insertCell();
    var CellUserName = row.insertCell();
    var CellEndpoint = row.insertCell();
    var CellDateTime = row.insertCell();

    CellUser.innerHTML = userid;
    CellUserName.innerHTML = username;
    CellEndpoint.innerHTML = endpoint;
    CellDateTime.innerHTML = dateString;
});


connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});


connection.on('SendMessageToClient', (title, user, message) => {
    const received = `title: ${title}, name: ${user}, message: ${message}`;
    console.log(received);
});

const ctx = document.getElementById('myChart').getContext('2d');
const myChart = new Chart(ctx, {
    type: 'line',
    data: {
        datasets: [{
            label: 'دما',
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)'
            ],
            borderColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
            ],
            borderWidth: 1
        }]
    },
    options: {
        tension: 0.3,
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});

function addData(label, data) {
    myChart.data.labels.push(label);
    myChart.data.datasets.forEach((dataset) => {
        dataset.data.push(data);
    });
    myChart.update();
}
