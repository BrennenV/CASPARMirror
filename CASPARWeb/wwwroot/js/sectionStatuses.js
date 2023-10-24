var dataTable;
$(document).ready(function () {
    loadList();
});
function loadList() {
    dataTable = $('#DT_SectionStatuses').DataTable({
        "ajax": {
            "url": "/api/sectionStatus",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //should not be capital
            { "data": "sectionStatusName", "width": "70%" },
            {
                "data": "sectionStatusColor",
                "render": function (data) {
                    return `<span style="background-color:${data};width:35px;height:18px;border-radius:10px;display:inline-block;"></span>`
                },
                "width": "5%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                    <a href="/Administrator/SectionStatuses/Upsert?id=${data}" class="btn btn-outline-primary mt-1 rounded" style="cursor:pointer; style="cursor:pointer; width: 100px;">
                        <i class="bi bi-pencil-square"></i> Edit </a>
                    <a href="/Administrator/SectionStatuses/Delete?id=${data}" class="btn btn-outline-danger mt-1 rounded" style="cursor:pointer; style="cursor:pointer; width: 100px;">
                        <i class="bi bi-trash"></i> Delete </a>
                    </div>`;
                }, "width": "30%"
            }
        ],
        "columnDefs": [
            {
                "targets": 1,
                "className": "text-center align-middle"
            }
        ],
        "language": {
            "emptyTable": "No data found."
        },
        "width": "100%"
    });
}