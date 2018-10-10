var Request = require('request');

Request.get("http://localhost:58095/api/DetalleVenta?fecha=2018-10-09 13:40:20.473", (error, Response, body) => {
    if(error){
        return console.dir(error);
    }
    console.dir(JSON.parse(body));
});

Request.get("http://localhost:58095/api/Prdocuto?id=1", (error, Response, body) => {
    if(error){
        return console.dir(error);
    }
    console.dir(JSON.parse(body));
});

Request.post({
    "headers": { "content-type": "application/json" },
    "url": "http://localhost:58095/api/DetalleVenta",
    "body": JSON.stringify(	
    [{
        "IdProducto": 2,
        "Cantidad": 3
    }])
}, (error, response, body) => {
    if(error){
        return console.dir(error);
    }
    //console.dir(JSON.parse(body));
});

Request.put({
    "headers": { "content-type": "application/json" },
    "url": "http://localhost:58095/api/DetalleVenta?idDetVen=2&cantidad=2",
}, (error, response, body) => {
    if(error){
        return console.dir(error);
    }
    //console.dir(JSON.parse(body));
});

Request.delete({
    "headers": { "content-type": "application/json" },
    "url": "http://localhost:58095/api/DetalleVenta?id=10",
}, (error, response, body) => {
    if(error){
        return console.dir(error);
    }
    //console.dir(JSON.parse(body));
});