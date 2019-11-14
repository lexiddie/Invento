// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const test = "sdfsdf";

function myAccFunc() {
  console.log("testing");
  const x = document.getElementById("demoAcc");
  if (x.className.indexOf("dropdown") == -1) {
    x.className += " dropdown";
    x.previousElementSibling.className += " w3-green";
  } else {
    x.className = x.className.replace(" dropdown", "");
    x.previousElementSibling.className = x.previousElementSibling.className.replace(
      " w3-green",
      ""
    );
  }
}
