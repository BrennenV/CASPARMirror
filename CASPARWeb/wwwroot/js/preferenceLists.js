var dataTable;
$(document).ready(function () {
    loadList();
});
function loadList() {
    dataTable = $('#DT_PreferenceLists').DataTable({
        "ajax": {
            "url": "/api/preferenceList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //should not be capital
            { "data": "semesterInstance.semesterInstanceName", "width": "70%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                    <a href="/Instructor/PreferenceLists/Upsert?id=${data}" class="btn btn-outline-primary mt-1 rounded" style="cursor:pointer; style="cursor:pointer; width: 100px;">
                        <i class="bi bi-pencil-square"></i> Edit </a>
                    <a href="/Instructor/PreferenceLists/Delete?id=${data}" class="btn btn-outline-danger mt-1 rounded" style="cursor:pointer; style="cursor:pointer; width: 100px;">
                        <i class="bi bi-trash"></i> Delete </a>
                    <a href="/Instructor/PreferenceListDetails/Index?id=${data}" class="btn btn-outline-info mt-1 rounded" style="cursor:pointer; style="cursor:pointer; width: 100px;">
                        <i class="far fa-trash-alt"></i> Details </a>
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