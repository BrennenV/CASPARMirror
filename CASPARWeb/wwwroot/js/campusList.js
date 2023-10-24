var dataTable;
$(document).ready(function () {
    loadList();
});
function loadList() {
    dataTable = $('#DT_Campus').DataTable({
        "ajax": {
            "url": "/api/campusList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //should not be capital
            { "data": "campusName", "width": "75%" },
            {
                "data": "id", "render": function (data) {
                    return `<div class="text-center">
                    <a href="/Administrator/Campuses/CreateCampus?id=${data}" type="button" class="btn btn-outline-primary mt-1 rounded" style="cursor:pointer; width: 100px;">
                        <i class="bi bi-pencil-square"></i> Edit </a>`;
                }, "width": "25%"
            }
        ],
        "language": {
            "emptyTable": "No data found."
        },
        "width": "100%"
    });
}
