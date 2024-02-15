// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let checkboxPassword = document.getElementById("passwordCheckBox");
let inputPassword = document.getElementById("passwordInput");

function openPassword() {
    if (checkboxPassword.checked) {
        inputPassword.type = "text";
    }
    else {
        inputPassword.type = "password";
    }
}