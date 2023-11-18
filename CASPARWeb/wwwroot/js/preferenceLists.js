var wishlistId;

$(document).ready(function () {
    loadSemesterInstances();
    loadCheckBoxes();

    $('#ddlSemesterInstance').change(function () {
        //setSemesterInstanceIdForQueryString();
        loadTemplateCourses();
        getWishlistId().then(function () {
            loadCourseWishlist();
        });
    });

    $("#ddlTemplateCourses").change(function () {
        var selectedCourseId = $(this).val(item.course.id);
        $("#selectedCourse").val(selectedCourseId);
    });

    //$('body').on('click', '.btn', function () {
    //    var courseId = $(this).data('course-id');
    //    console.log("Course Id = " + courseId);
    //    $.ajax({
    //        url: '/Instr/Wishlists/Index?handler=ArchiveCourse',
    //        type: 'POST',
    //        data: { selectedCourse: courseId },
    //        success: function () {
    //            location.reload();
    //        }
    //    });
    //});
});

$(document).on('click', '#btnRemoveCourse', function () {
    var courseId = $(this).data('course-id');
    // Now you can use courseId in your function
});

function loadCheckBoxes() {
    $.ajax({
        url: "/api/modality",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var container = $("#modalities");
            container.empty();
            $.each(data.data, function (index, modality) {
                if (!modality.isArchived) {
                    var div = $('<div>').addClass('form-check');

                    var input = $('<input>').addClass('form-check-input').attr({
                        type: 'checkbox',
                        id: 'check' + (index + 1),
                        name: 'modality' + (index + 1),
                        value: modality.modalityName
                    });

                    var label = $('<label>').addClass('form-check-label').attr('for', input.attr('id')).text(modality.modalityName);

                    div.append(input, label);
                    container.append(div);
                }
            });
        },
        error: function (xhr, error, thrown) {
            alert('Ajax error:' + xhr.responseText);
        }
    });

    // Load the time blocks
    loadTimeBlocks();

    // Load the days of the week
    loadDaysOfWeek();

    // Load the campuses
    loadCampuses();
}

function loadTimeBlocks() {
    $.ajax({
        url: "/api/timeBlockList",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var container1 = $("#timeBlocks1");
            var container2 = $("#timeBlocks2");
            container1.empty();
            container2.empty();

            // Sort the data from least to greatest
            data.data.sort(function (a, b) {
                var aTimes = a.timeBlockValue.split(' - ');
                var bTimes = b.timeBlockValue.split(' - ');
                var aStart = new Date('1970/01/01 ' + aTimes[0]);
                var aEnd = new Date('1970/01/01 ' + aTimes[1]);
                var bStart = new Date('1970/01/01 ' + bTimes[0]);
                var bEnd = new Date('1970/01/01 ' + bTimes[1]);

                // Compare start times first, then end times if start times are equal
                return aStart - bStart || aEnd - bEnd;
            });


            $.each(data.data, function (index, timeBlock) {
                if (!timeBlock.isArchived) {
                    var div = $('<div>').addClass('form-check');

                    var input = $('<input>').addClass('form-check-input').attr({
                        type: 'checkbox',
                        id: 'timeBlock' + (index + 1),
                        name: 'timeBlock' + (index + 1),
                        value: timeBlock.timeBlockValue
                    });

                    var label = $('<label>').addClass('form-check-label').attr('for', input.attr('id')).text(timeBlock.timeBlockValue);

                    div.append(input, label);

                    // Add to the first container if index is even, second container if index is odd
                    if (index % 2 === 0) {
                        container1.append(div);
                    } else {
                        container2.append(div);
                    }
                }
            });
        },
        error: function (xhr, error, thrown) {
            alert('Ajax error:' + xhr.responseText);
        }
    });
}

function loadDaysOfWeek() {
    $.ajax({
        url: "/api/daysOfWeek",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var container = $("#daysOfWeek");
            container.empty();
            $.each(data.data, function (index, day) {
                if (!day.isArchived) {
                    var div = $('<div>').addClass('form-check');

                    var input = $('<input>').addClass('form-check-input').attr({
                        type: 'checkbox',
                        id: 'daysOfWeek' + (index + 1),
                        name: 'daysOfWeek' + (index + 1),
                        value: day.daysOfWeekValue
                    });

                    var label = $('<label>').addClass('form-check-label').attr('for', input.attr('id')).text(day.daysOfWeekValue);

                    div.append(input, label);
                    container.append(div);
                }
            });
        },
        error: function (xhr, error, thrown) {
            alert('Ajax error:' + xhr.responseText);
        }
    });
}

