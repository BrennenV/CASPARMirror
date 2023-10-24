var dataTable;
$(document).ready(function () {
    loadList();
});
function loadList() {
    dataTable = $('#DT_Buildings').DataTable({
        "ajax": {
            "url": "/api/buildingList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //should not be capital
            { "data": "buildingName", "width": "37.5%" },
            { "data": "campus.campusName", "width": "37.5%" },
            {
                "data": "id", "render": function (data) {
                    return `<div class="text-center">
                    <a href="/Administrator/Buildings/CreateBuilding?id=${data}" type="button" class="btn btn-outline-primary mt-1 rounded" style="cursor:pointer; width: 100px;">
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
