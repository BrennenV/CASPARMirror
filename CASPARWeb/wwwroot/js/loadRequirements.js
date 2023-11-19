var dataTable;
$(document).ready(function () {
    loadList();
});
function loadList() {
    dataTable = $('#DT_LoadRequirements').DataTable({
        "ajax": {
            "url": "/api/loadRequirements",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //should not be capital
            { "data": "semesterInstanceName", "width": "70%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                    <a href="/Admin/LoadRequirements/Upsert?id=${data}" class="btn btn-outline-primary mt-1 rounded" style="cursor:pointer; style="cursor:pointer; width: 100px;">
                        <i class="bi bi-pencil-square"></i> Edit </a>
                    </div>`;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No data found."
        },
        "width": "100%"
    });
}