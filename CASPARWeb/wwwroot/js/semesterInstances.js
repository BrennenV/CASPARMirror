var dataTable;
$(document).ready(function () {
    loadList();
});
function loadList() {
    dataTable = $('#DT_SemesterInstances').DataTable({
        "ajax": {
            "url": "/api/semesterInstance",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //should not be capital
            { "data": "semesterInstanceName", "width": "25%" },
            {"data": "startDate", "render": function (data, type, row) {
                    if (type === 'display') {
                        // Format the date using JavaScript's Date object
                        const date = new Date(data);
                        return date.toLocaleDateString();
                    }
                    return data;
                }, "width": "25%" },
            {"data": "endDate", "render": function (data, type, row) {
                    if (type === 'display') {
                        // Format the date using JavaScript's Date object
                        const date = new Date(data);
                        return date.toLocaleDateString();
                    }
                    return data;
                }, "width": "25%" },
            {"data": "id", "render": function (data) {
                    return `<div class="text-center">
                    <a href="/Administrator/SemesterInstances/CreateSemesterInstance?id=${data}" type="button" class="btn btn-outline-primary mt-1 rounded" style="cursor:pointer; width: 100px;">
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
