﻿$(document).ready(function() {
    $('#CategoryId').change(function () {
        var categoryId = $(this).val();

        $.get('../task', { 'categoryId': categoryId }, function (data) {
            var data = renderTaskList(data);
            $('#taskListContainer').html(data);
        }, 'json');
    });

    onTaskChecked();
});

function categorySuccess(data) {
    $('#CategoryId').append('<option value="' + data.CategoryId + '">' + data.Name + '</option>');
}

function addTask(data) {
    $('#taskList').append(data);
}

function addSubTask(data) {
    var subtaskHtml = '<li><input type="checkbox" ' + (data.IsFinished ? 'checked' : '') + 
        'value=' + data.SubTaskId + ' data-type="1" />' + data.Text + '</li>';
    $('#' + data.TaskId).append(subtaskHtml);
}

function onSaveClick() {
    var taskList = $('#taskList');
    var checkboxes = taskList.find('input[type="checkbox"]');

    var sendArray = new Array();
    for (var i = 0; i < checkboxes.length; i++) {
        var obj = {
            IsFinished: $(checkboxes[i]).prop('checked'),
            TaskType: $(checkboxes[i]).attr('data-type'),
            Id: $(checkboxes[i]).attr('value')
        };
        sendArray.push(obj);
    }
    var obj = { saveData: sendArray };
    var str = JSON.stringify(obj);

    $.ajax({
        url: '../Home/SaveTasks',
//        url: '../task/save',
        data: str,
        type: 'PUT',
        contentType: 'application/json',
        dataType: 'json',
        success: function(result) {
            alert('Task list saved');
        }
    });
}

function onTaskChecked() {
    $(document).on('click', 'input[type="checkbox"]', function() {
        var type = $(this).attr('data-type');
        if (type == 0) {
            taskChecked(this);
        } else {
            subtaskChecked(this);
        }
    });
}

function taskChecked(issuer) {
    var isChecked = $(issuer).prop('checked');

    if (isChecked) {
        var id = $(issuer).val();
        var subtasks = $('#' + id + ' input[type="checkbox"]');

        for (var i = 0; i < subtasks.length; i++) {
            $(subtasks[i]).prop('checked', true);
        }
    }
}

function subtaskChecked(issuer) {
    var isChecked = $(issuer).prop('checked');
    var parent_li = $(issuer).parent();
    var parent_list = $(parent_li).parent();

    var taskId = $(parent_list).attr('id');
    var targetTask = $('#taskList input[type="checkbox"][value="' + taskId + '"][data-type="0"]');
    var subtasks = $(parent_list).find('input[type="checkbox"]');

    if ($(issuer).prop("checked")) {
        var taskCheck = true;
        for (var i = 0; i < subtasks.length; i++) {
            if (!$(subtasks[i]).prop('checked')) {
                taskCheck = false;
            }
        }

        $(targetTask).prop('checked', taskCheck);
    } else {
        $(targetTask).prop('checked', false);
    }


    if (isChecked) {
        
    }
}

function onDeleteClicked() {
    var taskList = $('#taskList');
    var checkboxes = taskList.find('input[type="checkbox"]:checked');

    var sendArray = new Array();
    for (var i = 0; i < checkboxes.length; i++) {
        var obj = {
//            IsFinished: $(checkboxes[i]).prop('checked'),
            TaskType: $(checkboxes[i]).attr('data-type'),
            Id: $(checkboxes[i]).attr('value')
        };
        sendArray.push(obj);
    }
    var obj = { deleteData: sendArray };
    var str = JSON.stringify(obj);

    $.ajax({
        url: '../Home/DeleteTasks',
//        url: '../task/delete',
        data: str,
        type: 'DELETE',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            for (var j = 0; j < checkboxes.length; j++) {
                var checkbox = checkboxes[j];
                var parent = $(checkbox).parent();
                $(parent).remove();
            }
        }
    });
}