// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var form = document.getElementById("login_form");
form.addEventListener("submit", function () {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "your-url-here", true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            console.log(xhr.responseText);
            //Sayfa yönlendirme vs işlemleri burada yapılır 
        }
    };
    xhr.send(); 
});