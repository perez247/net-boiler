
// const io = require("socket.io-client");
const socket = io('http://localhost/');

socket.on("notify", function (data) {
    console.log(data);
});
  

