const slider = document.getElementById('error-range');
const field = document.getElementById('error-field');

slider.value = Math.min(field.value, 10);

slider.addEventListener("input", function () {
    console.log(field.value);
    field.value = this.value;
});

field.addEventListener("input", function () {
    console.log(field.value);
    slider.value = Math.min(field.value, 10);
});