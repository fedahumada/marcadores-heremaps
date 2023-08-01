function signIn() {
    let txtUsername = document.getElementById("txtUsername")
    let txtPassword = document.getElementById("txtPassword")

    if (txtUsername.value === "") {
        alert("Username can't be empty")
        return false
    }
    if (txtPassword.value === "") {
        alert("Password can't be empty")
        return false
    }
    localStorage.setItem("username", txtUsername.value)
    localStorage.setItem("password", txtPassword.value)
    alert("Esta siendo redirigido. Presione Aceptar")
    window.location.replace("listadoMarcadores.html")
}