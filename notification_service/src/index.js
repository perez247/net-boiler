import http from 'http';
import express from 'express';
import cors from 'cors';
import morgan from 'morgan';
import bodyParser from 'body-parser';
import config from './config.json';
import dotenv from 'dotenv';
import socketLib from 'socket.io';

dotenv.config();
let app = express();

app.server = http.createServer(app);

let io = socketLib(app.server, {
    cors: {
        origin: "*",
        methods: ["GET", "POST"],
        // allowedHeaders: ["my-custom-header"],
        // credentials: true
      }
});

// logger
app.use(morgan('dev'));

// 3rd party middleware
// app.use(cors({
// 	exposedHeaders: config.corsHeaders
// }));

app.use(function(req, res, next) {
    res.header("Access-Control-Allow-Origin", "*");
    res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
    next();
});

app.use(bodyParser.json({
	limit : config.bodyLimit
}));

app.get('/', function(req, res) {
    res.sendFile(__dirname+'/../test/socker-listen.html');
 });

app.post('/notify', (req, res) => {

    // Validate it is from the server by checking the header for hashed value

    io.emit('notify', req.body);
});


io.on('connection', () => {
    io.emit("notify", "connected")
});

app.server.listen(process.env.PORT || config.port, () => {
    console.log(`Notification Service started on port ${app.server.address().port}`);
});

export default app;
