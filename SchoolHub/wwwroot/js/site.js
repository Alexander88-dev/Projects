// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//Boolean b = true;

function togglePassword(inputId, button)
{
    const input = document.getElementById("PasswordVisibilityI");
    const label = document.getElementById("labelId");
    
    if (input.type === "password")
    {
        input.type = "text";
        //label.innerText = 'новый текст'
    }
    else
    {
        input.type = "password";
        //label.innerText = 'новый текст'
    } 
}