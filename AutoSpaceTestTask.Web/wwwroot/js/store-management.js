$(document).ready(function () {
    initProductsButton();
    initEditButton();
    initOpenStoresButton();
    initStoreDetailsHandler();
});

function initProductsButton() {
    $('.btn-products').click(function () {
        var storeId = $(this).data('store-id');
        var storeName = $(data).data('store-name');

        $('#storeName').text(storeName);
        $('#productsModal').modal('show');

        $.get(`/Store/Products?storeId=${storeId}`, function (data) {
            $('#productsModalBody').html(data);
        });
    });
}

function initEditButton() {
    $('.btn-edit').click(function () {
        var storeId = $(this).data('store-id');

        $.get(`/Store/GetStoreForEdit?storeId=${storeId}`, function (store) {
            var browserTimeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;
            var formHtml = buildEditFormHtml(store, browserTimeZone);

            $('#editStoreModalBody').html(formHtml);
            $('#editStoreModal').modal('show');

            attachEditFormHandlers(store);
        });
    });
}

function buildEditFormHtml(store, timeZone) {
    return `
        <form id="editStoreForm">
            <input type="hidden" name="storeId" value="${store.storeId}" />
            
            <div class="mb-3">
                <label for="name" class="form-label">Store Name</label>
                <input type="text" class="form-control" id="name" name="name" value="${store.name}" required>
            </div>
            
            <div class="mb-3">
                <label for="address" class="form-label">Address</label>
                <input type="text" class="form-control" id="address" name="address" value="${store.address}" required>
            </div>
            
            <div class="mb-3">
                <label class="form-label">Working Schedule</label>
                <div id="scheduleContainer">
                    ${renderSchedule(store.storeSchedulesDto)}
                </div>
                <button type="button" class="btn btn-sm btn-secondary mt-2" id="addScheduleRow">
                    Add Working Hours
                </button>
            </div>
            
            <input type="hidden" id="timeZone" name="timeZone" value="${timeZone}" />
            
            <button type="submit" class="btn btn-primary">Save Changes</button>
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
        </form>
    `;
}

function renderSchedule(schedules) {
    if (!schedules || schedules.length === 0) {
        return '<div class="alert alert-info">No schedule configured</div>';
    }

    var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    var html = '';

    schedules.forEach(function (schedule, index) {
        var isDayOffChecked = schedule.isDayOff ? 'checked' : '';
        var startTime = schedule.start || '09:00';
        var endTime = schedule.end || '18:00';

        html += `
            <div class="schedule-row mb-2 p-2 border rounded" data-index="${index}">
                <div class="row">
                    <div class="col-md-3">
                        <select name="storeSchedules[${index}].dayOfWeek" class="form-control">
                            ${days.map(day => `<option value="${day}" ${schedule.dayOfWeek === day ? 'selected' : ''}>${day}</option>`).join('')}
                        </select>
                    </div>
                    <div class="col-md-2">
                        <div class="form-check mt-2">
                            <input type="checkbox" class="form-check-input is-day-off" ${isDayOffChecked}>
                            <label class="form-check-label">Day Off</label>
                        </div>
                    </div>
                    <div class="col-md-3 time-fields" ${schedule.isDayOff ? 'style="display:none"' : ''}>
                        <input type="time" name="storeSchedules[${index}].start" class="form-control" value="${startTime}">
                    </div>
                    <div class="col-md-3 time-fields" ${schedule.isDayOff ? 'style="display:none"' : ''}>
                        <input type="time" name="storeSchedules[${index}].end" class="form-control" value="${endTime}">
                    </div>
                    <div class="col-md-1">
                        <button type="button" class="btn btn-danger btn-sm remove-schedule">X</button>
                    </div>
                </div>
                <input type="hidden" name="storeSchedules[${index}].isDayOff" value="${schedule.isDayOff}">
            </div>
        `;
    });

    return html;
}

function attachEditFormHandlers(store) {
    updateTimeFieldsVisibility();

    $('#addScheduleRow').click(function () {
        var index = $('.schedule-row').length;
        var newRow = createScheduleRow(index);
        $('#scheduleContainer').append(newRow);
        updateTimeFieldsVisibility();
    });

    $(document).on('change', '.is-day-off', function () {
        updateTimeFieldsVisibility();
    });

    $(document).on('click', '.remove-schedule', function () {
        $(this).closest('.schedule-row').remove();
    });

    $('#editStoreForm').submit(function (e) {
        e.preventDefault();
        submitEditForm(store);
    });
}

