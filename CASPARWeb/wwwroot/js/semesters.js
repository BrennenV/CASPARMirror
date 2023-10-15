var dataTable;
$(document).ready(function () {
    loadList();
});
function loadList() {
    dataTable = $('#DT_Semesters').DataTable({
        "ajax": {
            "url": "/api/semester",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //should not be capital
            { "data": "semesterName", "width": "70%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                    <a href="/Administrator/Templates/Index?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width: 100px;">
                        <i class="far fa-edit"></i> Edit </a>
                    <a href="/Administrator/Semesters/Delete?id=${data}" class="btn btn-danger text-white" style="cursor:pointer; width: 100px;">
                        <i class="far fa-trash-alt"></i> Delete </a>
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