function loadCampuses() {
    $.ajax({
        url: "/api/campusList",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var container = $("#campuses");
            container.empty();
            $.each(data.data, function (index, campus) {
                if (!campus.isArchived) {
                    var div = $('<div>').addClass('form-check');

                    var input = $('<input>').addClass('form-check-input').attr({
                        type: 'checkbox',
                        id: 'campus' + (index + 1),
                        name: 'campus' + (index + 1),
                        value: campus.campusName
                    });

                    var label = $('<label>').addClass('form-check-label').attr('for', input.attr('id')).text(campus.campusName);

                    div.append(input, label);
                    container.append(div);
                }
            });
        },
        error: function (xhr, error, thrown) {
            alert('Ajax error:' + xhr.responseText);
        }
    });
}

function loadSemesterInstances() {
    $.ajax({
        url: "/api/semesterInstance",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var dropdown = $("#ddlSemesterInstance");
            dropdown.empty();
            $.each(data.data, function (index, item) {
                if (item.id == $("#selectedSemesterId").text()) {
                    dropdown.append($("<option />").val(item.id).text(item.semesterInstanceName).attr("selected", "selected"));
                }
                else {
                    dropdown.append($("<option />").val(item.id).text(item.semesterInstanceName));
                }
            });

            loadTemplateCourses();
            getWishlistId().then(function () {
                loadCourseWishlist();
            });
        },
        error: function (xhr, error, thrown) {
            alert('Ajax error:' + xhr.responseText);
        }
    });
}

function loadTemplateCourses() {
    var selectedSemesterId = $("#ddlSemesterInstance").val(); // get the selected semester ID

    $.ajax({
        url: "/api/template?id=" + selectedSemesterId, // pass the semester ID to the API
        type: "GET",
        dataType: "json",
        success: function (data) {
            var dropdown = $("#ddlTemplateCourses");
            dropdown.empty();
            dropdown.append($("<option />").val("").text("Add Courses")); // default option

            $.each(data.data, function (index, item) {
                var courseInfo = item.course.academicProgram.programCode + ' ' +
                    item.course.courseNumber + ' ' +
                    item.course.courseTitle;
                dropdown.append($("<option />").val(item.course.id).text(courseInfo));
            });
        },
        error: function (xhr, error, thrown) {
            alert('Ajax error:' + xhr.responseText);
        }
    });
}

function loadCourseWishlist() {
    $.ajax({
        url: "/api/wishlistCourse",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var table = $("#T_WishlistCourses");

            // Clear the table
            table.empty();

            // Add the table header
            var header = $('<tr>').append(
                $('<th>').text('Rank'),
                $('<th>').text('Course'),
                $('<th>').text(' ')
            );
            table.append(header);

            // Add the table body
            var body = $('<tbody>');
            table.append(body);

            // Sort the data by rank in ascending order
            data.data.sort(function (a, b) {
                return a.preferenceRank - b.preferenceRank;
            });

            // Populate the table with the sorted data
            $.each(data.data, function (i, item) {
                if (item.wishlistId == wishlistId) {
                    var row = $('<tr>').append(
                        $('<td>').text(item.preferenceRank),
                        $('<td>').text(item.course.academicProgram.programCode + " " + item.course.courseNumber + " " + item.course.courseTitle),
                        $('<td>').html('<button class="btn btn-outline-danger rounded archive-btn" data-course-id="' + item.courseId + '"><i class="bi bi-trash-fill"></i></button>')
                    );
                    body.append(row);  // Append the row to the tbody, not the table

                    // Add event listener to the button
                    row.find('.archive-btn').on('click', function () {
                        var courseId = $(this).data('course-id');
                        console.log("Course Id = " + courseId);

                        // Add post request here
                        $.ajax({
                            url: '/api/WishlistCourse/OnPostArchiveCourse',
                            type: 'POST',
                            data: { selectedCourse: courseId },
                            success: function (data) {
                                // Handle success, if needed
                                console.log("Post request successful");
                            },
                            error: function (error) {
                                // Handle error, if needed
                                console.error("Error in post request", error);
                            }
                        });

                    });
                }
            });


        },
        error: function (xhr, error, thrown) {
            alert('Ajax error:' + xhr.responseText);
        }
    });
}


function getWishlistId() {
    var selectedSemesterId = $("#ddlSemesterInstance").val();

    // Return the promise from the AJAX call
    return $.ajax({
        url: '/api/wishlist',
        type: 'GET',
        dataType: "json",
        success: function (data) {
            $.each(data.data, function (index, item) {
                if (item.userId == userId && item.semesterInstanceId == selectedSemesterId) {
                    console.log("Its working " + item.id);
                    wishlistId = item.id;
                }
            });
        },
        error: function (error) {
            console.log("Its not working");
            console.log(error);
        }
    });
}

function setSemesterInstanceIdForQueryString() {
    var dropdown = $("#ddlSemesterInstance");
    var btn = $("#addPreferenceBtn");

    var oldHref = btn.attr("href");
    var newHref = oldHref.slice(0, oldHref.indexOf("=") + 1) + dropdown.val().toString();

    btn.attr("href", newHref);
}

function loadTemplate() {
    
}