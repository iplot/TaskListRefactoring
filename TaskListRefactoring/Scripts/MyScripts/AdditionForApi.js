function addCategoryClick() {
    if ($('#Name').val() == "") {
        return;
    }

    $.ajax({
        url: '../category/add',
        data: $('#category').serialize(),
        type: 'POST',
        success: categorySuccess,
        dataType: 'json'
    });
}

function addTaskClick() {
    var categoryId = $('#CategoryId').val();
    var text = $('#Text').val();
    var date = $('#date').val();

    if (categoryId == 0 || text == '' || date == '') {
        return;
    }

    $.ajax({
        url: '../task/add',
        data: $('#task').serialize(),
        type: 'POST',
        dataType: 'json',
        success: function(data) {
            addTask(generateTaskHtml());
        }
    });
}

//!!!!
function generateTaskHtml(data) {
    var html = '<li><input type="checkbox"' + (data.IsFinished ? ' checked ' : '') + 'value="' + data.TaskId + '" data-type="0"/>' +
        data.Text + '(' + data.Date + ')' +
        '<form name="subTask" id="subTask">' +
        '<input type="text" value="'+ data.TaskId +'" class="hidden" name="TaskId"/>' +
        '<input type="text" name="Text" required=""/>' +
        '<input type="button" onclick="addSubTaskClick(this)" value="Add SubTask" class="btn btn-primary"/>' +
        '</form>' +
        '<ul id="' + data.TaskId + '">';

    if(data.SubTasks != null){
        for (var i = 0; i < data.SubTasks.length; i++) {
            html += '<li><input type="checkbox"' + (data.SubTasks[i].IsFinished ? ' checked ' : '')
                + 'value="'+ data.SubTasks[i].SubTaskId +'" data-type="1"/>' + data.SubTasks[i].Text + '</li>';
        }
    }

    html += '</ul></li>';
    return html;
}

function addSubTaskClick(item) {
    var form = $(item).parent();

    $.ajax({
        url: '../subtask/add',
        data: $(form).serialize(),
        type: 'POST',
        dataType: 'json',
        success: addSubTask
    });
}

//!!!!
function renderTaskList(data) {
    var html = '<h2>Task list</h2><ul id="taskList">';

    for (var i = 0; i < data.length; i++) {
        html += generateTaskHtml(data[i]);
    }

    html += '</ul><br/><button class="btn btn-success" onclick="onSaveClick()">Save</button>' +
        '<button class="btn btn-danger" onclick="onDeleteClicked()">Delete Finished</button>';

    return html;
}