function createScheduleRow(index) {
    return `
        <div class="schedule-row mb-2 p-2 border rounded" data-index="${index}">
            <div class="row">
                <div class="col-md-3">
                    <select name="storeSchedules[${index}].dayOfWeek" class="form-control">
                        <option value="Monday">Monday</option>
                        <option value="Tuesday">Tuesday</option>
                        <option value="Wednesday">Wednesday</option>
                        <option value="Thursday">Thursday</option>
                        <option value="Friday">Friday</option>
                        <option value="Saturday">Saturday</option>
                        <option value="Sunday">Sunday</option>
                    </select>
                </div>
                <div class="col-md-2">
                    <div class="form-check mt-2">
                        <input type="checkbox" class="form-check-input is-day-off">
                        <label class="form-check-label">Day Off</label>
                    </div>
                </div>
                <div class="col-md-3 time-fields">
                    <input type="time" name="storeSchedules[${index}].start" class="form-control" value="09:00">
                </div>
                <div class="col-md-3 time-fields">
                    <input type="time" name="storeSchedules[${index}].end" class="form-control" value="18:00">
                </div>
                <div class="col-md-1">
                    <button type="button" class="btn btn-danger btn-sm remove-schedule">X</button>
                </div>
            </div>
            <input type="hidden" name="storeSchedules[${index}].isDayOff" value="false">
        </div>
    `;
}

function updateTimeFieldsVisibility() {
    $('.schedule-row').each(function () {
        var isDayOff = $(this).find('.is-day-off').is(':checked');
        var timeFields = $(this).find('.time-fields');
        var hiddenInput = $(this).find('input[name$=".isDayOff"]');

        if (isDayOff) {
            timeFields.hide();
            hiddenInput.val('true');
        } else {
            timeFields.show();
            hiddenInput.val('false');
        }
    });
}

function submitEditForm(store) {
    var schedules = collectScheduleData();

    var updateData = {
        storeId: store.storeId,
        name: $('#name').val(),
        address: $('#address').val(),
        storeSchedulesDto: schedules,
        timeZone: $('#timeZone').val()
    };

    $.ajax({
        url: '/Store/UpdateStore',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(updateData),
        success: function (response) {
            if (response.success) {
                $('#editStoreModal').modal('hide');
                location.reload();
            } else {
                alert('Error: ' + response.message);
            }
        },
        error: function (xhr) {
            alert('Error occurred: ' + (xhr.responseJSON?.message || 'Unknown error'));
        }
    });
}

function collectScheduleData() {
    var schedules = [];
    $('.schedule-row').each(function () {
        var dayOfWeek = $(this).find('select[name$=".dayOfWeek"]').val();
        var isDayOff = $(this).find('.is-day-off').is(':checked');
        var start = $(this).find('input[name$=".start"]').val();
        var end = $(this).find('input[name$=".end"]').val();

        schedules.push({
            dayOfWeek: dayOfWeek,
            isDayOff: isDayOff,
            start: start,
            end: end
        });
    });
    return schedules;
}

function initOpenStoresButton() {
    $('#btnOpenStores').click(function () {
        $('#openStoresModal').modal('show');

        $.get('/Store/GetOpenStores', function (data) {
            $('#openStoresModalBody').html(data);
        });
    });
}

function initStoreDetailsHandler() {
    $(document).on('click', '.btn-store-details', function () {
        var storeId = $(this).data('store-id');

        $.get(`/Store/GetStoreDetails?storeId=${storeId}`, function (data) {
            var scheduleHtml = formatSchedule(data.store.storeSchedulesDto);

            var detailsHtml = `
                <div class="mb-3">
                    <strong>Store Name:</strong> ${data.store.name}
                </div>
                <div class="mb-3">
                    <strong>Code:</strong> ${data.store.code}
                </div>
                <div class="mb-3">
                    <strong>Address:</strong> ${data.store.address}
                </div>
                <div class="mb-3">
                    <strong>Products Count:</strong> ${data.productCount}
                </div>
                <div class="mb-3">
                    <strong>Working Hours:</strong>
                    ${scheduleHtml}
                </div>
            `;

            $('#storeDetailsModalBody').html(detailsHtml);
            $('#storeDetailsModal').modal('show');
        });
    });
}

function formatSchedule(schedules) {
    if (!schedules || schedules.length === 0) {
        return '<div>Not configured</div>';
    }

    var orderedSchedules = schedules.sort((a, b) => {
        var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
        return days.indexOf(a.dayOfWeek) - days.indexOf(b.dayOfWeek);
    });

    var html = '';
    orderedSchedules.forEach(function (schedule) {
        var dayName = schedule.dayOfWeek.substring(0, 3);
        if (schedule.isDayOff) {
            html += `<div>${dayName}: off</div>`;
        } else {
            html += `<div>${dayName}: ${schedule.start} -- ${schedule.end}</div>`;
        }
    });

    return html;
}