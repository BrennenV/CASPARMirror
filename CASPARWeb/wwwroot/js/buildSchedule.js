var dataTable;
$(document).ready(function () {
    loadList();
});
function loadList() {
    dataTable = $('#DT_Semesters').DataTable({
        "ajax": {
            "url": "/api/semesterInstance",
            "type": "GET",
            "datatype": "json"
        },
        order: [[0, 'desc']],
        "columns": [
            //should not be capital
            { "data": "semesterInstanceName", "width": "50%" },
            {
                "data": null,
                "render": function (data, type, row) {
                    let now = new Date().getTime();
                    let semesterStart = new Date(row.startDate).getTime();
                    let semesterEnd = new Date(row.endDate).getTime();

                    if (now > semesterStart && now > semesterEnd) {
                        return `<span class="badge rounded-pill bg-secondary">Semester Completed</span>`;
                    }

                    if (now > semesterStart) {
                        return `<span class="badge rounded-pill bg-secondary">Semester In Progress</span>`;
                    }

                    return `<span class="badge rounded-pill bg-primary">Open for Scheduling</span>`;
                }, "width": "20%"
            },
            {
                "data": "id",
                "render": function (data, type, row) {
                    let now = new Date().getTime();
                    let semesterStart = new Date(row.startDate).getTime();

                    if (now > semesterStart) {
                        return `<div class="text-center">
                        <span class="btn btn-outline-secondary mt-1 rounded disabled" style="cursor:pointer; style="cursor:pointer; width: 100px;">
                            <i class="bi bi-pencil-square"></i> Build Schedule </span>
                        </div>`;
                    }

                    return `<div class="text-center">
                        <a href="/Coord/BuildSchedule/Sections?semesterInstanceId=${data}" class="btn btn-outline-primary mt-1 rounded" style="cursor:pointer; style="cursor:pointer; width: 100px;">
                            <i class="bi bi-pencil-square"></i> Build Schedule </a>
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