let moreDataCount = 0;

const errorsValue = document.getElementById('errors-value');
const errorsSeed = document.getElementById('errors-seed');
const generationSeed = document.getElementById('generation-seed');
const region = document.getElementById('region');

const slider = document.getElementById('errors-slider');
const field = document.getElementById('errors-value');
const generationField = document.getElementById('generation-seed');
const errorsField = document.getElementById('errors-seed');
const regionRange = document.getElementById('region');

//const getDataUrl = 'https://localhost:7118/api/getdata';
//const exportUrl = 'https://localhost:7118/api/export'
const exportUrl = document.getElementById('export-url').value;
const getDataUrl = document.getElementById('get-data-url').value;

window.addEventListener('wheel', async function () {
    if (this.window.scrollY + this.window.innerHeight >= this.document.documentElement.scrollHeight) {

        moreDataCount += 10;

        reloadTableData();
    }
});

document.addEventListener('DOMContentLoaded', function () {
    reloadTableData();
}, false);

slider.value = 0;
field.value = slider.value;

slider.addEventListener("input", async function () {

    if (slider.value == '') {
        return;
    }
    if (field.value > 1000) {
        field.value = 1000;
    }
    field.value = this.value;

    reloadTableData();
});

// Обработчик события изменения значения текстового поля
field.addEventListener("input", async function () {

    if (field.value == '') {
        return;
    }
    if (field.value > 1000) {
        field.value = 1000;
    }
    slider.value = Math.min(field.value, 10);

    reloadTableData();
});

generationField.addEventListener("input", async function () {

    moreDataCount = 0;

    if (generationField.value == '') {
        return;
    }
    reloadTableData();
});

errorsField.addEventListener("input", async function () {

    if (errorsField.value == '') {
        return;
    }

    reloadTableData();
});

regionRange.addEventListener('change', function () {
    moreDataCount = 0;

    if (generationField.value == '') {
        return;
    }
    reloadTableData();
});

async function loadMore() {
    moreDataCount += 10;

    reloadTableData();
}

async function randomGenerate() {
    errorsValue.value = Math.ceil(Math.random() * (0, 1000));
    errorsSeed.value = Math.ceil(Math.random() * (0, 2147483647));
    generationSeed.value = Math.ceil(Math.random() * (0, 2147483647));

    const moreDataCount = 0;

    slider.value = Math.min(field.value, 10);

    reloadTableData();
}

async function reloadTableData() {
    let dataArray;

    try {
        dataArray = await postData(getDataUrl, {
            Region: region.value,
            ErrorsValue: Number.parseFloat(errorsValue.value),
            ErrorsSeed: Number.parseInt(errorsSeed.value),
            GenerationSeed: Number.parseInt(generationSeed.value),
            More: moreDataCount
        });

    } catch (error) {
        console.error('Ошибка при отправке данных:', error);
        // Добавьте обработку ошибки, например, отображение сообщения об ошибке пользователю
    }

    let dataList = document.getElementById('table-body');
    dataList.innerHTML = ""; // Удаление содержимого таблицы

    for (let i = 0; i < dataArray.length; i++) {
        const tr = document.createElement('tr');

        for (let j = 0; j < dataArray[i].length; j++) {
            const td = document.createElement('td');

            if (i % 2 == 1) {
                td.style.backgroundColor = "#ECE8E5";
            }
            else {
                td.style.backgroundColor = "#D6D6CE";
            }

            td.textContent = dataArray[i][j];

            tr.appendChild(td);
        }

        dataList.appendChild(tr);
    }
}


const postData = async (url = '', data = {}) => {
    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)

    });

    //console.log(JSON.stringify(data));

    return response.json();
};

async function exportData() {
    const payload = {
        Region: region.value,
        ErrorsValue: Number.parseFloat(errorsValue.value),
        ErrorsSeed: Number.parseInt(errorsSeed.value),
        GenerationSeed: Number.parseInt(generationSeed.value),
        More: moreDataCount
    };

    try {
        const response = await fetch(exportUrl, {
            method: 'POST',
            body: JSON.stringify(payload),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            const fileBlob = await response.blob();
            const fileName = 'exported_file.csv'; // Замените на имя файла, которое вы хотите сохранить

            // Создайте ссылку для скачивания файла
            const downloadLink = document.createElement('a');
            downloadLink.href = URL.createObjectURL(fileBlob);
            downloadLink.download = fileName;
            downloadLink.click();
        } else {
            console.error('Ошибка при скачивании файла:', response.status);
        }
    } catch (error) {
        console.error('Произошла ошибка:', error);
    }
}
