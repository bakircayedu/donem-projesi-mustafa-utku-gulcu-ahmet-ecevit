(function () {
'use strict'
const forms = document.querySelectorAll('.requires-validation')
Array.from(forms)
  .forEach(function (form) {
    form.addEventListener('submit', function (event) {
      if (!form.checkValidity()) {
        event.preventDefault()
        event.stopPropagation()
      }

      form.classList.add('was-validated')
    }, false)
  })
})()

var ekle=document.createElement("input");
            
ekle.setAttribute("type","text");
ekle.setAttribute("name","mesaj");
ekle.setAttribute("id","mesaj");

var btnEkle = document.getElementById("faturaeklebuton");
btnEkle.click= function(){
  btnEkle.appendChild(ekle);
}
