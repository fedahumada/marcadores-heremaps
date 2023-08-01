const username = localStorage.getItem("username");
const password = localStorage.getItem("password");
console.log(username);
console.log(password);

// Api hosteada en azure web-app services
//const API_GET_MARCADORES_AZURE = "https://fedmaps.azurewebsites.net/api/GetMarcadores/credenciales/" + username + "-" + password

const API_GET_MARCADORES_URL = 'https://localhost:7039/api/GetMarcadores/credenciales/' + username + "-" + password

function listarMarcadores() {
    fetch(API_GET_MARCADORES_URL)
        .then((respuesta) => respuesta.json())
        .then((respuesta) => {
            if (!respuesta.ok) {
                alert("Error de conexion a API")
                return
            }

            const cuerpoTabla = document.querySelector("tbody")
            let id = 1;
            respuesta.litadoMarcadores.forEach((per) => {
                const fila = document.createElement('tr')
                fila.innerHTML += `<td>${id}</td>`
                fila.innerHTML += `<td>${per.latitud}</td>`
                fila.innerHTML += `<td>${per.longitud}</td>`
                fila.innerHTML += `<td>${per.info}</td>`
                fila.innerHTML += `<button type="submit" onclick="mapa(${per.latitud},${per.longitud},${id})" class="btn btn-primary">GO MAP</button>`
                cuerpoTabla.append(fila)
                id++
            });
        }).catch((err) => {
            alert("Algo salio mal!!")
        })
}

function mapa(latitud, longitud, id) {
    localStorage.setItem("id", id)
    localStorage.setItem("latitud" + id, parseFloat(latitud))
    localStorage.setItem("longitud" + id, parseFloat(longitud))
    window.location.replace("mapa.html")
}