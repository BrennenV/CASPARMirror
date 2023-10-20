var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_Students').DataTable({
        "ajax": {
            "url": "/api/student",
            "type": "GET",
            "datatype": "json",
            "error": function (xhr, error, thrown) {
                alert('Ajax error:' + xhr.responseText); }
        },
        "columns": [
            { "data": "wishlistdetail.course.coursetitle", "width": "40%" },
            { "data": "modality.modalityname", "width": "15%" },
            { "data": "wishlistdetail.course.coursesection.classroom.buildiing.campus.campusname", "width": "15%" },
            { "data": "wishlistpartofday", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Students/Index?id=${data}" class="btn btn-primary-outline mb-1 rounded" style="cursor:pointer; width: 100px;">
                                    <i class="far fa-edit"></i> Edit </a>
                                <a href="/Students/Index?id=${data}" class="btn btn-danger mb-1 rounded text-white" style="cursor:pointer; width: 100px;">
                                    <i class="far fa-trash-alt"></i> Delete </a>    
                           </div>`;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No data found."
        },
        "width": "100%",
        "order": [[0, "asc"]]
    });
}